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
    public class RunningNumberController : ControllerBase
    {
        private readonly DatabasePmContext _context;

        public RunningNumberController(DatabasePmContext context)
        {
            _context = context;
        }

        // GET: api/RunningNumber
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MRunningNumber>>> GetMRunningNumber()
        {
          if (_context.MRunningNumber == null)
          {
              return NotFound();
          }
            return await _context.MRunningNumber.ToListAsync();
        }

        // GET: api/RunningNumber/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MRunningNumber>> GetMRunningNumber(int id)
        {
          if (_context.MRunningNumber == null)
          {
              return NotFound();
          }
            var mRunningNumber = await _context.MRunningNumber.FindAsync(id);

            if (mRunningNumber == null)
            {
                return NotFound();
            }

            return mRunningNumber;
        }

        // PUT: api/RunningNumber/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMRunningNumber(int id, MRunningNumber mRunningNumber)
        {
            if (id != mRunningNumber.Id)
            {
                return BadRequest();
            }

            _context.Entry(mRunningNumber).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MRunningNumberExists(id))
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

        // POST: api/RunningNumber
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MRunningNumber>> PostMRunningNumber(MRunningNumber mRunningNumber)
        {
          if (_context.MRunningNumber == null)
          {
              return Problem("Entity set 'DatabasePmContext.MRunningNumber'  is null.");
          }
            _context.MRunningNumber.Add(mRunningNumber);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMRunningNumber", new { id = mRunningNumber.Id }, mRunningNumber);
        }

        // DELETE: api/RunningNumber/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMRunningNumber(int id)
        {
            if (_context.MRunningNumber == null)
            {
                return NotFound();
            }
            var mRunningNumber = await _context.MRunningNumber.FindAsync(id);
            if (mRunningNumber == null)
            {
                return NotFound();
            }

            _context.MRunningNumber.Remove(mRunningNumber);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MRunningNumberExists(int id)
        {
            return (_context.MRunningNumber?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
