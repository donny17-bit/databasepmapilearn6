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
    public class ApprovalDetailController : ControllerBase
    {
        private readonly DatabasePmContext _context;

        public ApprovalDetailController(DatabasePmContext context)
        {
            _context = context;
        }

        // GET: api/ApprovalDetail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MApprovalDetail>>> GetMApprovalDetails()
        {
          if (_context.MApprovalDetails == null)
          {
              return NotFound();
          }
            return await _context.MApprovalDetails.ToListAsync();
        }

        // GET: api/ApprovalDetail/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MApprovalDetail>> GetMApprovalDetail(int id)
        {
          if (_context.MApprovalDetails == null)
          {
              return NotFound();
          }
            var mApprovalDetail = await _context.MApprovalDetails.FindAsync(id);

            if (mApprovalDetail == null)
            {
                return NotFound();
            }

            return mApprovalDetail;
        }

        // PUT: api/ApprovalDetail/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMApprovalDetail(int id, MApprovalDetail mApprovalDetail)
        {
            if (id != mApprovalDetail.Id)
            {
                return BadRequest();
            }

            _context.Entry(mApprovalDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MApprovalDetailExists(id))
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

        // POST: api/ApprovalDetail
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MApprovalDetail>> PostMApprovalDetail(MApprovalDetail mApprovalDetail)
        {
          if (_context.MApprovalDetails == null)
          {
              return Problem("Entity set 'DatabasePmContext.MApprovalDetails'  is null.");
          }
            _context.MApprovalDetails.Add(mApprovalDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMApprovalDetail", new { id = mApprovalDetail.Id }, mApprovalDetail);
        }

        // DELETE: api/ApprovalDetail/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMApprovalDetail(int id)
        {
            if (_context.MApprovalDetails == null)
            {
                return NotFound();
            }
            var mApprovalDetail = await _context.MApprovalDetails.FindAsync(id);
            if (mApprovalDetail == null)
            {
                return NotFound();
            }

            _context.MApprovalDetails.Remove(mApprovalDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MApprovalDetailExists(int id)
        {
            return (_context.MApprovalDetails?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
