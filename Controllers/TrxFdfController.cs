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
    public class TrxFdfController : ControllerBase
    {
        private readonly DatabasePmContext _context;

        public TrxFdfController(DatabasePmContext context)
        {
            _context = context;
        }

        // GET: api/TrxFdf
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrxFdf>>> GetTrxFdf()
        {
          if (_context.TrxFdf == null)
          {
              return NotFound();
          }
            return await _context.TrxFdf.ToListAsync();
        }

        // GET: api/TrxFdf/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TrxFdf>> GetTrxFdf(int id)
        {
          if (_context.TrxFdf == null)
          {
              return NotFound();
          }
            var trxFdf = await _context.TrxFdf.FindAsync(id);

            if (trxFdf == null)
            {
                return NotFound();
            }

            return trxFdf;
        }

        // PUT: api/TrxFdf/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrxFdf(int id, TrxFdf trxFdf)
        {
            if (id != trxFdf.Id)
            {
                return BadRequest();
            }

            _context.Entry(trxFdf).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrxFdfExists(id))
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

        // POST: api/TrxFdf
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TrxFdf>> PostTrxFdf(TrxFdf trxFdf)
        {
          if (_context.TrxFdf == null)
          {
              return Problem("Entity set 'DatabasePmContext.TrxFdf'  is null.");
          }
            _context.TrxFdf.Add(trxFdf);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrxFdf", new { id = trxFdf.Id }, trxFdf);
        }

        // DELETE: api/TrxFdf/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrxFdf(int id)
        {
            if (_context.TrxFdf == null)
            {
                return NotFound();
            }
            var trxFdf = await _context.TrxFdf.FindAsync(id);
            if (trxFdf == null)
            {
                return NotFound();
            }

            _context.TrxFdf.Remove(trxFdf);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrxFdfExists(int id)
        {
            return (_context.TrxFdf?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
