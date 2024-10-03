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
            // BadRequest("user role don't have menu");

            var res = VMRoleMenu.Menu.FromDb(RoleMenu);

            return Res.Success();
            // return Ok(res);
        }
    }
}
