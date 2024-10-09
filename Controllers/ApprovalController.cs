using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using databasepmapilearn6.models;
using Microsoft.AspNetCore.Authorization;
using databasepmapilearn6.Responses;

namespace databasepmapilearn6.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ApprovalController : ControllerBase
    {
        private readonly DatabasePmContext _context;

        public ApprovalController(DatabasePmContext context)
        {
            _context = context;
        }

        // GET: api/Approval
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MApproval>>> GetMApprovals()
        {
            if (_context.MApprovals == null)
            {
                return NotFound();
            }
            return await _context.MApprovals.ToListAsync();
        }

        // GET: api/Approval/5
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<MApproval>> GetApprovalId(int id)
        {
            if (_context.MApprovalDetails == null)
            {
                return Problem("Entity set 'DatabasePmContext.MApprovalDetails' is null in GetApprovalId ApprovalController.");
            }

            // get from database
            var approvals = await _context.MApprovalDetails
                .Where(m => (m.ApprovalId == id) && (!m.IsDeleted))
                .Select(m => new MApprovalDetail { })
                .ToArrayAsync();

            if (approvals == null)
            {
                return Res.NotFound("approval");
            }

            return Res.Success();
        }

        // PUT: api/Approval/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMApproval(int id, MApproval mApproval)
        {
            if (id != mApproval.Id)
            {
                return BadRequest();
            }

            _context.Entry(mApproval).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MApprovalExists(id))
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

        // POST: api/Approval
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MApproval>> PostMApproval(MApproval mApproval)
        {
            if (_context.MApprovals == null)
            {
                return Problem("Entity set 'DatabasePmContext.MApprovals'  is null.");
            }
            _context.MApprovals.Add(mApproval);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMApproval", new { id = mApproval.Id }, mApproval);
        }

        // DELETE: api/Approval/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMApproval(int id)
        {
            if (_context.MApprovals == null)
            {
                return NotFound();
            }
            var mApproval = await _context.MApprovals.FindAsync(id);
            if (mApproval == null)
            {
                return NotFound();
            }

            _context.MApprovals.Remove(mApproval);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MApprovalExists(int id)
        {
            return (_context.MApprovals?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
