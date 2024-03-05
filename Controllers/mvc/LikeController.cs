using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using project_webbservice.Models;
using projekt_webbservice.Data;
using Microsoft.AspNetCore.Authorization;


namespace projekt_webbservice.Controllers.mvc
{

    [Authorize]
    public class LikeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LikeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Like
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Like.Include(l => l.Audio).Include(l => l.User);
            return View(await applicationDbContext.ToListAsync());
        }
        // public async Task<IActionResult> Index(string searchstring)
        // {
        //     // If searchstring is null or empty
        //     if (string.IsNullOrEmpty(searchstring))
        //     {
        //         var allLikes = await _context.Like.ToListAsync();
        //         return View(allLikes);
        //     }
        //     else
        //     {
        //         // If searchstring exists
        //         // If searchstring exists
        //         // If searchstring exists
        //         var searchResult = await _context.Like
        //             .Where(a => a.LikeID.ToString().Contains(searchstring.ToLower()))
        //             .ToListAsync();

        //         ViewBag.search = searchstring;
        //         return View(searchResult);
        //     }
        // }



        // GET: Like/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var like = await _context.Like
                .Include(l => l.Audio)
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.LikeID == id);
            if (like == null)
            {
                return NotFound();
            }

            return View(like);
        }

        // GET: Like/Create
        public IActionResult Create()
        {
            ViewData["AudioID"] = new SelectList(_context.Audio, "AudioID", "AudioID");
            ViewData["UserID"] = new SelectList(_context.User, "UserId", "UserId");
            return View();
        }

        // POST: Like/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LikeID,UserID,AudioID,LikedAt")] Like like)
        {
            if (ModelState.IsValid)
            {
                _context.Add(like);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AudioID"] = new SelectList(_context.Audio, "AudioID", "AudioID", like.AudioID);
            ViewData["UserID"] = new SelectList(_context.User, "UserId", "UserId", like.UserID);
            return View(like);
        }

        // GET: Like/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var like = await _context.Like.FindAsync(id);
            if (like == null)
            {
                return NotFound();
            }
            ViewData["AudioID"] = new SelectList(_context.Audio, "AudioID", "AudioID", like.AudioID);
            ViewData["UserID"] = new SelectList(_context.User, "UserId", "UserId", like.UserID);
            return View(like);
        }

        // POST: Like/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LikeID,UserID,AudioID,LikedAt")] Like like)
        {
            if (id != like.LikeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(like);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LikeExists(like.LikeID))
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
            ViewData["AudioID"] = new SelectList(_context.Audio, "AudioID", "AudioID", like.AudioID);
            ViewData["UserID"] = new SelectList(_context.User, "UserId", "UserId", like.UserID);
            return View(like);
        }

        // GET: Like/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var like = await _context.Like
                .Include(l => l.Audio)
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.LikeID == id);
            if (like == null)
            {
                return NotFound();
            }

            return View(like);
        }

        // POST: Like/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var like = await _context.Like.FindAsync(id);
            if (like != null)
            {
                _context.Like.Remove(like);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LikeExists(int id)
        {
            return _context.Like.Any(e => e.LikeID == id);
        }
    }
}
