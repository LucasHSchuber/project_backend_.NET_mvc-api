using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_webbservice.Models;
using projekt_webbservice.Data;
using System.Linq;

namespace projekt_webbservice.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAudioApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserAudioApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/UserAudioApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserAudio>>> GetUserAudio()
        {
            return await _context.UserAudio.ToListAsync();
        }

        // GET: api/UserAudioApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<UserAudio>>> GetUserAudio(int id)
        {
            var userAudios = await _context.UserAudio
            .Where(ua => ua.UserId == id)
            .ToListAsync();

            if (userAudios == null)
            {
                return NotFound();
            }

            return userAudios;
        }




        // PUT: api/UserAudioApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserAudio(int id, UserAudio userAudio)
        {
            if (id != userAudio.UserAudioId)
            {
                return BadRequest();
            }

            _context.Entry(userAudio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserAudioExists(id))
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

        // POST: api/UserAudioApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserAudio>> PostUserAudio(UserAudio userAudio)
        {
            if (userAudio == null)
            {
                return BadRequest("Invalid data: UserAudio object is null.");
            }

            bool alreadyExists = await _context.UserAudio
                .AnyAsync(l => l.UserId == userAudio.UserId && l.AudioId == userAudio.AudioId);

            if (alreadyExists)
            {
                return BadRequest("User already has the audio in the table.");
            }

            try
            {
                _context.UserAudio.Add(userAudio);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetUserAudio", new { id = userAudio.UserAudioId }, userAudio);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error occurred while saving UserAudio: {ex.Message}");

                // Return an appropriate error response
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while saving UserAudio.");
            }
        }


        // DELETE: api/UserAudioApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAudio(int id)
        {
            var userAudio = await _context.UserAudio.FindAsync(id);
            if (userAudio == null)
            {
                return NotFound();
            }

            _context.UserAudio.Remove(userAudio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserAudioExists(int id)
        {
            return _context.UserAudio.Any(e => e.UserAudioId == id);
        }



        // DELETE: api/UserAudioApi/5/7
        [HttpDelete("{userId}/{audioId}")]
        public async Task<IActionResult> DeleteUserAudio(int userId, int audioId)
        {
            try
            {
                // Find the user audio record to delete
                var userAudio = await _context.UserAudio
                    .FirstOrDefaultAsync(u => u.UserId == userId && u.AudioId == audioId);

                if (userAudio == null)
                {
                    return NotFound();
                }

                _context.UserAudio.Remove(userAudio);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while deleting UserAudio: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting UserAudio.");
            }
        }






    }
}



