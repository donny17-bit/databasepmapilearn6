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
    public class FileTypeController : ControllerBase
    {
        private readonly DatabasePmContext _context;

        public FileTypeController(DatabasePmContext context)
        {
            _context = context;
        }

        // GET: api/FileType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MFileType>>> GetMFileType()
        {
          if (_context.MFileType == null)
          {
              return NotFound();
          }
            return await _context.MFileType.ToListAsync();
        }

        // GET: api/FileType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MFileType>> GetMFileType(int id)
        {
          if (_context.MFileType == null)
          {
              return NotFound();
          }
            var mFileType = await _context.MFileType.FindAsync(id);

            if (mFileType == null)
            {
                return NotFound();
            }

            return mFileType;
        }

        // PUT: api/FileType/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMFileType(int id, MFileType mFileType)
        {
            if (id != mFileType.Id)
            {
                return BadRequest();
            }

            _context.Entry(mFileType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MFileTypeExists(id))
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

        // POST: api/FileType
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MFileType>> PostMFileType(MFileType mFileType)
        {
          if (_context.MFileType == null)
          {
              return Problem("Entity set 'DatabasePmContext.MFileType'  is null.");
          }
            _context.MFileType.Add(mFileType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMFileType", new { id = mFileType.Id }, mFileType);
        }

        // DELETE: api/FileType/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMFileType(int id)
        {
            if (_context.MFileType == null)
            {
                return NotFound();
            }
            var mFileType = await _context.MFileType.FindAsync(id);
            if (mFileType == null)
            {
                return NotFound();
            }

            _context.MFileType.Remove(mFileType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MFileTypeExists(int id)
        {
            return (_context.MFileType?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
