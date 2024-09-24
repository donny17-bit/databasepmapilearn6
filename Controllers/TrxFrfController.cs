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
    public class TrxFrfController : ControllerBase
    {
        private readonly DatabasePmContext _context;

        public TrxFrfController(DatabasePmContext context)
        {
            _context = context;
        }

        // GET: api/TrxFrf
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrxFrf>>> GetTrxFrf()
        {
          if (_context.TrxFrf == null)
          {
              return NotFound();
          }
            return await _context.TrxFrf.ToListAsync();
        }

        // GET: api/TrxFrf/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TrxFrf>> GetTrxFrf(int id)
        {
          if (_context.TrxFrf == null)
          {
              return NotFound();
          }
            var trxFrf = await _context.TrxFrf.FindAsync(id);

            if (trxFrf == null)
            {
                return NotFound();
            }

            return trxFrf;
        }

        // PUT: api/TrxFrf/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrxFrf(int id, TrxFrf trxFrf)
        {
            if (id != trxFrf.Id)
            {
                return BadRequest();
            }

            _context.Entry(trxFrf).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrxFrfExists(id))
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

        // POST: api/TrxFrf
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TrxFrf>> PostTrxFrf(TrxFrf trxFrf)
        {
          if (_context.TrxFrf == null)
          {
              return Problem("Entity set 'DatabasePmContext.TrxFrf'  is null.");
          }
            _context.TrxFrf.Add(trxFrf);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrxFrf", new { id = trxFrf.Id }, trxFrf);
        }

        // DELETE: api/TrxFrf/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrxFrf(int id)
        {
            if (_context.TrxFrf == null)
            {
                return NotFound();
            }
            var trxFrf = await _context.TrxFrf.FindAsync(id);
            if (trxFrf == null)
            {
                return NotFound();
            }

            _context.TrxFrf.Remove(trxFrf);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrxFrfExists(int id)
        {
            return (_context.TrxFrf?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
