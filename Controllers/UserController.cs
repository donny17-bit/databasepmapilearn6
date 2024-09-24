using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using databasepmapilearn6.models;

namespace databasepmapilearn6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<ActionResult<MUser>> PostUser([FromBody] MUser mUser)
        {
          if (_context.MUser == null)
          {
              return Problem("Entity set 'DatabasePmContext.MUser' is null.");
          }

          var User = new MUser {
            RoleId = mUser.RoleId,
            PositionId = mUser.PositionId,
            Username = mUser.Username,
            Name = mUser.Name,
            Email = mUser.Email,
            Password = mUser.Password,
            CreatedBy = 2, // sementara hardcode dulu 
            CreatedDate = DateTime.Now,
            IsDeleted = false // sementara hardcode dulu
          };

          try {
            await _context.MUser.AddAsync(User);
            await _context.SaveChangesAsync();

             // when success return user info as response
            return CreatedAtAction("GetMUser", new { id = mUser.Id }, mUser);
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
