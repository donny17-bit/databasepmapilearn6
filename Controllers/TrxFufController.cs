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
    public class TrxFufController : ControllerBase
    {
        private readonly DatabasePmContext _context;

        public TrxFufController(DatabasePmContext context)
        {
            _context = context;
        }

        // GET: api/TrxFuf
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrxFuf>>> GetTrxFuf()
        {
            if (_context.TrxFuf == null)
            {
                return NotFound();
            }
            return await _context.TrxFuf.ToListAsync();
        }

        // GET: api/TrxFuf/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TrxFuf>> GetTrxFuf(int id)
        {
            if (_context.TrxFuf == null)
            {
                return NotFound();
            }
            var trxFuf = await _context.TrxFuf.FindAsync(id);

            if (trxFuf == null)
            {
                return NotFound();
            }

            return trxFuf;
        }

        // PUT: api/TrxFuf/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrxFuf(int id, TrxFuf trxFuf)
        {
            if (id != trxFuf.Id)
            {
                return BadRequest();
            }

            _context.Entry(trxFuf).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrxFufExists(id))
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

        // POST: api/TrxFuf
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TrxFuf>> PostTrxFuf(TrxFuf trxFuf)
        {
            if (_context.TrxFuf == null)
            {
                return Problem("Entity set 'DatabasePmContext.TrxFuf'  is null.");
            }
            _context.TrxFuf.Add(trxFuf);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrxFuf", new { id = trxFuf.Id }, trxFuf);
        }

        // DELETE: api/TrxFuf/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrxFuf(int id)
        {
            if (_context.TrxFuf == null)
            {
                return NotFound();
            }
            var trxFuf = await _context.TrxFuf.FindAsync(id);
            if (trxFuf == null)
            {
                return NotFound();
            }

            _context.TrxFuf.Remove(trxFuf);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrxFufExists(int id)
        {
            return (_context.TrxFuf?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
