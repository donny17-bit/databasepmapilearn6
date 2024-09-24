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
    public class TrxFrfFileController : ControllerBase
    {
        private readonly DatabasePmContext _context;

        public TrxFrfFileController(DatabasePmContext context)
        {
            _context = context;
        }

        // GET: api/TrxFrfFile
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrxFrfFile>>> GetTrxFrfFile()
        {
          if (_context.TrxFrfFile == null)
          {
              return NotFound();
          }
            return await _context.TrxFrfFile.ToListAsync();
        }

        // GET: api/TrxFrfFile/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TrxFrfFile>> GetTrxFrfFile(int id)
        {
          if (_context.TrxFrfFile == null)
          {
              return NotFound();
          }
            var trxFrfFile = await _context.TrxFrfFile.FindAsync(id);

            if (trxFrfFile == null)
            {
                return NotFound();
            }

            return trxFrfFile;
        }

        // PUT: api/TrxFrfFile/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrxFrfFile(int id, TrxFrfFile trxFrfFile)
        {
            if (id != trxFrfFile.Id)
            {
                return BadRequest();
            }

            _context.Entry(trxFrfFile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrxFrfFileExists(id))
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

        // POST: api/TrxFrfFile
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TrxFrfFile>> PostTrxFrfFile(TrxFrfFile trxFrfFile)
        {
          if (_context.TrxFrfFile == null)
          {
              return Problem("Entity set 'DatabasePmContext.TrxFrfFile'  is null.");
          }
            _context.TrxFrfFile.Add(trxFrfFile);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrxFrfFile", new { id = trxFrfFile.Id }, trxFrfFile);
        }

        // DELETE: api/TrxFrfFile/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrxFrfFile(int id)
        {
            if (_context.TrxFrfFile == null)
            {
                return NotFound();
            }
            var trxFrfFile = await _context.TrxFrfFile.FindAsync(id);
            if (trxFrfFile == null)
            {
                return NotFound();
            }

            _context.TrxFrfFile.Remove(trxFrfFile);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrxFrfFileExists(int id)
        {
            return (_context.TrxFrfFile?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
