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
    public class TrxTypeController : ControllerBase
    {
        private readonly DatabasePmContext _context;

        public TrxTypeController(DatabasePmContext context)
        {
            _context = context;
        }

        // GET: api/TrxType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MTrxType>>> GetMTrxType()
        {
          if (_context.MTrxType == null)
          {
              return NotFound();
          }
            return await _context.MTrxType.ToListAsync();
        }

        // GET: api/TrxType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MTrxType>> GetMTrxType(int id)
        {
          if (_context.MTrxType == null)
          {
              return NotFound();
          }
            var mTrxType = await _context.MTrxType.FindAsync(id);

            if (mTrxType == null)
            {
                return NotFound();
            }

            return mTrxType;
        }

        // PUT: api/TrxType/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMTrxType(int id, MTrxType mTrxType)
        {
            if (id != mTrxType.Id)
            {
                return BadRequest();
            }

            _context.Entry(mTrxType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MTrxTypeExists(id))
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

        // POST: api/TrxType
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MTrxType>> PostMTrxType(MTrxType mTrxType)
        {
          if (_context.MTrxType == null)
          {
              return Problem("Entity set 'DatabasePmContext.MTrxType'  is null.");
          }
            _context.MTrxType.Add(mTrxType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMTrxType", new { id = mTrxType.Id }, mTrxType);
        }

        // DELETE: api/TrxType/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMTrxType(int id)
        {
            if (_context.MTrxType == null)
            {
                return NotFound();
            }
            var mTrxType = await _context.MTrxType.FindAsync(id);
            if (mTrxType == null)
            {
                return NotFound();
            }

            _context.MTrxType.Remove(mTrxType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MTrxTypeExists(int id)
        {
            return (_context.MTrxType?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
