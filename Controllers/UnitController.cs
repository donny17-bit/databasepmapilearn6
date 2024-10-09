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
using databasepmapilearn6.ViewModels;

namespace databasepmapilearn6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UnitController : ControllerBase
    {
        private readonly DatabasePmContext _context;

        public UnitController(DatabasePmContext context)
        {
            _context = context;
        }

        // Get: api/Unit/dropdown
        [HttpGet("[action]")]
        public async Task<ActionResult<MUnit>> Dropdown([FromQuery] IMUnit.Dropdown input)
        {
            if (_context.MUnit == null) return Problem("Entity set 'DatabasePmContext.MUnit' is null in Dropdown UnitController.");

            // validasi input
            if (!ModelState.IsValid) return Res.Failed(ModelState);

            // cek role user 
            // nnti

            // get the data
            var units = await _context.MUnit.Where(m => !m.IsDeleted).ToArrayAsync();
            if (units == null) return Res.NotFound("unit");

            var unitsCount = _context.MUnit.Where(m => !m.IsDeleted).Count();

            // display to the view model
            var res = VMUnit.Dropdown.FromDb(units);

            return ResTable.Success(res, unitsCount);
        }

        // GET: api/Unit
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MUnit>>> GetMUnit()
        {
            if (_context.MUnit == null)
            {
                return NotFound();
            }
            return await _context.MUnit.ToListAsync();
        }

        // GET: api/Unit/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MUnit>> GetMUnit(int id)
        {
            if (_context.MUnit == null)
            {
                return NotFound();
            }
            var mUnit = await _context.MUnit.FindAsync(id);

            if (mUnit == null)
            {
                return NotFound();
            }

            return mUnit;
        }

        // PUT: api/Unit/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMUnit(int id, MUnit mUnit)
        {
            if (id != mUnit.Id)
            {
                return BadRequest();
            }

            _context.Entry(mUnit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MUnitExists(id))
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

        // POST: api/Unit
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MUnit>> PostMUnit(MUnit mUnit)
        {
            if (_context.MUnit == null)
            {
                return Problem("Entity set 'DatabasePmContext.MUnit'  is null.");
            }
            _context.MUnit.Add(mUnit);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMUnit", new { id = mUnit.Id }, mUnit);
        }

        // DELETE: api/Unit/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMUnit(int id)
        {
            if (_context.MUnit == null)
            {
                return NotFound();
            }
            var mUnit = await _context.MUnit.FindAsync(id);
            if (mUnit == null)
            {
                return NotFound();
            }

            _context.MUnit.Remove(mUnit);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MUnitExists(int id)
        {
            return (_context.MUnit?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
