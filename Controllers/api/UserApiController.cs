using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_webbservice.Models;
using projekt_webbservice.Data;
using BCrypt.Net;
using System.Security.Cryptography;



namespace projekt_webbservice.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/UserApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return await _context.User.ToListAsync();
        }

        // GET: api/UserApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }





        // PUT: api/UserApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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





        // POST: api/UserApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser([FromBody] User user)
        {

            //check is email exists when creating user
            var emailExists = await _context.User
                 .AnyAsync(u => u.Email == user.Email);

            if (emailExists)
            {
                return Conflict("Email already exists");

            }
            //check is username exists when creating user
            var usernameExists = await _context.User
                 .AnyAsync(u => u.Username == user.Username);

            if (usernameExists)
            {
                return Conflict("Username already exists");

            }
            //check if passwords matches
            string rpassword = HttpContext.Request.Query["rpassword"];
            if (rpassword != user.PasswordHash)
            {
                return Conflict("Passwords doesnt match");
            }


            // Hash the password using bcrypt
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            user.PasswordHash = hashedPassword;

            user.Created = DateTime.Now;
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }



        // POST: api/UserApi/login
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("login")]
        public async Task<ActionResult<User>> LoginUser([FromBody] User user)
        {


            // Validate input
            if (string.IsNullOrEmpty(user.PasswordHash) || string.IsNullOrEmpty(user.Email) || user == null)
            {
                return BadRequest("Invalid input");
            }

            try
            {
                string? hashedPassword = null;

                var userWithEmail = await _context.User
                    .FirstOrDefaultAsync(u => u.Email == user.Email);

                if (userWithEmail != null)
                {
                    hashedPassword = userWithEmail.PasswordHash;

                    if (BCrypt.Net.BCrypt.Verify(user.PasswordHash, hashedPassword))
                    {
                        // Generate a random token
                        byte[] randomBytes = new byte[32]; // 256 bits
                        using (var rng = RandomNumberGenerator.Create())
                        {
                            rng.GetBytes(randomBytes);
                        }
                        string token = Convert.ToBase64String(randomBytes);

                        userWithEmail.Token = token;
                        await _context.SaveChangesAsync();

                        return Ok(token);
                    }
                    else
                    {
                        return Unauthorized("Incorrect password");
                    }
                }
                else
                {
                    return NotFound("Email not found");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
            }

            // user.Created = DateTime.Now;
            // _context.User.Add(user);
            // await _context.SaveChangesAsync();

            // return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }



        // DELETE: api/UserApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.UserId == id);
        }
    }


}



