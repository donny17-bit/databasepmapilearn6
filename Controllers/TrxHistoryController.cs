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
    public class TrxHistoryController : ControllerBase
    {
        private readonly DatabasePmContext _context;

        public TrxHistoryController(DatabasePmContext context)
        {
            _context = context;
        }

        // GET: api/TrxHistory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrxHistory>>> GetTrxHistory()
        {
          if (_context.TrxHistory == null)
          {
              return NotFound();
          }
            return await _context.TrxHistory.ToListAsync();
        }

        // GET: api/TrxHistory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TrxHistory>> GetTrxHistory(int id)
        {
          if (_context.TrxHistory == null)
          {
              return NotFound();
          }
            var trxHistory = await _context.TrxHistory.FindAsync(id);

            if (trxHistory == null)
            {
                return NotFound();
            }

            return trxHistory;
        }

        // PUT: api/TrxHistory/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrxHistory(int id, TrxHistory trxHistory)
        {
            if (id != trxHistory.Id)
            {
                return BadRequest();
            }

            _context.Entry(trxHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrxHistoryExists(id))
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

        // POST: api/TrxHistory
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TrxHistory>> PostTrxHistory(TrxHistory trxHistory)
        {
          if (_context.TrxHistory == null)
          {
              return Problem("Entity set 'DatabasePmContext.TrxHistory'  is null.");
          }
            _context.TrxHistory.Add(trxHistory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrxHistory", new { id = trxHistory.Id }, trxHistory);
        }

        // DELETE: api/TrxHistory/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrxHistory(int id)
        {
            if (_context.TrxHistory == null)
            {
                return NotFound();
            }
            var trxHistory = await _context.TrxHistory.FindAsync(id);
            if (trxHistory == null)
            {
                return NotFound();
            }

            _context.TrxHistory.Remove(trxHistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrxHistoryExists(int id)
        {
            return (_context.TrxHistory?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
