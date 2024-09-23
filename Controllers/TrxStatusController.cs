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
    public class TrxStatusController : ControllerBase
    {
        private readonly DatabasePmContext _context;

        public TrxStatusController(DatabasePmContext context)
        {
            _context = context;
        }

        // GET: api/TrxStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MTrxStatus>>> GetMTrxStatus()
        {
          if (_context.MTrxStatus == null)
          {
              return NotFound();
          }
            return await _context.MTrxStatus.ToListAsync();
        }

        // GET: api/TrxStatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MTrxStatus>> GetMTrxStatus(int id)
        {
          if (_context.MTrxStatus == null)
          {
              return NotFound();
          }
            var mTrxStatus = await _context.MTrxStatus.FindAsync(id);

            if (mTrxStatus == null)
            {
                return NotFound();
            }

            return mTrxStatus;
        }

        // PUT: api/TrxStatus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMTrxStatus(int id, MTrxStatus mTrxStatus)
        {
            if (id != mTrxStatus.Id)
            {
                return BadRequest();
            }

            _context.Entry(mTrxStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MTrxStatusExists(id))
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

        // POST: api/TrxStatus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MTrxStatus>> PostMTrxStatus(MTrxStatus mTrxStatus)
        {
          if (_context.MTrxStatus == null)
          {
              return Problem("Entity set 'DatabasePmContext.MTrxStatus'  is null.");
          }
            _context.MTrxStatus.Add(mTrxStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMTrxStatus", new { id = mTrxStatus.Id }, mTrxStatus);
        }

        // DELETE: api/TrxStatus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMTrxStatus(int id)
        {
            if (_context.MTrxStatus == null)
            {
                return NotFound();
            }
            var mTrxStatus = await _context.MTrxStatus.FindAsync(id);
            if (mTrxStatus == null)
            {
                return NotFound();
            }

            _context.MTrxStatus.Remove(mTrxStatus);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MTrxStatusExists(int id)
        {
            return (_context.MTrxStatus?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
