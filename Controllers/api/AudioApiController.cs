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
    public class AudioApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AudioApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/AudioApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Audio>>> GetAudio()
        {
            return await _context.Audio.ToListAsync();
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
