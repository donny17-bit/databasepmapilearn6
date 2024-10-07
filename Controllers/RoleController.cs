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
using databasepmapilearn6.ViewModels;
using databasepmapilearn6.Responses;

namespace databasepmapilearn6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoleController : ControllerBase
    {
        private readonly DatabasePmContext _context;

        public RoleController(DatabasePmContext context)
        {
            _context = context;
        }

        // GET : /api/role/dropdown?
        [HttpGet("[action]")]
        public async Task<IActionResult> Dropdown([FromQuery] IMRole.Dropdown input)
        {
            if (_context.MRole == null) return Problem("Entity set 'DatabasePmContext.MRole' is null.");

            // validaasi input 
            if (!ModelState.IsValid) return Res.Failed(ModelState);

            // get claim
            var iClaim = IMClaim.FromUserClaim(User.Claims);

            // base query
            var query = _context.MRole.Where(m => (m.Id != 1) && (!m.IsDeleted));

            // search
            if (!string.IsNullOrEmpty(input.Search))
            {
                query = query.Where(m => m.Name.ToLower().Contains(input.Search.ToLower()));
            }

            // get the data from database
            var role = await query
                .Take(input.Show)
                .Union(query.Where(m => input.AlreadyIds.Contains(m.Id)))
                .OrderByDescending(m => m.Id)
                .ToArrayAsync();

            var res = VMRole.Dropdown.FromDb(role);

            return ResDropdown.Success(res);
        }


        // GET: api/Role/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MRole>> GetMRole(int? id)
        {
            if (_context.MRole == null)
            {
                return NotFound();
            }

            // pastikan id ada
            // ngga terlalu pengaruh ketika di ada/ngga
            // if (!id.HasValue) return BadRequest("role diperlukan");

            // get role id current user
            var RoleId = IMClaim.FromUserClaim(User.Claims).RoleId;

            if (RoleId != 1 && RoleId != 2) return BadRequest("you don't have permission to access");

            var mRole = await _context.MRole
                // user
                .Include(m => m.Users)
                // menu & icon
                .Include(m => m.RoleMenus).ThenInclude(m => m.Menu).ThenInclude(m => m.Icon)
                // filter
                .Where(m => (m.Id == id) && (!m.IsDeleted))
                .SingleOrDefaultAsync();

            if (mRole == null) return BadRequest("Role not found in the database");

            var res = VMRole.Detail.FromDb(mRole);

            return Ok(res);
        }

        // PUT: api/Role/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMRole(int id, IMRole.EditRole input)
        {
            if (_context.MRole == null) return Problem("Entity set 'DatabasePmContext.MRole' is null in PutMRole RoleController.");

            // check input is valid or not
            // return bad request if it's invalid 
            // without this method, the checking is still occurs behind the scene but the model will not know if it's an invalid data
            // in other word this used to return badrequest response if it's invalid
            if (!ModelState.IsValid) return BadRequest();

            // get claim (user info)
            var iClaim = IMClaim.FromUserClaim(User.Claims);

            // validate user
            if (iClaim.RoleId != 1 && iClaim.RoleId != 2) return BadRequest("you don't have permission to edit role");

            // get role from the DB
            var RoleName = await _context.MRole
                .Include(m => m.RoleMenus).ThenInclude(m => m.Menu).ThenInclude(m => m.Icon)
                .Where(m => (m.Id == id) && (!m.IsDeleted))
                .SingleOrDefaultAsync();

            if (RoleName == null) return BadRequest("Role not found in the database");

            // change role name
            RoleName.Name = input.Name;
            RoleName.UpdatedBy = iClaim.Id;
            RoleName.UpdatedDate = DateTime.Now;

            // get all menu id from database
            List<int> MenuIdList = RoleName.RoleMenus.Select(m => m.MenuId).ToList();

            // edit menu id
            var RoleMenus = RoleName.RoleMenus.Where(m => !input.MenuId.Contains(m.MenuId));

            // update 
            // blm tau cara bacanya
            RoleName.RoleMenus.Where(m => input.MenuId.Contains(m.MenuId)).Select(m =>
            {
                int insert = input.MenuId.Single(n => n == m.MenuId);
                return m;
            }).ToList();

            // insert
            // lanjut nnti


            Console.WriteLine($"isi dari RoleMenus : {RoleMenus}");

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return NoContent();
        }

        // POST: api/Role
        [HttpPost("[action]")]
        public async Task<ActionResult<MRole>> Create([FromBody] IMRole.CreateRole input)
        {
            if (_context.MRole == null)
            {
                return Problem("Entity set 'DatabasePmContext.MRole' is null.");
            }

            // check input is valid or not
            // return bad request if it's invalid 
            // without this method, the checking is still occurs behind the scene but the model will not know if it's an invalid data
            // in other word this used to return badrequest response if it's invalid
            if (!ModelState.IsValid) return BadRequest();

            // get claim
            var iClaim = IMClaim.FromUserClaim(User.Claims);

            // cek role current user
            if (iClaim.RoleId != 1 && iClaim.RoleId != 2) return BadRequest("you don't have permission to create role");

            // check in the DB if the name already exists or not
            var RoleName = await _context.MRole.Where(m => (m.Name == input.Name) && (!m.IsDeleted)).SingleOrDefaultAsync();
            if (RoleName != null) return BadRequest("Role name already exists in the DB");

            var mRole = new MRole
            {
                Name = input.Name,
                // add role menu masih bingung bacanya
                RoleMenus = new List<MRoleMenu>(
                    input.MenuId.Select(m => new MRoleMenu
                    {
                        MenuId = m
                    }).ToList()
                ),
                // bingung sampai sini
                CreatedBy = iClaim.Id,
                CreatedDate = DateTime.Now,
                IsDeleted = false
            };

            try
            {
                await _context.MRole.AddAsync(mRole);
                await _context.SaveChangesAsync();
            }
            catch (System.Exception e)
            {
                return BadRequest($"Error on create role in RoleController : {e}");
            }

            return Ok(input);
        }

        // DELETE: api/Role/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.MRole == null)
            {
                return Problem("Entity set 'DatabasePmContext.MRole' is null.");
            }

            // get current user role 
            var iClaim = IMClaim.FromUserClaim(User.Claims);
            if (iClaim.RoleId != 1 && iClaim.RoleId != 2) return BadRequest("you don't have permission to delete role");

            // get role from DB
            var RoleName = await _context.MRole.Where(m => (m.Id == id) && (!m.IsDeleted)).SingleOrDefaultAsync();
            if (RoleName == null) return BadRequest("Role not found on the Database");

            // modify is delete
            RoleName.UpdatedBy = iClaim.Id;
            RoleName.UpdatedDate = DateTime.Now;
            RoleName.IsDeleted = true;

            try
            {
                // save
                _context.MRole.Update(RoleName);

                // commit
                await _context.SaveChangesAsync();
            }
            catch (System.Exception e)
            {
                return BadRequest($"Error on create role in RoleController : {e}");
            }

            return Ok($"Success delele role dengan ID : {id}");
        }

        private bool MRoleExists(int id)
        {
            return (_context.MRole?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
