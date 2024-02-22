using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using project_webbservice.Models;
using projekt_webbservice.Data;

namespace projekt_webbservice.Controllers
{
    public class AvatarController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly string wwwRootPath;


        public AvatarController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
           _context = context;
            _hostEnvironment = hostEnvironment;
            wwwRootPath = hostEnvironment.WebRootPath;
        }

        // GET: Avatar
        public async Task<IActionResult> Index()
        {
            return View(await _context.Avatar.ToListAsync());
        }

        // GET: Avatar/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avatar = await _context.Avatar
                .FirstOrDefaultAsync(m => m.AvatarId == id);
            if (avatar == null)
            {
                return NotFound();
            }

            return View(avatar);
        }

        // GET: Avatar/Create
        public IActionResult Create()
        {
            return View();
        }




        // POST: Avatar/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AvatarId,AvatarImageName,AvatarImageFile")] Avatar avatar)
        {
            if (ModelState.IsValid)
            {

                if (avatar.AvatarImageFile != null)
                {

                    //generate new file name
                    string fileName = Path.GetFileNameWithoutExtension(avatar.AvatarImageFile.FileName);
                    string extension = Path.GetExtension(avatar.AvatarImageFile.FileName);

                    avatar.AvatarImageName = fileName = fileName.Replace(" ", String.Empty) + DateTime.Now.ToString("yymmssff") + extension;

                    string path = Path.Combine(wwwRootPath + "/imgupload", fileName);

                    //store in file system
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await avatar.AvatarImageFile.CopyToAsync(fileStream);
                    }
                }
                else
                {
                    avatar.AvatarImageName = "empty.jpg";
                }

                _context.Add(avatar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(avatar);
        }




        // GET: Avatar/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avatar = await _context.Avatar.FindAsync(id);
            if (avatar == null)
            {
                return NotFound();
            }
            return View(avatar);
        }




        // POST: Avatar/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AvatarId,AvatarImageName")] Avatar avatar)
        {
            if (id != avatar.AvatarId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(avatar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AvatarExists(avatar.AvatarId))
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
            return View(avatar);
        }

        // GET: Avatar/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avatar = await _context.Avatar
                .FirstOrDefaultAsync(m => m.AvatarId == id);
            if (avatar == null)
            {
                return NotFound();
            }

            return View(avatar);
        }

        // POST: Avatar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var avatar = await _context.Avatar.FindAsync(id);
            if (avatar != null)
            {
                _context.Avatar.Remove(avatar);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AvatarExists(int id)
        {
            return _context.Avatar.Any(e => e.AvatarId == id);
        }
    }
}
