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
    public class UserListController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserListController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserList
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UserList.Include(u => u.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UserList/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userList = await _context.UserList
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.ListID == id);
            if (userList == null)
            {
                return NotFound();
            }

            return View(userList);
        }

        // GET: UserList/Create
        public IActionResult Create()
        {
            ViewData["UserID"] = new SelectList(_context.User, "UserId", "UserId");
            return View();
        }

        // POST: UserList/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ListID,UserID,ListName")] UserList userList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserID"] = new SelectList(_context.User, "UserId", "UserId", userList.UserID);
            return View(userList);
        }

        // GET: UserList/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userList = await _context.UserList.FindAsync(id);
            if (userList == null)
            {
                return NotFound();
            }
            ViewData["UserID"] = new SelectList(_context.User, "UserId", "UserId", userList.UserID);
            return View(userList);
        }

        // POST: UserList/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ListID,UserID,ListName")] UserList userList)
        {
            if (id != userList.ListID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserListExists(userList.ListID))
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
            ViewData["UserID"] = new SelectList(_context.User, "UserId", "UserId", userList.UserID);
            return View(userList);
        }

        // GET: UserList/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userList = await _context.UserList
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.ListID == id);
            if (userList == null)
            {
                return NotFound();
            }

            return View(userList);
        }

        // POST: UserList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userList = await _context.UserList.FindAsync(id);
            if (userList != null)
            {
                _context.UserList.Remove(userList);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserListExists(int id)
        {
            return _context.UserList.Any(e => e.ListID == id);
        }
    }
}
