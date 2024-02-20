using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_webbservice.Models;
using projekt_webbservice.Data;

namespace projekt_webbservice.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserListApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserListApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/UserListApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserList>>> GetUserList()
        {
            return await _context.UserList.ToListAsync();
        }

        // GET: api/UserListApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserList>> GetUserList(int id)
        {
            var userList = await _context.UserList.FindAsync(id);

            if (userList == null)
            {
                return NotFound();
            }

            return userList;
        }

        // PUT: api/UserListApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserList(int id, UserList userList)
        {
            if (id != userList.ListID)
            {
                return BadRequest();
            }

            _context.Entry(userList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserListExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserListApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserList>> PostUserList(UserList userList)
        {
            _context.UserList.Add(userList);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserList", new { id = userList.ListID }, userList);
        }

        // DELETE: api/UserListApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserList(int id)
        {
            var userList = await _context.UserList.FindAsync(id);
            if (userList == null)
            {
                return NotFound();
            }

            _context.UserList.Remove(userList);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserListExists(int id)
        {
            return _context.UserList.Any(e => e.ListID == id);
        }
    }
}
