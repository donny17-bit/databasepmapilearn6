using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using databasepmapilearn6.models;
using Microsoft.AspNetCore.Authorization;
using databasepmapilearn6.InputModels;
using databasepmapilearn6.Responses;
using databasepmapilearn6.Utilities;
using databasepmapilearn6.ViewModels;

namespace databasepmapilearn6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JobTypeController : ControllerBase
    {
        private readonly DatabasePmContext _context;

        public JobTypeController(DatabasePmContext context)
        {
            _context = context;
        }

        // GET: api/jobtype/dropdown
        [HttpGet("[action]")]
        public async Task<ActionResult> Dropdown([FromQuery] IMJobType.Dropdown input)
        {
            if (_context.MJobType == null) return Res.Failed("_context MJobType is null in dropdown JobTypeController");

            // initialize log
            var logger = UtlLogger.Create(User.Identity.Name, $"{nameof(JobTypeController)}/{nameof(Dropdown)}", UtlConverter.ObjectToJson(input));

            // validasi input
            if (!ModelState.IsValid) return Res.Failed(ModelState);

            // get data from database 
            var query = _context.MJobType
                .Where(m => !m.IsDeleted);

            // search
            if (!string.IsNullOrEmpty(input.Search))
            {
                query = query.Where(m => m.Name.ToLower().Contains(input.Search));
            }

            try
            {
                var jobTypes = await _context.MJobType
                    .Take(input.Show)
                    .Union(query.Where(m => input.already_ids.Contains(m.Id))) // ga tau buat apa
                    .OrderByDescending(m => m.Id)
                    .ToArrayAsync();

                // convert to dropdown
                var res = VMJobType.Dropdown.FromDb(jobTypes);

                logger.Success();

                return ResDropdown.Success(res);
            }
            catch (System.Exception e)
            {

                return Res.Failed(logger, e);
            }
        }

        // GET: api/JobType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MJobType>>> GetMJobType()
        {
            if (_context.MJobType == null)
            {
                return NotFound();
            }
            return await _context.MJobType.ToListAsync();
        }

        // GET: api/JobType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MJobType>> GetMJobType(int id)
        {
            if (_context.MJobType == null)
            {
                return NotFound();
            }
            var mJobType = await _context.MJobType.FindAsync(id);

            if (mJobType == null)
            {
                return NotFound();
            }

            return mJobType;
        }

        // PUT: api/JobType/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMJobType(int id, MJobType mJobType)
        {
            if (id != mJobType.Id)
            {
                return BadRequest();
            }

            _context.Entry(mJobType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MJobTypeExists(id))
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

        // POST: api/JobType
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MJobType>> PostMJobType(MJobType mJobType)
        {
            if (_context.MJobType == null)
            {
                return Problem("Entity set 'DatabasePmContext.MJobType'  is null.");
            }
            _context.MJobType.Add(mJobType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMJobType", new { id = mJobType.Id }, mJobType);
        }

        // DELETE: api/JobType/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMJobType(int id)
        {
            if (_context.MJobType == null)
            {
                return NotFound();
            }
            var mJobType = await _context.MJobType.FindAsync(id);
            if (mJobType == null)
            {
                return NotFound();
            }

            _context.MJobType.Remove(mJobType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MJobTypeExists(int id)
        {
            return (_context.MJobType?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
