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
    public class RoleController : ControllerBase
    {
        private readonly DatabasePmContext _context;

        public RoleController(DatabasePmContext context)
        {
            _context = context;
        }

        // GET: api/Role
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MRole>>> GetMRole()
        {
          if (_context.MRole == null)
          {
              return NotFound();
          }
            return await _context.MRole.ToListAsync();
        }

        // GET: api/Role/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MRole>> GetMRole(int id)
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

            return mRole;
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
