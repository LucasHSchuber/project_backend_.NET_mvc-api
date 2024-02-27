using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_webbservice.Models;
using projekt_webbservice.Data;
using projekt_webbservice.DTOs;

namespace projekt_webbservice.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LikeApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/LikeApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Like>>> GetLike()
        {
            return await _context.Like.ToListAsync();
        }

        // GET: api/LikeApi/5
        [HttpGet("{id}")]
        public async Task<IEnumerable<Like>> GetLike(int id)
        {
            var likesByUser = await _context.Like
                .Where(u => u.UserID == id)
                .ToListAsync();

            // Check if any likes are found
            if (likesByUser == null || !likesByUser.Any())
            {
                // Return an empty collection if no likes are found
                return Enumerable.Empty<Like>();
            }

            return likesByUser;
        }

        // GET: api/UserApi/mylist/5
        [HttpGet("{id}/myfavorites")]
        public async Task<ActionResult<IEnumerable<AudioDto>>> GetAudiosByUserId(int id)
        {
            var userAudios = await _context.Like
                .Where(ua => ua.UserID == id)
                .Select(ua => ua.AudioID)
                .ToListAsync();

            var audioDtos = await _context.Audio
                .Include(a => a.Category) // Include the Category navigation property
                .Where(a => userAudios.Contains(a.AudioID)) // Filter audios based on user's list
                .Select(a => new AudioDto
                {
                    AudioID = a.AudioID,
                    Title = a.Title,
                    Description = a.Description,
                    Duration = a.Duration,
                    Created = a.Created,
                    ImageName = a.ImageName,
                    FilePath = a.FilePath,
                    CategoryName = a.Category.Name // Include the Category name
                })
                .ToListAsync();

            return audioDtos;
        }



        // PUT: api/LikeApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLike(int id, Like like)
        {
            if (id != like.LikeID)
            {
                return BadRequest();
            }

            _context.Entry(like).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LikeExists(id))
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




        // POST: api/LikeApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Like>> PostLike(Like like)
        {
            _context.Like.Add(like);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLike", new { id = like.LikeID }, like);
        }




        // DELETE: api/LikeApi/5
        [HttpDelete("{userId}/{audioId}")]
        public async Task<IActionResult> DeleteUserAudio(int userId, int audioId)
        {
            // var like = await _context.Like.FindAsync(id);
            var audioToDelete = await _context.Like
                     .FirstOrDefaultAsync(u => u.UserID == userId && u.AudioID == audioId);


            if (audioToDelete == null)
            {
                return NotFound();
            }

            _context.Like.Remove(audioToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LikeExists(int id)
        {
            return _context.Like.Any(e => e.LikeID == id);
        }
    }
}
