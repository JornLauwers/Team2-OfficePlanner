using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficePlanner.Server.Data;
using OfficePlanner.Server.Models;
using OfficePlanner.Shared;

namespace OfficePlanner.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthenticationManager authenticationManager;

        public UsersController(ApplicationDbContext context, IAuthenticationManager authenticationManager)
        {
            _context = context;
            this.authenticationManager = authenticationManager;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUsers(int id)
        {
            var users = await _context.Users.FindAsync(id);

            if (users == null)
            {
                return NotFound();
            }

            return users;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers(int id, Users users)
        {
            if (id != users.Id)
            {
                return BadRequest();
            }

            _context.Entry(users).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Users>> PostUsers(Users user)
        {

            user.Password = GeneratePasswordHash(user.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsers", new { id = user.Id}, user.Username);
        }


        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserCredentials userCredentials)
        {
            var user = _context.Users.Single(a => a.Username == userCredentials.Username);


            if (IsPasswordValid(user.Password, userCredentials.Password))
            {
                var token = authenticationManager.Authenticate(userCredentials.Username);
                return Ok(token);
            }

            return Unauthorized();
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        private string GeneratePasswordHash (string password)
        {
            // Hashing and salting info: https://www.mking.net/blog/password-security-best-practices-with-examples-in-csharp
            var salt = GenerateSalt(128);
            var iterations = 10000;
            var securePassword = GenerateHash(Encoding.ASCII.GetBytes(password), salt, iterations, 64);
            return Convert.ToBase64String(salt) + "," + Convert.ToBase64String(securePassword) + "," + iterations.ToString();
        }

        private bool IsPasswordValid (string passwordFromDb, string enteredPassword)
        {
            var pw = passwordFromDb.Split(',');
            var securePassword = GenerateHash(Encoding.ASCII.GetBytes(enteredPassword), Encoding.ASCII.GetBytes(pw[1]), Int32.Parse(pw[3]), 64);
            var base64StringPw = pw[1] + "," + Convert.ToBase64String(securePassword) + "," + pw[3];
            if (base64StringPw == passwordFromDb)
            {
                return true;
            }

            return false;
        }

        byte[] GenerateSalt(int length)
        {
            var bytes = new byte[length];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(bytes);
            }

            return bytes;
        }

        byte[] GenerateHash(byte[] password, byte[] salt, int iterations, int length)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                return deriveBytes.GetBytes(length);
            }
        }



    }
}
