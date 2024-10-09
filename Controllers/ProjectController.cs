using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using databasepmapilearn6.models;
using databasepmapilearn6.Responses;
using databasepmapilearn6.InputModels;
using databasepmapilearn6.ViewModels;
using databasepmapilearn6.Utilities;
using Microsoft.AspNetCore.Authorization;

namespace databasepmapilearn6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        private readonly DatabasePmContext _context;

        public ProjectController(DatabasePmContext context)
        {
            _context = context;
        }

        // GET: api/project/dropdown
        [HttpGet("[action]")]
        public async Task<ActionResult<MProject>> Dropdown([FromQuery] IMProject.Dropdown input)
        {
            // initialize logger
            var logger = UtlLogger.Create(User.Identity.Name, $"{nameof(ProjectController)}/{nameof(Dropdown)}", UtlConverter.ObjectToJson(input));

            // validasi input
            if (!ModelState.IsValid) return Res.Failed(ModelState);

            try
            {
                // get from database
                var query = _context.MProjects.Where(m =>
                    (input.unit_ids.Contains(m.UnitId))
                    && (!m.IsDeleted));

                // search
                if (!string.IsNullOrEmpty(input.Search))
                {
                    query = query.Where(m => m.Name.ToLower().Contains(input.Search));
                }

                // query final
                var project = await query.Take(input.Show)
                    .Union(query.Where(m => input.already_ids.Contains(m.Id))) // ga tau ini buat apa 
                    .OrderByDescending(m => m.Id)
                    .ToArrayAsync();

                // ubah ke bentuk dropdown
                var res = VMProject.Dropdown.FromDb(project);

                return ResDropdown.Success(res);
            }
            catch (System.Exception e)
            {
                return Res.Failed(logger, e);
            }
        }

        // GET: api/Project
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MPosition>>> GetMPositions()
        {
            if (_context.MPositions == null)
            {
                return NotFound();
            }
            return await _context.MPositions.ToListAsync();
        }

        // GET: api/Project/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MPosition>> GetMPosition(int id)
        {
            if (_context.MPositions == null)
            {
                return NotFound();
            }
            var mPosition = await _context.MPositions.FindAsync(id);

            if (mPosition == null)
            {
                return NotFound();
            }

            return mPosition;
        }

        // PUT: api/Project/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMPosition(int id, MPosition mPosition)
        {
            if (id != mPosition.Id)
            {
                return BadRequest();
            }

            _context.Entry(mPosition).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MPositionExists(id))
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

        // POST: api/Project
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MPosition>> PostMPosition(MPosition mPosition)
        {
            if (_context.MPositions == null)
            {
                return Problem("Entity set 'DatabasePmContext.MPositions'  is null.");
            }
            _context.MPositions.Add(mPosition);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMPosition", new { id = mPosition.Id }, mPosition);
        }

        // DELETE: api/Project/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMPosition(int id)
        {
            if (_context.MPositions == null)
            {
                return NotFound();
            }
            var mPosition = await _context.MPositions.FindAsync(id);
            if (mPosition == null)
            {
                return NotFound();
            }

            _context.MPositions.Remove(mPosition);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MPositionExists(int id)
        {
            return (_context.MPositions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
