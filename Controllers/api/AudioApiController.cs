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
    public class AudioApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AudioApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/AudioApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AudioDto>>> GetAudio()
        {
            var audioDtos = await _context.Audio
                .Include(a => a.Category) // Include the Category navigation property
                .Select(a => new AudioDto
                {
                    AudioID = a.AudioID,
                    Title = a.Title,
                    Description = a.Description,
                    Duration = a.Duration,
                    Created = a.Created,
                    ImageName = a.ImageName,
                    ImageNameOriginal = a.ImageNameOriginal,
                    FilePath = a.FilePath,
                    CategoryName = a.Category.Name // Include the Category name
                })
            .ToListAsync();

            return audioDtos;
        }

        // GET: api/AudioApi/bycategory
        [HttpGet("bycategory/{category}")]
        public async Task<ActionResult<IEnumerable<AudioDto>>> GetAudio(string category)
        {
            var audioDtos = await _context.Audio
                .Include(a => a.Category) // Include the Category navigation property
                .Where(a => a.Category.Name == category)
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


        // GET: api/AudioApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Audio>> GetAudio(int id)
        {
            var audio = await _context.Audio.FindAsync(id);

            if (audio == null)
            {
                return NotFound();
            }

            return audio;
        }

        // GET: api/UserApi/mylist/5
        [HttpGet("{id}/mylist")]
        public async Task<ActionResult<IEnumerable<AudioDto>>> GetAudiosByUserId(int id)
        {
            var userAudios = await _context.UserAudio
                .Where(ua => ua.UserId == id)
                .Select(ua => ua.AudioId)
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

        // GET: api/Audio
        [HttpGet("{id}/list")]
        public async Task<ActionResult<IEnumerable<Audio>>> GetAudios(int id)
        {
            // var userAudios = await _context.UserAudio
            //     .Where(ua => ua.UserId == id)
            //     .Select(ua => ua.AudioId)
            //     .ToListAsync();

            var audios = await _context.Audio
                .Include(a => a.Category) // Include the Category navigation property
                                          // .Where(a => userAudios.Contains(a.AudioID)) // Filter audios based on user's list
                .ToListAsync();

            return audios;
        }




        // PUT: api/AudioApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAudio(int id, Audio audio)
        {
            if (id != audio.AudioID)
            {
                return BadRequest();
            }

            _context.Entry(audio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AudioExists(id))
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

        // POST: api/AudioApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Audio>> PostAudio(Audio audio)
        {
            _context.Audio.Add(audio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAudio", new { id = audio.AudioID }, audio);
        }

        // DELETE: api/AudioApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAudio(int id)
        {
            var audio = await _context.Audio.FindAsync(id);
            if (audio == null)
            {
                return NotFound();
            }

            _context.Audio.Remove(audio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AudioExists(int id)
        {
            return _context.Audio.Any(e => e.AudioID == id);
        }
    }
}
