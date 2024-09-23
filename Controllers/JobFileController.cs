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
    public class JobFileController : ControllerBase
    {
        private readonly DatabasePmContext _context;

        public JobFileController(DatabasePmContext context)
        {
            _context = context;
        }

        // GET: api/JobFile
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MJobFile>>> GetMJobFile()
        {
          if (_context.MJobFile == null)
          {
              return NotFound();
          }
            return await _context.MJobFile.ToListAsync();
        }

        // GET: api/JobFile/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MJobFile>> GetMJobFile(int id)
        {
          if (_context.MJobFile == null)
          {
              return NotFound();
          }
            var mJobFile = await _context.MJobFile.FindAsync(id);

            if (mJobFile == null)
            {
                return NotFound();
            }

            return mJobFile;
        }

        // PUT: api/JobFile/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMJobFile(int id, MJobFile mJobFile)
        {
            if (id != mJobFile.Id)
            {
                return BadRequest();
            }

            _context.Entry(mJobFile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MJobFileExists(id))
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

        // POST: api/JobFile
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MJobFile>> PostMJobFile(MJobFile mJobFile)
        {
          if (_context.MJobFile == null)
          {
              return Problem("Entity set 'DatabasePmContext.MJobFile'  is null.");
          }
            _context.MJobFile.Add(mJobFile);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMJobFile", new { id = mJobFile.Id }, mJobFile);
        }

        // DELETE: api/JobFile/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMJobFile(int id)
        {
            if (_context.MJobFile == null)
            {
                return NotFound();
            }
            var mJobFile = await _context.MJobFile.FindAsync(id);
            if (mJobFile == null)
            {
                return NotFound();
            }

            _context.MJobFile.Remove(mJobFile);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MJobFileExists(int id)
        {
            return (_context.MJobFile?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
