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

namespace databasepmapilearn6.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase 
    {
        private readonly DatabasePmContext _context;    
        private readonly ConfJwt _confJwt;
        public AuthController(DatabasePmContext context, IOptions<ConfJwt> optionsJwt)
        {
            _context = context;
            _confJwt = optionsJwt.Value;
        }

        // tanya kenapa harus di private dan ditaruh sini  
        // private const int CL_MAX_RETRY_COUNT = 2;

        // POST : api/auth
        [HttpPost]
        public async Task<ActionResult> Login([FromBody] IMAuth.Login mUser)
        {
            if (_context.MUser == null) return Problem("context MUser is null on Login AuthContoller");

            // check input is valid or not
            // return bad request if it's invalid 
            // without this method, the checking is still occurs behind the scene but the model will not know if it's an invalid data
            // in other word this used to return badrequest response if it's invalid
            if(!ModelState.IsValid) return BadRequest();

            // get data user from DB
            var user = await _context.MUser
            // .Include(m => m.Role)
            .Where(m => 
                // cari username
                (m.Username == mUser.Username) && 
                // akun tidak di delete
                (!m.IsDeleted))
            .SingleOrDefaultAsync(); 
            // nnti dibikin catatan dokumentasi perbedaan pake where atau ngga nya
            // .SingleOrDefaultAsync(m => m.Username == mUser.Username)

            // check user on the DB or not
            if (user == null) return BadRequest("user is not on the DB");

            // check if user is locked or not
            if (user.LockedUntil > DateTime.Now) return BadRequest("user is locked, please wait for a moment and try again later");

            // check password
            // password salah 
            if (mUser.Password != user.Password) {

                user.RetryCount += 1; // masih hardcode blm dibuat private variable

                // lock user jika sudah banyak percobaan
                if (user.RetryCount > 2 ) {
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
            var Jwt = UtlGenerator.Jwt(_confJwt, claim, 60);

            // save refersh token and access token to model auth
            var res = VMAuth.Login.Success(Jwt, RefreshToken);

            // update data user
            user.RetryCount = 0;
            user.LockedUntil = null;
            user.RefreshToken = RefreshToken;

            try
            {
                _context.MUser.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest($"Error on the Login right password API : {e}");
            }
            
            // create response object
            var response = new {
                user.Email,
                user.Username,
                jwt = res
            };

            return Ok(response);
        }
    }
}