using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using databasepmapilearn6.models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;
using databasepmapilearn6.ExtensionMethods;
using databasepmapilearn6.InputModels;
using databasepmapilearn6.ViewModels;
using databasepmapilearn6.Responses;

namespace databasepmapilearn6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [AllowAnonymous] //explicity tells that this controller don't need authentication
    [Authorize] //for now use authorization, because menu need claim data, ask later why in dbpmAPI use alloAnonymous and use claim
    public class MenuController : ControllerBase
    {
        private readonly DatabasePmContext _context;

        public MenuController(DatabasePmContext context)
        {
            _context = context;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> Dropdown([FromQuery] IMMenu.Dropdown input)
        {
            // check input is valid or not
            // return bad request if it's invalid 
            // without this method, the checking is still occurs behind the scene but the model will not know if it's an invalid data
            // in other word this used to return badrequest response if it's invalid
            if (!ModelState.IsValid) return Res.Failed(ModelState);

            // get claim
            var iClaim = IMClaim.FromUserClaim(User.Claims);

            // check role user
            if (iClaim.RoleId != 1 && iClaim.RoleId != 2) return Res.Failed("you don't have permission");

            // make query
            var query = _context.MMenus.Where(m => !m.IsDeleted);

            // search
            if (!string.IsNullOrEmpty(input.Search)) query = query.Where(m => m.Name.ToLower().Contains(input.Search));

            // get data menu
            // cari tau cara bacanya
            var menu = await query.Take(input.Show)
                .Union(query.Where(m => input.AlreadyIds.Contains(m.ID)))
                .OrderByDescending(m => m.ID)
                .ToArrayAsync();

            var vm = VMMenu.Dropdown.FromDb(menu);

            return ResDropdown.Success(vm);
        }

        // GET: api/Menu
        [HttpGet("getmenu")]
        public async Task<ActionResult<IEnumerable<MMenu>>> GetMMenus()
        {
            if (_context.MRoleMenu == null)
            {
                // change it later
                return Problem("MRoleMenu not found on the menu contoller");
            }

            // get role id from claim
            // this both code are the same approach to get the role id value
            // var RoleId = IMClaim.FromUserClaim(User.Claims).RoleId;
            var RoleId = HttpContext.User.Claims.GetRoleId();

            // get menu by role_id from db
            var RoleMenu = await _context.MRoleMenu
                .Include(m => m.Menu)
                .ThenInclude(m => m.Icon)
                .Where(m => (m.RoleId == RoleId) && (!m.Menu.IsDeleted)).ToListAsync();

            if (RoleMenu == null) return Res.NotFound("menu");

            var vm = VMRoleMenu.Menu.FromDb(RoleMenu);

            return Res.Success(vm);
        }
    }
}
