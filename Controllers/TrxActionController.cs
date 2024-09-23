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
    public class TrxActionController : ControllerBase
    {
        private readonly DatabasePmContext _context;

        public TrxActionController(DatabasePmContext context)
        {
            _context = context;
        }

        // GET: api/TrxAction
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MTrxAction>>> GetMTrxAction()
        {
          if (_context.MTrxAction == null)
          {
              return NotFound();
          }
            return await _context.MTrxAction.ToListAsync();
        }

        // GET: api/TrxAction/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MTrxAction>> GetMTrxAction(int id)
        {
          if (_context.MTrxAction == null)
          {
              return NotFound();
          }
            var mTrxAction = await _context.MTrxAction.FindAsync(id);

            if (mTrxAction == null)
            {
                return NotFound();
            }

            return mTrxAction;
        }

        // PUT: api/TrxAction/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMTrxAction(int id, MTrxAction mTrxAction)
        {
            if (id != mTrxAction.Id)
            {
                return BadRequest();
            }

            _context.Entry(mTrxAction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MTrxActionExists(id))
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

        // POST: api/TrxAction
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MTrxAction>> PostMTrxAction(MTrxAction mTrxAction)
        {
          if (_context.MTrxAction == null)
          {
              return Problem("Entity set 'DatabasePmContext.MTrxAction'  is null.");
          }
            _context.MTrxAction.Add(mTrxAction);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMTrxAction", new { id = mTrxAction.Id }, mTrxAction);
        }

        // DELETE: api/TrxAction/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMTrxAction(int id)
        {
            if (_context.MTrxAction == null)
            {
                return NotFound();
            }
            var mTrxAction = await _context.MTrxAction.FindAsync(id);
            if (mTrxAction == null)
            {
                return NotFound();
            }

            _context.MTrxAction.Remove(mTrxAction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MTrxActionExists(int id)
        {
            return (_context.MTrxAction?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
