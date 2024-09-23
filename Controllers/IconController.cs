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
    public class IconController : ControllerBase
    {
        private readonly DatabasePmContext _context;

        public IconController(DatabasePmContext context)
        {
            _context = context;
        }

        // GET: api/Icon
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MIcon>>> GetMIcon()
        {
          if (_context.MIcon == null)
          {
              return NotFound();
          }
            return await _context.MIcon.ToListAsync();
        }

        // GET: api/Icon/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MIcon>> GetMIcon(int id)
        {
          if (_context.MIcon == null)
          {
              return NotFound();
          }
            var mIcon = await _context.MIcon.FindAsync(id);

            if (mIcon == null)
            {
                return NotFound();
            }

            return mIcon;
        }

        // PUT: api/Icon/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMIcon(int id, MIcon mIcon)
        {
            if (id != mIcon.Id)
            {
                return BadRequest();
            }

            _context.Entry(mIcon).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MIconExists(id))
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

        // POST: api/Icon
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MIcon>> PostMIcon(MIcon mIcon)
        {
          if (_context.MIcon == null)
          {
              return Problem("Entity set 'DatabasePmContext.MIcon'  is null.");
          }
            _context.MIcon.Add(mIcon);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMIcon", new { id = mIcon.Id }, mIcon);
        }

        // DELETE: api/Icon/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMIcon(int id)
        {
            if (_context.MIcon == null)
            {
                return NotFound();
            }
            var mIcon = await _context.MIcon.FindAsync(id);
            if (mIcon == null)
            {
                return NotFound();
            }

            _context.MIcon.Remove(mIcon);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MIconExists(int id)
        {
            return (_context.MIcon?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
