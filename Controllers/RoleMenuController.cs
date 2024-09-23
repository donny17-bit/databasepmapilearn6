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
    public class RoleMenuController : ControllerBase
    {
        private readonly DatabasePmContext _context;

        public RoleMenuController(DatabasePmContext context)
        {
            _context = context;
        }

        // GET: api/RoleMenu
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MRoleMenu>>> GetMRoleMenu()
        {
          if (_context.MRoleMenu == null)
          {
              return NotFound();
          }
            return await _context.MRoleMenu.ToListAsync();
        }

        // GET: api/RoleMenu/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MRoleMenu>> GetMRoleMenu(int id)
        {
          if (_context.MRoleMenu == null)
          {
              return NotFound();
          }
            var mRoleMenu = await _context.MRoleMenu.FindAsync(id);

            if (mRoleMenu == null)
            {
                return NotFound();
            }

            return mRoleMenu;
        }

        // PUT: api/RoleMenu/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMRoleMenu(int id, MRoleMenu mRoleMenu)
        {
            if (id != mRoleMenu.Id)
            {
                return BadRequest();
            }

            _context.Entry(mRoleMenu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MRoleMenuExists(id))
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

        // POST: api/RoleMenu
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MRoleMenu>> PostMRoleMenu(MRoleMenu mRoleMenu)
        {
          if (_context.MRoleMenu == null)
          {
              return Problem("Entity set 'DatabasePmContext.MRoleMenu'  is null.");
          }
            _context.MRoleMenu.Add(mRoleMenu);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMRoleMenu", new { id = mRoleMenu.Id }, mRoleMenu);
        }

        // DELETE: api/RoleMenu/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMRoleMenu(int id)
        {
            if (_context.MRoleMenu == null)
            {
                return NotFound();
            }
            var mRoleMenu = await _context.MRoleMenu.FindAsync(id);
            if (mRoleMenu == null)
            {
                return NotFound();
            }

            _context.MRoleMenu.Remove(mRoleMenu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MRoleMenuExists(int id)
        {
            return (_context.MRoleMenu?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
