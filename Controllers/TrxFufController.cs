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
using Microsoft.AspNetCore.Authorization;
using databasepmapilearn6.Utilities;
using databasepmapilearn6.ExtensionMethods;
using databasepmapilearn6.ViewModels;
using static databasepmapilearn6.ExtensionMethods.ExtIQueryable;

namespace databasepmapilearn6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TrxFufController : ControllerBase
    {
        private readonly DatabasePmContext _context;

        private readonly List<ColumnMapping> columMappings = new List<ColumnMapping> {
            // nama variable harus sama antara view model dengan model asli (VMTrxFuf dengan TrxFuf) (case sensitif)
            ColumnMapping.Create(nameof(VMTrxFuf.Table.id), "id", Enumerations.EnumDbdt.INT),
            ColumnMapping.Create(nameof(VMTrxFuf.Table.fuf_number), "FufNumber", Enumerations.EnumDbdt.STRING),
            ColumnMapping.Create(nameof(VMTrxFuf.Table.status_name), "TrxStatus.Name", Enumerations.EnumDbdt.STRING),
            ColumnMapping.Create(nameof(VMTrxFuf.Table.unit_name), "Unit.Name", Enumerations.EnumDbdt.STRING),
            ColumnMapping.Create(nameof(VMTrxFuf.Table.year), "year", Enumerations.EnumDbdt.INT),
            ColumnMapping.Create(nameof(VMTrxFuf.Table.project_name), "Project.Name", Enumerations.EnumDbdt.STRING),
            ColumnMapping.Create(nameof(VMTrxFuf.Table.job_type_name), "JobType.Name", Enumerations.EnumDbdt.STRING),
        };

        public TrxFufController(DatabasePmContext context)
        {
            _context = context;
        }

        // GET: api/trxfuf/table
        [HttpGet("[action]")]
        public async Task<ActionResult> Table([FromQuery] IMTrxFuf.Table input)
        {
            if (_context.TrxFuf == null) return Res.Failed("Entity set 'DatabasePmContext.TrxFuf' is null in Table TrxFufController.");

            // validate input
            if (!ModelState.IsValid) return Res.Failed(ModelState);

            // initialize log
            var logger = UtlLogger.Create(User.Identity.Name, $"{nameof(TrxFufController)}/{nameof(Table)}", UtlConverter.ObjectToJson(input));

            // get claim
            var iClaim = IMClaim.FromUserClaim(User.Claims);

            // base query
            var query = _context.TrxFuf
                .Include(m => m.TrxStatus)
                .Include(m => m.Unit)
                .Include(m => m.Project)
                .Include(m => m.JobType)
                .Where(m => (m.CreatedBy == iClaim.Id) && (!m.IsDeleted));

            // search
            if (input.Search.Count > 0)
            {
                query = query.DynamicSearch(input.Search, columMappings);
            }

            // sort
            if (input.Sort.Count > 0)
            {
                // do sorting
                query = query.DynamicSort(input.Sort, columMappings);
            }
            else
            {
                query = query.OrderByDescending(m => m.Id);
            }

            try
            {
                // get data from database
                var trxFufs = await query
                    .SkipAndTake(input.Show, input.Page)
                    .ToArrayAsync();

                // total data
                var trxFufsCount = await query.CountAsync();

                // convert to table 
                var res = VMTrxFuf.Table.FromDb(trxFufs);

                // log
                logger.Success();

                return ResTable.Success(res, trxFufsCount);
            }
            catch (System.Exception e)
            {
                return Res.Failed(logger, e);
            }
        }


        // GET: api/TrxFuf/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TrxFuf>> GetTrxFuf(int id)
        {
            if (_context.TrxFuf == null)
            {
                return NotFound();
            }
            var trxFuf = await _context.TrxFuf.FindAsync(id);

            if (trxFuf == null)
            {
                return NotFound();
            }

            return trxFuf;
        }

        // PUT: api/TrxFuf/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrxFuf(int id, TrxFuf trxFuf)
        {
            if (id != trxFuf.Id)
            {
                return BadRequest();
            }

            _context.Entry(trxFuf).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrxFufExists(id))
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

        // POST: api/TrxFuf
        [HttpPost]
        public async Task<ActionResult<TrxFuf>> PostTrxFuf(TrxFuf trxFuf)
        {
            if (_context.TrxFuf == null)
            {
                return Problem("Entity set 'DatabasePmContext.TrxFuf'  is null.");
            }
            _context.TrxFuf.Add(trxFuf);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrxFuf", new { id = trxFuf.Id }, trxFuf);
        }

        // DELETE: api/TrxFuf/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrxFuf(int id)
        {
            if (_context.TrxFuf == null)
            {
                return NotFound();
            }
            var trxFuf = await _context.TrxFuf.FindAsync(id);
            if (trxFuf == null)
            {
                return NotFound();
            }

            _context.TrxFuf.Remove(trxFuf);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrxFufExists(int id)
        {
            return (_context.TrxFuf?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
