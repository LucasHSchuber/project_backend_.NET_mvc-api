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
    public class UserListAudioApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserListAudioApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/UserListAudioApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserListAudio>>> GetUserListAudio()
        {
            return await _context.UserListAudio.ToListAsync();
        }

        // GET: api/UserListAudioApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserListAudio>> GetUserListAudio(int id)
        {
            var userListAudio = await _context.UserListAudio.FindAsync(id);

            if (userListAudio == null)
            {
                return NotFound();
            }

            return userListAudio;
        }

        // PUT: api/UserListAudioApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserListAudio(int id, UserListAudio userListAudio)
        {
            if (id != userListAudio.UserListAudioID)
            {
                return BadRequest();
            }

            _context.Entry(userListAudio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserListAudioExists(id))
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

        // POST: api/UserListAudioApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserListAudio>> PostUserListAudio(UserListAudio userListAudio)
        {
            _context.UserListAudio.Add(userListAudio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserListAudio", new { id = userListAudio.UserListAudioID }, userListAudio);
        }

        // DELETE: api/UserListAudioApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserListAudio(int id)
        {
            var userListAudio = await _context.UserListAudio.FindAsync(id);
            if (userListAudio == null)
            {
                return NotFound();
            }

            _context.UserListAudio.Remove(userListAudio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserListAudioExists(int id)
        {
            return _context.UserListAudio.Any(e => e.UserListAudioID == id);
        }
    }
}
