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
    public class TrxController : ControllerBase
    {
        private readonly DatabasePmContext _context;

        public TrxController(DatabasePmContext context)
        {
            _context = context;
        }

        // GET: api/Trx
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MTrx>>> GetMTrx()
        {
          if (_context.MTrx == null)
          {
              return NotFound();
          }
            return await _context.MTrx.ToListAsync();
        }

        // GET: api/Trx/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MTrx>> GetMTrx(int id)
        {
          if (_context.MTrx == null)
          {
              return NotFound();
          }
            var mTrx = await _context.MTrx.FindAsync(id);

            if (mTrx == null)
            {
                return NotFound();
            }

            return mTrx;
        }

        // PUT: api/Trx/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMTrx(int id, MTrx mTrx)
        {
            if (id != mTrx.Id)
            {
                return BadRequest();
            }

            _context.Entry(mTrx).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MTrxExists(id))
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

        // POST: api/Trx
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MTrx>> PostMTrx(MTrx mTrx)
        {
          if (_context.MTrx == null)
          {
              return Problem("Entity set 'DatabasePmContext.MTrx'  is null.");
          }
            _context.MTrx.Add(mTrx);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMTrx", new { id = mTrx.Id }, mTrx);
        }

        // DELETE: api/Trx/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMTrx(int id)
        {
            if (_context.MTrx == null)
            {
                return NotFound();
            }
            var mTrx = await _context.MTrx.FindAsync(id);
            if (mTrx == null)
            {
                return NotFound();
            }

            _context.MTrx.Remove(mTrx);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MTrxExists(int id)
        {
            return (_context.MTrx?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
