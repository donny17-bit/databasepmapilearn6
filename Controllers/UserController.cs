using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using databasepmapilearn6.models;
using databasepmapilearn6.Utilities;
using databasepmapilearn6.InputModels;
using Microsoft.AspNetCore.Authorization;


namespace databasepmapilearn6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly DatabasePmContext _context;

        public UserController(DatabasePmContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MUser>>> GetMUser()
        {
          if (_context.MUser == null)
          {
              return NotFound();
          }
            return await _context.MUser.ToListAsync();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MUser>> GetMUser(int id)
        {
          if (_context.MUser == null)
          {
              return NotFound();
          }
            var mUser = await _context.MUser.FindAsync(id);

            if (mUser == null)
            {
                return NotFound();
            }

            return mUser;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMUser(int id, MUser mUser)
        {
            if (id != mUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(mUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MUserExists(id))
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

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MUser>> PostUser([FromBody] IMUser.Create mUser)
        {
            if (_context.MUser == null)
            {
                return Problem("Entity set 'DatabasePmContext.MUser' is null.");
            }


            // validasi cek role user sebelum create akun user
            // User object auto generate from system security
            var iClaim = IMClaim.FromUserClaim(User.Claims);
            if (iClaim.RoleId != 1 && iClaim.RoleId != 2) return BadRequest("user don't have permission to create user");

            // generate random password
            var (rawPassword, hashedPassword) = UtlSecurity.GeneratePassword(16);

            var user = new MUser {
                RoleId = mUser.RoleId,
                PositionId = mUser.PositionId,
                Username = mUser.Username,
                Name = mUser.Name,
                Email = mUser.Email,
                Password = hashedPassword, 
                RetryCount = 0, // sementara hardcode dulu
                CreatedBy = iClaim.Id, 
                CreatedDate = DateTime.Now,
                IsDeleted = false // sementara hardcode dulu
            };

            var res = new {
                roleId = mUser.RoleId,
                positionId = mUser.PositionId,
                username = mUser.Username,
                name = mUser.Name,
                email = mUser.Email,
                password = rawPassword
            };

            try {
                await _context.MUser.AddAsync(user);
                await _context.SaveChangesAsync();

                // when success return success code and send user information
                return Created("/api/user", res);
            }
            catch {  
                return BadRequest("Error on the API"); // change the error response later
            }
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMUser(int id)
        {
            if (_context.MUser == null)
            {
                return NotFound();
            }
            var mUser = await _context.MUser.FindAsync(id);
            if (mUser == null)
            {
                return NotFound();
            }

            _context.MUser.Remove(mUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MUserExists(int id)
        {
            return (_context.MUser?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
