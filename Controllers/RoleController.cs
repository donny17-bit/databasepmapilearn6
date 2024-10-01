using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using databasepmapilearn6.models;
using Microsoft.AspNetCore.Authorization;
using databasepmapilearn6.InputModels;

namespace databasepmapilearn6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoleController : ControllerBase
    {
        private readonly DatabasePmContext _context;

        public RoleController(DatabasePmContext context)
        {
            _context = context;
        }

        // GET: api/Role/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MRole>> GetMRole(int id)
        {
            if (_context.MRole == null)
            {
                return NotFound();
            }

            // pastikan id ada
            // if (!id.HasValue) return BadRequest("role diperlukan");

            // get role id current user
            var RoleId = IMClaim.FromUserClaim(User.Claims).RoleId;

            if (RoleId != 1 && RoleId != 2) return BadRequest("you don't have permission to access");

            var mRole = await _context.MRole
                .Where(m => (m.Id == id) && (!m.IsDeleted))
                .SingleOrDefaultAsync();

            if (mRole == null) return BadRequest("Role not found in the database");

            return Ok(mRole);
        }

        // PUT: api/Role/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMRole(int id, MRole mRole)
        {
            if (id != mRole.Id)
            {
                return BadRequest();
            }

            _context.Entry(mRole).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MRoleExists(id))
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

        // POST: api/Role
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MRole>> PostMRole(MRole mRole)
        {
            if (_context.MRole == null)
            {
                return Problem("Entity set 'DatabasePmContext.MRole'  is null.");
            }
            _context.MRole.Add(mRole);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMRole", new { id = mRole.Id }, mRole);
        }

        // DELETE: api/Role/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMRole(int id)
        {
            if (_context.MRole == null)
            {
                return NotFound();
            }
            var mRole = await _context.MRole.FindAsync(id);
            if (mRole == null)
            {
                return NotFound();
            }

            _context.MRole.Remove(mRole);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MRoleExists(int id)
        {
            return (_context.MRole?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
