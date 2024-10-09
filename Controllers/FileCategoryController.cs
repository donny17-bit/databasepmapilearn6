using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using databasepmapilearn6.models;
using databasepmapilearn6.InputModels;
using databasepmapilearn6.Responses;
using databasepmapilearn6.Utilities;
using databasepmapilearn6.ViewModels;

namespace databasepmapilearn6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileCategoryController : ControllerBase
    {
        private readonly DatabasePmContext _context;

        public FileCategoryController(DatabasePmContext context)
        {
            _context = context;
        }

        // GET: api/FileCategory/dropdown
        [HttpGet("[action]")]
        public async Task<ActionResult> Dropdown([FromQuery] IMFileCategory.Dropdown input)
        {
            if (_context.MFileCategory == null) return Res.Failed("_context MFileCategory is null in dropdown FileCatergoryController");

            // validasi input
            if (!ModelState.IsValid) return Res.Failed(ModelState);

            // initialize log
            var logger = UtlLogger.Create(User.Identity.Name, $"{nameof(FileCategoryController)}/{nameof(Dropdown)}", UtlConverter.ObjectToJson(input));

            // query
            var query = _context.MFileCategory
                .Where(m => (!m.IsDeleted) &&
                    (input.file_types.Contains(m.FileTypeId)));

            // search
            if (!string.IsNullOrEmpty(input.Search))
            {
                query = query.Where(m => m.Name.ToLower().Contains(input.Search));
            }

            try
            {
                // get from database
                var fileCategorys = await query.Take(input.Show)
                    .Union(query.Where(m => input.already_ids.Contains(m.Id))) // ga tau buat apa
                    .OrderByDescending(m => m.Id)
                    .ToArrayAsync();

                // convert to dropdown
                var res = VMFileCategory.Dropdown.FromDb(fileCategorys);

                logger.Success();

                return ResDropdown.Success(res);
            }
            catch (System.Exception e)
            {
                return Res.Failed(logger, e);
            }
        }

        // GET: api/FileCategory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MFileCategory>>> GetMFileCategory()
        {
            if (_context.MFileCategory == null)
            {
                return NotFound();
            }
            return await _context.MFileCategory.ToListAsync();
        }

        // GET: api/FileCategory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MFileCategory>> GetMFileCategory(int id)
        {
            if (_context.MFileCategory == null)
            {
                return NotFound();
            }
            var mFileCategory = await _context.MFileCategory.FindAsync(id);

            if (mFileCategory == null)
            {
                return NotFound();
            }

            return mFileCategory;
        }

        // PUT: api/FileCategory/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMFileCategory(int id, MFileCategory mFileCategory)
        {
            if (id != mFileCategory.Id)
            {
                return BadRequest();
            }

            _context.Entry(mFileCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MFileCategoryExists(id))
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

        // POST: api/FileCategory
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MFileCategory>> PostMFileCategory(MFileCategory mFileCategory)
        {
            if (_context.MFileCategory == null)
            {
                return Problem("Entity set 'DatabasePmContext.MFileCategory'  is null.");
            }
            _context.MFileCategory.Add(mFileCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMFileCategory", new { id = mFileCategory.Id }, mFileCategory);
        }

        // DELETE: api/FileCategory/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMFileCategory(int id)
        {
            if (_context.MFileCategory == null)
            {
                return NotFound();
            }
            var mFileCategory = await _context.MFileCategory.FindAsync(id);
            if (mFileCategory == null)
            {
                return NotFound();
            }

            _context.MFileCategory.Remove(mFileCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MFileCategoryExists(int id)
        {
            return (_context.MFileCategory?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
