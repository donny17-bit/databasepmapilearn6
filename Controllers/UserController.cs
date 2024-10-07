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
using databasepmapilearn6.ViewModels;
using databasepmapilearn6.Responses;
using static databasepmapilearn6.Utilities.UtlEmail;
using databasepmapilearn6.ExtensionMethods;
using databasepmapilearn6.Domains.Utilities;
using static databasepmapilearn6.ExtensionMethods.ExtIQueryable;
using databasepmapilearn6.Enumerations;

namespace databasepmapilearn6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly DatabasePmContext _context;

        private readonly IUtlEmail _utlEmail;

        private readonly List<ColumnMapping> TABLE_COLUMN_MAPPING = new List<ColumnMapping>()
        {
            ColumnMapping.Create(nameof(VMUser.Table.username), "username", EnumDbdt.STRING),
            ColumnMapping.Create(nameof(VMUser.Table.name), "name", EnumDbdt.STRING),
            ColumnMapping.Create(nameof(VMUser.Table.email), "email", EnumDbdt.STRING),
            ColumnMapping.Create(nameof(VMUser.Table.position_code), "Position.Code", EnumDbdt.STRING),
            ColumnMapping.Create(nameof(VMUser.Table.position_name), "Position.Name", EnumDbdt.STRING),
            ColumnMapping.Create(nameof(VMUser.Table.role_name), "Role.Name", EnumDbdt.STRING)
        };

        public UserController(DatabasePmContext context, IUtlEmail utlEmail)
        {
            _context = context;
            _utlEmail = utlEmail;
        }


        [HttpGet("[action]")]
        public async Task<ActionResult<MUser>> Table([FromQuery] IMUser.Table input)
        {
            if (_context.MUser == null) return Problem("Entity set 'DatabasePmContext.MUser' is null.");

            // validasi input
            if (!ModelState.IsValid) return Res.Failed(ModelState);

            // get claim
            var iClaim = IMClaim.FromUserClaim(User.Claims);

            // validasi role user
            if (iClaim.RoleId != 1 && iClaim.RoleId != 2) return Res.Failed("you don't have permission");

            // base query
            var query = _context.MUser
                .Include(m => m.Role)
                .Include(m => m.Position)
                .Where(m => (m.Id != 1) && (!m.IsDeleted));


            // search 
            if (input.Search.Count > 0)
            {
                query = query.DynamicSearch(input.Search, TABLE_COLUMN_MAPPING);
            }

            // sort 
            if (input.Sort.Count > 0)
            {
                query = query.DynamicSort(input.Sort, TABLE_COLUMN_MAPPING);
            }
            else
            {
                query = query.OrderByDescending(m => m.Id);
            }

            // count filtered data
            var userCount = await query.CountAsync();

            // get data user
            var user = await query
            .SkipAndTake(input.Show, input.Page) // pagination
            .Include(m => m.Position) // position
            .Include(m => m.Role) // role
            .ToArrayAsync();

            var res = VMUser.Table.FromDb(user);

            return ResTable.Success(res, userCount);
        }

        // GET: api/User
        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<MUser>>> GetUserInfo()
        {
            if (_context.MUser == null)
            {
                // change it later
                return Problem("MUser not found on the user contoller");
            }

            // get claim
            var iClaim = IMClaim.FromUserClaim(User.Claims);

            // get user from db
            var user = await _context.MUser
                .Where(m => (m.Id == iClaim.Id) && (!m.IsDeleted))
                .Select(m => new MUser
                {
                    Id = m.Id,
                    RoleId = m.RoleId,
                    PositionId = m.PositionId,
                    Username = m.Username,
                    Name = m.Name,
                    Email = m.Email,
                    CreatedBy = m.CreatedBy,
                    CreatedDate = m.CreatedDate,
                    UpdatedBy = m.UpdatedBy,
                    UpdatedDate = m.UpdatedDate,
                    Role = new MRole
                    {
                        Id = m.Role.Id,
                        Name = m.Role.Name
                    },
                    Position = new MPosition
                    {
                        Id = m.Position.Id,
                        Name = m.Position.Name
                    }
                })
                .SingleOrDefaultAsync();

            // check if user null
            if (user == null) return Res.NotFound("user");

            var vm = VMUser.Detail.FromDb(user);

            return Res.Success(vm);
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MUser>> GetMUserDetail(int? id)
        {
            if (_context.MUser == null)
            {
                return NotFound();
            }

            // this id check is unneccessary
            // because when the ID is null, router will auto chose GetMUser method (route: api/User)
            // if(!id.HasValue) return BadRequest("please input user id");

            // get claim
            var iClaim = IMClaim.FromUserClaim(User.Claims);

            // check user role
            if (iClaim.RoleId != 1 && iClaim.RoleId != 2) return BadRequest("you don't have permission");

            // get user from db
            var user = await _context.MUser.Where(m => (m.Id == id) && (!m.IsDeleted)).SingleOrDefaultAsync();

            if (user == null) return BadRequest("user not found on the database");

            var res = VMUser.Detail.FromDb(user);

            return Ok(res);
        }

        // PUT: api/User/Edit/5
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] IMUser.Edit input)
        {
            if (_context.MUser == null)
            {
                return Problem("Entity set 'DatabasePmContext.MUser' is null.");
            }

            // ini tidak perlu karena jika id null akan mengarah ke action lain dan itu tidak ada methodnya
            // add check if id parameter null or not
            // if (!id.HasValue) return BadRequest("id is null");

            // check input is valid or not
            // return bad request if it's invalid 
            // without this method, the checking is still occurs behind the scene but the model will not know if it's an invalid data
            // in other word this used to return badrequest response if it's invalid
            if (!ModelState.IsValid) return BadRequest();

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
            if (user.Username != input.Username)
            {
                var username = await _context.MUser.Where(m => (m.Username == input.Username) && (!m.IsDeleted)).SingleOrDefaultAsync();
                if (username != null) return BadRequest("Username already exists");
            }

            // check email on the database
            if (user.Email != input.Email)
            {
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

            var res = new
            {
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
        public async Task<ActionResult<MUser>> Create([FromBody] IMUser.Create input)
        {
            if (_context.MUser == null)
            {
                return Problem("Entity set 'DatabasePmContext.MUser' is null.");
            }

            // validate input
            if (!ModelState.IsValid) return Res.Failed(ModelState);

            // log
            var logger = UtlLogger.Create(User.Identity.Name, $"{nameof(UserController)}/{nameof(Create)}", UtlConverter.ObjectToJson(input));

            // validasi cek role user sebelum create akun user
            // User object auto generate from system security
            var iClaim = IMClaim.FromUserClaim(User.Claims);
            if (iClaim.RoleId != 1 && iClaim.RoleId != 2) return BadRequest("user don't have permission to create user");

            // check username sudah pernah ada belum di DB
            // get user from DB
            var mUser = await _context.MUser.Where(m => (m.Username == input.Username) && (!m.IsDeleted)).SingleOrDefaultAsync();
            if (mUser != null) return Res.Failed("Username sudah ada di database");

            // generate random password
            var (rawPassword, hashedPassword) = UtlSecurity.GeneratePassword(8);

            // inisialisasi email
            // cari tau cara bacanya
            var imEmailMessage = new List<IMEmail.Message>();
            var imEmailAddressList = new List<IMEmail.Address> { IMEmail.Address.FromHardCode(input.Name, input.Email) };

            imEmailMessage.Add(
                IMEmail.Message.Create(
                    imEmailAddressList,
                    $"Pembuatan Akun {input.Username}",
                    new UtlEmailContentBuilder()
                        .Text($"kepada user {input.Name}")
                        .Enter(2)
                        .Text("Berikut adalah password untuk melakukan login : ")
                        .List(m => m
                            .ListItem(n => n.Text("Password : ").Text(rawPassword, isBold: true))
                        )
                        .Enter(2)
                        .Text("Dimohon untuk segera merubah password")
                        .Enter()
                        .Build()

                )
            );

            var user = new MUser
            {
                RoleId = input.role_id,
                PositionId = input.position_id,
                Username = input.Username,
                Name = input.Name,
                Email = input.Email,
                Password = hashedPassword,
                RetryCount = 0,
                CreatedBy = iClaim.Id,
                CreatedDate = DateTime.Now,
                IsDeleted = false
            };

            try
            {
                await _context.MUser.AddAsync(user);
                await _context.SaveChangesAsync();

                // send email
                _utlEmail.Send(logger, imEmailMessage);

                // log success
                // logger.Success(); // default logger
                logger.Success(rawPassword); // untuk sementara, karena password blm bisa dikirim ke email


                // when success return success code
                return Res.Success();
            }
            catch (Exception e)
            {
                return Res.Failed(logger, e);
            }
        }

        // DELETE: api/User/Delete/5
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Delete(int id)
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

        // PUT: api/User/resetPassword/5
        [HttpPut("reset-password/{id}")]
        public async Task<ActionResult> ResetPassword(int id)
        {
            if (_context.MUser == null)
            {
                return Problem("Entity set 'DatabasePmContext.MUser' is null.");
            }

            // get claim
            var iClaim = IMClaim.FromUserClaim(User.Claims);

            // check role id
            if (iClaim.RoleId != 1 && iClaim.RoleId != 2) return BadRequest("you don't have permission to reset password");

            // get user from database
            var user = await _context.MUser.Where(m => (m.Id == id) && (!m.IsDeleted)).SingleOrDefaultAsync();

            // check if user null or not
            if (user == null) return BadRequest($"user with ID: {id} is not found");

            // check if role user admin or not 
            if (user.RoleId == 1 || user.RoleId == 2) return BadRequest("admin cannot reset password");

            // generate password 
            var (rawPassword, hashedPassword) = UtlSecurity.GeneratePassword(16);

            user.Password = hashedPassword;
            user.UpdatedBy = iClaim.Id;
            user.UpdatedDate = DateTime.Now;

            var res = new
            {
                user.Username,
                password = rawPassword,
                message = "reset password success"
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
                return BadRequest($"Error on reset password User on UserController API : {e}");
            }

            return Ok(res);
        }
    }
}
