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
    public class UserListAudioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserListAudioController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserListAudio
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UserListAudio.Include(u => u.Audio);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UserListAudio/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userListAudio = await _context.UserListAudio
                .Include(u => u.Audio)
                .FirstOrDefaultAsync(m => m.UserListAudioID == id);
            if (userListAudio == null)
            {
                return NotFound();
            }

            return View(userListAudio);
        }

        // GET: UserListAudio/Create
        public IActionResult Create()
        {
            ViewData["AudioID"] = new SelectList(_context.Audio, "AudioID", "Title");
            ViewData["ListID"] = new SelectList(_context.UserList, "ListID", "ListName");
            return View();
        }

        // POST: UserListAudio/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserListAudioID,ListID,AudioID")] UserListAudio userListAudio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userListAudio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AudioID"] = new SelectList(_context.Audio, "AudioID", "AudioID", userListAudio.AudioID);
            return View(userListAudio);
        }

        // GET: UserListAudio/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userListAudio = await _context.UserListAudio.FindAsync(id);
            if (userListAudio == null)
            {
                return NotFound();
            }
            ViewData["AudioID"] = new SelectList(_context.Audio, "AudioID", "AudioID", userListAudio.AudioID);
            return View(userListAudio);
        }

        // POST: UserListAudio/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserListAudioID,ListID,AudioID")] UserListAudio userListAudio)
        {
            if (id != userListAudio.UserListAudioID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userListAudio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserListAudioExists(userListAudio.UserListAudioID))
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
            ViewData["AudioID"] = new SelectList(_context.Audio, "AudioID", "AudioID", userListAudio.AudioID);
            return View(userListAudio);
        }

        // GET: UserListAudio/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userListAudio = await _context.UserListAudio
                .Include(u => u.Audio)
                .FirstOrDefaultAsync(m => m.UserListAudioID == id);
            if (userListAudio == null)
            {
                return NotFound();
            }

            return View(userListAudio);
        }

        // POST: UserListAudio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userListAudio = await _context.UserListAudio.FindAsync(id);
            if (userListAudio != null)
            {
                _context.UserListAudio.Remove(userListAudio);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserListAudioExists(int id)
        {
            return _context.UserListAudio.Any(e => e.UserListAudioID == id);
        }
    }
}
