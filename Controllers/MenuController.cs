using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using databasepmapilearn6.models;
using Microsoft.AspNetCore.JsonPatch;

namespace databasepmapilearn6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly DatabasePmContext _context;

        public MenuController(DatabasePmContext context)
        {
            _context = context;
        }

        // GET: api/Menu
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MMenu>>> GetMMenus()
        {
          if (_context.MMenus == null)
          {
              return NotFound();
          }
            return await _context.MMenus.ToListAsync();
        }

        // GET: api/Menu/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MMenu>> GetMMenu(int id)
        {
          if (_context.MMenus == null)
          {
              return NotFound();
          }
            var mMenu = await _context.MMenus.FindAsync(id);

            if (mMenu == null)
            {
                return NotFound();
            }

            return mMenu;
        }


        // PUT: api/Menu/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMMenu(int id, MMenu mMenu)
        {
            if (id != mMenu.ID)
            {
                return BadRequest();
            }

            _context.Entry(mMenu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MMenuExists(id))
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

        // POST: api/Menu
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MMenu>> PostMMenu(MMenu mMenu)
        {
          if (_context.MMenus == null)
          {
              return Problem("Entity set 'DatabasePmContext.MMenus'  is null.");
          }
            _context.MMenus.Add(mMenu);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMMenu", new { id = mMenu.ID }, mMenu);
        }

        // DELETE: api/Menu/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMMenu(int id)
        {
            if (_context.MMenus == null)
            {
                return NotFound();
            }
            var mMenu = await _context.MMenus.FindAsync(id);
            if (mMenu == null)
            {
                return NotFound();
            }

            _context.MMenus.Remove(mMenu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MMenuExists(int id)
        {
            return (_context.MMenus?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
