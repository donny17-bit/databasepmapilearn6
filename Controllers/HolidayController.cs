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
    public class HolidayController : ControllerBase
    {
        private readonly DatabasePmContext _context;

        public HolidayController(DatabasePmContext context)
        {
            _context = context;
        }

        // GET: api/Holiday
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MHoliday>>> GetMHoliday()
        {
          if (_context.MHoliday == null)
          {
              return NotFound();
          }
            return await _context.MHoliday.ToListAsync();
        }

        // GET: api/Holiday/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MHoliday>> GetMHoliday(int id)
        {
          if (_context.MHoliday == null)
          {
              return NotFound();
          }
            var mHoliday = await _context.MHoliday.FindAsync(id);

            if (mHoliday == null)
            {
                return NotFound();
            }

            return mHoliday;
        }

        // PUT: api/Holiday/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMHoliday(int id, MHoliday mHoliday)
        {
            if (id != mHoliday.Id)
            {
                return BadRequest();
            }

            _context.Entry(mHoliday).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MHolidayExists(id))
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

        // POST: api/Holiday
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MHoliday>> PostMHoliday(MHoliday mHoliday)
        {
          if (_context.MHoliday == null)
          {
              return Problem("Entity set 'DatabasePmContext.MHoliday'  is null.");
          }
            _context.MHoliday.Add(mHoliday);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMHoliday", new { id = mHoliday.Id }, mHoliday);
        }

        // DELETE: api/Holiday/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMHoliday(int id)
        {
            if (_context.MHoliday == null)
            {
                return NotFound();
            }
            var mHoliday = await _context.MHoliday.FindAsync(id);
            if (mHoliday == null)
            {
                return NotFound();
            }

            _context.MHoliday.Remove(mHoliday);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MHolidayExists(int id)
        {
            return (_context.MHoliday?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
