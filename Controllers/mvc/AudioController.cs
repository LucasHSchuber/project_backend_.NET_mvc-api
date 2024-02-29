using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using project_webbservice.Models;
using projekt_webbservice.Data;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;
using NAudio.Wave;

using System.IO;

namespace projekt_webbservice.Controllers.mvc
{
    public class AudioController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly string wwwRootPath;


        public AudioController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            wwwRootPath = hostEnvironment.WebRootPath;
        }

        // GET: Audio
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Audio.Include(a => a.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Audio/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var audio = await _context.Audio
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.AudioID == id);
            if (audio == null)
            {
                return NotFound();
            }

            return View(audio);
        }

        // GET: Audio/Create
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Category, "CategoryId", "Name");
            return View();
        }

        // POST: Audio/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AudioID,Title,Description,Duration,ImageFile,AudioFile,Created,CategoryID,AudioData")] Audio audio)
        {
            if (ModelState.IsValid)
            {
                //checks if image file is passed to backend
                if (audio.ImageFile != null)
                {
                    // Save the original image
                    string originalFileName = $"original_{Path.GetFileName(audio.ImageFile.FileName)}";
                    string originalPath = Path.Combine(wwwRootPath + "/imgupload", originalFileName);
                    using (var originalFileStream = new FileStream(originalPath, FileMode.Create))
                    {
                        await audio.ImageFile.CopyToAsync(originalFileStream);
                    }

                    // Resize and compress the image
                    using (var image = SixLabors.ImageSharp.Image.Load(audio.ImageFile.OpenReadStream()))
                    {
                        image.Mutate(x => x.Resize(new ResizeOptions
                        {
                            Mode = ResizeMode.Max, // Choose resize mode
                            Size = new SixLabors.ImageSharp.Size(800, 600) // Set the maximum dimensions
                        }));

                        // Save the compressed image
                        string fileName = $"compressed_{Path.GetFileName(audio.ImageFile.FileName)}";
                        string path = Path.Combine(wwwRootPath + "/imgupload", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            image.Save(fileStream, new JpegEncoder { Quality = 80 }); // Adjust quality as needed
                        }

                        // Set the UNcompressed image name
                        audio.ImageNameOriginal = originalFileName;
                        // Set the compressed image name
                        audio.ImageName = fileName;
                    }
                }
                else
                {
                    // ModelState.AddModelError(string.Empty, "Please select an image file.");
                    return View(audio);
                }

                //checks if audio file is passed to backend
                if (audio.AudioFile != null)
                {
                    // Handle audio file upload
                    string audioFileName = Path.GetFileNameWithoutExtension(audio.AudioFile.FileName);
                    string audioExtension = Path.GetExtension(audio.AudioFile.FileName);
                    string uniqueAudioFileName = audioFileName.Replace(" ", String.Empty) + DateTime.Now.ToString("yymmssff") + audioExtension;
                    string audioPath = Path.Combine(wwwRootPath + "/audioupload", uniqueAudioFileName);
                    audio.FilePath = uniqueAudioFileName;

                    using (var fileStream = new FileStream(audioPath, FileMode.Create))
                    {
                        await audio.AudioFile.CopyToAsync(fileStream);
                    }

                }
                else
                {
                    // ModelState.AddModelError(string.Empty, "Please select an audio file");
                    return View(audio);
                }

                audio.Created = DateTime.Now;

                _context.Add(audio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["CategoryID"] = new SelectList(_context.Category, "CategoryId", "Name", audio.CategoryID);
            return View(audio);
        }




        // GET: Audio/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var audio = await _context.Audio.FindAsync(id);
            if (audio == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(_context.Category, "CategoryId", "Name", audio.CategoryID);
            return View(audio);
        }

        // POST: Audio/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AudioID,Title,Description,Duration,FilePath,Created,CategoryID,AudioData")] Audio audio)
        {
            if (id != audio.AudioID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(audio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AudioExists(audio.AudioID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(_context.Category, "CategoryId", "CategoryId", audio.CategoryID);
            return View(audio);
        }

        // GET: Audio/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var audio = await _context.Audio
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.AudioID == id);
            if (audio == null)
            {
                return NotFound();
            }

            return View(audio);
        }

        // POST: Audio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var audio = await _context.Audio.FindAsync(id);
            if (audio != null)
            {
                _context.Audio.Remove(audio);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AudioExists(int id)
        {
            return _context.Audio.Any(e => e.AudioID == id);
        }
    }
}
