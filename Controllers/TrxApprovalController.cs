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
    public class TrxApprovalController : ControllerBase
    {
        private readonly DatabasePmContext _context;

        public TrxApprovalController(DatabasePmContext context)
        {
            _context = context;
        }

        // GET: api/TrxApproval
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrxApproval>>> GetTrxApproval()
        {
          if (_context.TrxApproval == null)
          {
              return NotFound();
          }
            return await _context.TrxApproval.ToListAsync();
        }

        // GET: api/TrxApproval/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TrxApproval>> GetTrxApproval(int id)
        {
          if (_context.TrxApproval == null)
          {
              return NotFound();
          }
            var trxApproval = await _context.TrxApproval.FindAsync(id);

            if (trxApproval == null)
            {
                return NotFound();
            }

            return trxApproval;
        }

        // PUT: api/TrxApproval/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrxApproval(int id, TrxApproval trxApproval)
        {
            if (id != trxApproval.Id)
            {
                return BadRequest();
            }

            _context.Entry(trxApproval).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrxApprovalExists(id))
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

        // POST: api/TrxApproval
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TrxApproval>> PostTrxApproval(TrxApproval trxApproval)
        {
          if (_context.TrxApproval == null)
          {
              return Problem("Entity set 'DatabasePmContext.TrxApproval'  is null.");
          }
            _context.TrxApproval.Add(trxApproval);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrxApproval", new { id = trxApproval.Id }, trxApproval);
        }

        // DELETE: api/TrxApproval/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrxApproval(int id)
        {
            if (_context.TrxApproval == null)
            {
                return NotFound();
            }
            var trxApproval = await _context.TrxApproval.FindAsync(id);
            if (trxApproval == null)
            {
                return NotFound();
            }

            _context.TrxApproval.Remove(trxApproval);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrxApprovalExists(int id)
        {
            return (_context.TrxApproval?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
