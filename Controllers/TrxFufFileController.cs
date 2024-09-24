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
    public class TrxFufFileController : ControllerBase
    {
        private readonly DatabasePmContext _context;

        public TrxFufFileController(DatabasePmContext context)
        {
            _context = context;
        }

        // GET: api/TrxFufFile
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrxFufFile>>> GetTrxFufFile()
        {
          if (_context.TrxFufFile == null)
          {
              return NotFound();
          }
            return await _context.TrxFufFile.ToListAsync();
        }

        // GET: api/TrxFufFile/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TrxFufFile>> GetTrxFufFile(int id)
        {
          if (_context.TrxFufFile == null)
          {
              return NotFound();
          }
            var trxFufFile = await _context.TrxFufFile.FindAsync(id);

            if (trxFufFile == null)
            {
                return NotFound();
            }

            return trxFufFile;
        }

        // PUT: api/TrxFufFile/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrxFufFile(int id, TrxFufFile trxFufFile)
        {
            if (id != trxFufFile.Id)
            {
                return BadRequest();
            }

            _context.Entry(trxFufFile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrxFufFileExists(id))
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

        // POST: api/TrxFufFile
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TrxFufFile>> PostTrxFufFile(TrxFufFile trxFufFile)
        {
          if (_context.TrxFufFile == null)
          {
              return Problem("Entity set 'DatabasePmContext.TrxFufFile'  is null.");
          }
            _context.TrxFufFile.Add(trxFufFile);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrxFufFile", new { id = trxFufFile.Id }, trxFufFile);
        }

        // DELETE: api/TrxFufFile/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrxFufFile(int id)
        {
            if (_context.TrxFufFile == null)
            {
                return NotFound();
            }
            var trxFufFile = await _context.TrxFufFile.FindAsync(id);
            if (trxFufFile == null)
            {
                return NotFound();
            }

            _context.TrxFufFile.Remove(trxFufFile);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrxFufFileExists(int id)
        {
            return (_context.TrxFufFile?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
