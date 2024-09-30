using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using databasepmapilearn6.models;
using databasepmapilearn6.Utilities;
using databasepmapilearn6.InputModels;
using Microsoft.AspNetCore.Authorization;


namespace databasepmapilearn6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly DatabasePmContext _context;

        public UserController(DatabasePmContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MUser>>> GetMUser()
        {
          if (_context.MUser == null)
          {
              return NotFound();
          }
            return await _context.MUser.ToListAsync();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MUser>> GetMUser(int id)
        {
          if (_context.MUser == null)
          {
              return NotFound();
          }
            var mUser = await _context.MUser.FindAsync(id);

            if (mUser == null)
            {
                return NotFound();
            }

            return mUser;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMUser(int id, [FromBody] IMUser.Edit input)
        {
            if (_context.MUser == null)
            {
                return Problem("Entity set 'DatabasePmContext.MUser' is null.");
            }

            // check input is valid or not
            // return bad request if it's invalid 
            // without this method, the checking is still occurs behind the scene but the model will not know if it's an invalid data
            // in other word this used to return badrequest response if it's invalid
            if(!ModelState.IsValid) return BadRequest();

            // get claim
            var iClaim = IMClaim.FromUserClaim(User.Claims);
            // check the role 
            if (iClaim.RoleId != 1 && iClaim.RoleId != 2) return BadRequest("user don't have permission to edit user");

            // ambil data user from DB
            var user = await _context.MUser.Where(m => (m.Id == id) && (!m.IsDeleted)).SingleOrDefaultAsync();

            // check if username exist on DB or not
            if (user == null) return BadRequest("user not found");

            // check role user
            if (user.RoleId == 1) return BadRequest("this user cannot be edit");

            // check username on the database
            if (user.Username != input.Username) {
                var username = await _context.MUser.Where(m => (m.Username == input.Username) && (!m.IsDeleted)).SingleOrDefaultAsync();
                if (username != null) return BadRequest("Username already exists");
            }
            
            // check email on the database
            if (user.Email != input.Email) {
                var email = await _context.MUser.Where(m => (m.Email == input.Email) && (!m.IsDeleted)).SingleOrDefaultAsync();
                if (email != null) return BadRequest("email already exists");
            }
            
            // edit user data
            user.RoleId = input.RoleId;
            user.PositionId = input.PositionId;
            user.Username = input.Username;
            user.Name = input.Name;
            user.Email = input.Email;
            user.UpdatedBy = iClaim.Id;
            user.UpdatedDate = DateTime.Now;

            var res = new {
                username = user.Username,
                message = "Success edit user"
            };
            
            try
            {   
                // update
                _context.MUser.Update(user);

                // commit
                await _context.SaveChangesAsync();
            }
            catch (System.Exception e)
            {
                return BadRequest($"Error occured in edit user in UserController : {e}");
            }

            return Ok(res);
        }

        // POST: api/User
        [HttpPost]
        public async Task<ActionResult<MUser>> PostUser([FromBody] IMUser.Create mUser)
        {
            if (_context.MUser == null)
            {
                return Problem("Entity set 'DatabasePmContext.MUser' is null.");
            }


            // validasi cek role user sebelum create akun user
            // User object auto generate from system security
            var iClaim = IMClaim.FromUserClaim(User.Claims);
            if (iClaim.RoleId != 1 && iClaim.RoleId != 2) return BadRequest("user don't have permission to create user");

            // generate random password
            var (rawPassword, hashedPassword) = UtlSecurity.GeneratePassword(16);

            var user = new MUser {
                RoleId = mUser.RoleId,
                PositionId = mUser.PositionId,
                Username = mUser.Username,
                Name = mUser.Name,
                Email = mUser.Email,
                Password = hashedPassword, 
                RetryCount = 0, 
                CreatedBy = iClaim.Id, 
                CreatedDate = DateTime.Now,
                IsDeleted = false
            };

            var res = new {
                roleId = mUser.RoleId,
                positionId = mUser.PositionId,
                username = mUser.Username,
                name = mUser.Name,
                email = mUser.Email,
                password = rawPassword
            };

            try {
                await _context.MUser.AddAsync(user);
                await _context.SaveChangesAsync();

                // when success return success code and send user information
                return Created("/api/user", res);
            }
            catch {  
                return BadRequest("Error on the API"); // change the error response later
            }
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMUser(int id)
        {
            if (_context.MUser == null)
            {
                return Problem("Entity set 'DatabasePmContext.MUser' is null.");
            }

            // get claim
            var iClaim = IMClaim.FromUserClaim(User.Claims);
            // check role id
            if (iClaim.RoleId != 1 && iClaim.RoleId != 2) return BadRequest("you don't have permission to delete user");

            // check MJobFile
            // belum ditambahkan 

            // get user data
            var user = await _context.MUser.Where(m => (m.Id == id) && (!m.IsDeleted)).SingleOrDefaultAsync();
            if (user == null) return BadRequest("User not found in the Database");

            // check target id 
            if (user.RoleId == 1 || user.RoleId == 2) return BadRequest("admin user cannot be deleted");

            user.IsDeleted = true;
            user.UpdatedBy = iClaim.Id;
            user.UpdatedDate = DateTime.Now;

            try
            {
                // save
                _context.MUser.Update(user);

                // commit
                await _context.SaveChangesAsync();
            }
            catch (System.Exception e)
            {
                
                return BadRequest($"Error on the delete User API : {e}");
            }

            return Ok("Success delete user");
        }

    }
}
