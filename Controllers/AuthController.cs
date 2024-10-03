using databasepmapilearn6.models;
using databasepmapilearn6.InputModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using databasepmapilearn6.Utilities;
using databasepmapilearn6.Constans;
using databasepmapilearn6.Configurations;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using databasepmapilearn6.ViewModels;
using Microsoft.AspNetCore.Authorization;
using databasepmapilearn6.Responses;

namespace databasepmapilearn6.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    // [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly DatabasePmContext _context;
        private readonly ConfJwt _confJwt;
        public AuthenticationController(DatabasePmContext context, IOptions<ConfJwt> optionsJwt)
        {
            _context = context;
            _confJwt = optionsJwt.Value;
        }

        // tanya kenapa harus di private dan ditaruh sini  
        // private const int CL_MAX_RETRY_COUNT = 2;

        // POST : api/auth
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] IMAuth.Login input)
        {
            if (_context.MUser == null) return Problem("context MUser is null on Login AuthContoller");

            // convert input object to json
            var inputJson = UtlConverter.ObjectToJson(input);

            // for logger
            var utlLogger = UtlLogger.Create(CDefault.Anonymous, $"{nameof(AuthenticationController)}/{nameof(Login)}", inputJson, false);


            // check input is valid or not
            // return bad request if it's invalid 
            // without this method, the checking is still occurs behind the scene but the model will not know if it's an invalid data
            // in other word this used to return badrequest response if it's invalid
            if (!ModelState.IsValid) return BadRequest();

            // konversi input ke json string
            // ini belum

            // get data user from DB
            var user = await _context.MUser
            .Include(m => m.Role)
            .Where(m =>
                // cari username
                (m.Username == input.Username) &&
                // akun tidak di delete
                (!m.IsDeleted))
            .SingleOrDefaultAsync();
            // nnti dibikin catatan dokumentasi perbedaan pake where atau ngga nya
            // .SingleOrDefaultAsync(m => m.Username == input.Username)

            // check user on the DB or not
            if (user == null) return BadRequest("user is not on the DB");

            // check if user is locked or not
            if (user.LockedUntil > DateTime.Now) return BadRequest("user is locked, please wait for a moment and try again later");

            // check password
            // password salah 
            if (!UtlSecurity.ValidatePassword(user.Password, input.Password))
            {

                user.RetryCount += 1; // masih hardcode blm dibuat private variable

                // lock user jika sudah banyak percobaan
                if (user.RetryCount > 2)
                {
                    // if (user.RetryCount > CL_MAX_RETRY_COUNT) {

                    user.LockedUntil = DateTime.Now.AddMinutes(5);
                }

                // update data user on DB
                try
                {
                    // _context.Entry(user).State = EntityState.Modified;
                    _context.MUser.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    BadRequest($"Error on the Login Wrong password API : {e}");
                }

                return BadRequest("Password not match");
            }

            // password benar //

            // create refresh token
            string RefreshToken = UtlGenerator.GenerateRandom(CDefault.TokenLength, CDefault.RandomCharRange);

            // create claim
            var claim = IMClaim.FromDb(user);

            // create jwt token
            string Jwt = UtlGenerator.Jwt(_confJwt, claim, 60);

            // save refresh token and access token to model auth
            var res = VMAuth.Login.Success(Jwt, RefreshToken);

            // update data user
            user.RetryCount = 0;
            user.LockedUntil = null;
            user.RefreshToken = RefreshToken;

            try
            {
                _context.MUser.Update(user);
                await _context.SaveChangesAsync();

                if (input.Username == null) return BadRequest();

                // log
                // to show log in the debug console
                utlLogger.Success(input.Username);
            }
            catch (Exception e)
            {
                // add failed request
                Res.Failed(utlLogger, e);
            }

            return Res.Success(res);
        }

        [Route("[action]")]
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> ChangePassword([FromBody] IMAuth.ChangePassword input)
        {
            if (_context.MUser == null) return Problem("context MUser is null on Login AuthContoller");

            // check input is valid or not
            // return bad request if it's invalid 
            // without this method, the checking is still occurs behind the scene but the model will not know if it's an invalid data
            // in other word this used to return badrequest response if it's invalid
            if (!ModelState.IsValid) return BadRequest();

            // ambil claim from user
            // cari tau User itu dari mana
            // User object auto generate from system security
            var iClaim = IMClaim.FromUserClaim(User.Claims);

            // get password from DB using id
            var user = await _context.MUser.Where(m => (m.Id == iClaim.Id) && (!m.IsDeleted)).SingleOrDefaultAsync();
            if (user == null) return Problem("user on changePassword AuthController is null");

            // validate old password
            bool validate = UtlSecurity.ValidatePassword(user.Password, input.OldPassword);

            // old password wrong
            if (!validate)
            {
                return BadRequest("Old Password is incorrect");
            }

            // old password correct
            string hashedNewPassword = UtlSecurity.HashedPassword(input.NewPassword);

            user.Password = hashedNewPassword;
            user.UpdatedBy = iClaim.Id;
            user.UpdatedDate = DateTime.Now;

            try
            {
                // update
                _context.MUser.Update(user);

                // commit
                await _context.SaveChangesAsync();
            }
            catch (System.Exception e)
            {
                return BadRequest($"Error on the ChangePassword AuthPassword API : {e}");
            }

            var response = new
            {
                user.Email,
                user.Username,
                Message = "change password success"
            };

            return Ok(response);
        }
    }
}