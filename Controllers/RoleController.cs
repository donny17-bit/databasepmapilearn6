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
using databasepmapilearn6.ViewModels;

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
        public async Task<ActionResult<MRole>> GetMRole(int? id)
        {
            if (_context.MRole == null)
            {
                return NotFound();
            }

            // pastikan id ada
            // ngga terlalu pengaruh ketika di ada/ngga
            // if (!id.HasValue) return BadRequest("role diperlukan");

            // get role id current user
            var RoleId = IMClaim.FromUserClaim(User.Claims).RoleId;

            if (RoleId != 1 && RoleId != 2) return BadRequest("you don't have permission to access");

            var role = await _context.MRole
                // user
                .Include(m => m.Users)
                // menu & icon
                .Include(m => m.RoleMenus).ThenInclude(m => m.Menu).ThenInclude(m => m.Icon)
                // filter
                .Where(m => (m.Id == id) && (!m.IsDeleted))
                .SingleOrDefaultAsync();

            if (role == null) return BadRequest("Role not found in the database");

            var res = VMRole.Detail.FromDb(role);

            return Ok(res);
        }

        // PUT: api/Role/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMRole(int id, MRole mRole)
        {
            if (_context.MRole == null) return Problem("Entity set 'DatabasePmContext.MRole' is null in PutMRole RoleController.");

            // check input is valid or not
            // return bad request if it's invalid 
            // without this method, the checking is still occurs behind the scene but the model will not know if it's an invalid data
            // in other word this used to return badrequest response if it's invalid
            if (!ModelState.IsValid) return BadRequest();

            // get claim (user info)
            var iClaim = IMClaim.FromUserClaim(User.Claims);

            // validate user
            if (iClaim.RoleId != 1 && iClaim.RoleId != 2) return BadRequest("you don't have permission to edit role");

            var role = await _context.MRole
                .Where(m => (m.Id == id) && (!m.IsDeleted))
                .SingleOrDefaultAsync();


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
