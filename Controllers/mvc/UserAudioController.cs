using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using project_webbservice.Models;
using projekt_webbservice.Data;

namespace projekt_webbservice.Controllers.mvc
{
    public class UserAudioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserAudioController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserAudio
    
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UserAudio.Include(u => u.Audio).Include(u => u.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UserAudio/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAudio = await _context.UserAudio
                .Include(u => u.Audio)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UserAudioId == id);
            if (userAudio == null)
            {
                return NotFound();
            }

            return View(userAudio);
        }

        // GET: UserAudio/Create
        public IActionResult Create()
        {
            ViewData["AudioId"] = new SelectList(_context.Audio, "AudioID", "AudioID");
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "Username");
            return View();
        }

        // POST: UserAudio/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserAudioId,UserId,AudioId")] UserAudio userAudio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userAudio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AudioId"] = new SelectList(_context.Audio, "AudioID", "AudioID", userAudio.AudioId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserId", userAudio.UserId);
            return View(userAudio);
        }

        // GET: UserAudio/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAudio = await _context.UserAudio.FindAsync(id);
            if (userAudio == null)
            {
                return NotFound();
            }
            ViewData["AudioId"] = new SelectList(_context.Audio, "AudioID", "AudioID", userAudio.AudioId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserId", userAudio.UserId);
            return View(userAudio);
        }

        // POST: UserAudio/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserAudioId,UserId,AudioId")] UserAudio userAudio)
        {
            if (id != userAudio.UserAudioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userAudio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserAudioExists(userAudio.UserAudioId))
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
            ViewData["AudioId"] = new SelectList(_context.Audio, "AudioID", "AudioID", userAudio.AudioId);
            ViewData["UserId"] = new SelectList(_context.User, "UserId", "UserId", userAudio.UserId);
            return View(userAudio);
        }

        // GET: UserAudio/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAudio = await _context.UserAudio
                .Include(u => u.Audio)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UserAudioId == id);
            if (userAudio == null)
            {
                return NotFound();
            }

            return View(userAudio);
        }

        // POST: UserAudio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userAudio = await _context.UserAudio.FindAsync(id);
            if (userAudio != null)
            {
                _context.UserAudio.Remove(userAudio);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserAudioExists(int id)
        {
            return _context.UserAudio.Any(e => e.UserAudioId == id);
        }
    }
}
