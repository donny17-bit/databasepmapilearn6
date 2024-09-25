using databasepmapilearn6.models;
using databasepmapilearn6.InputModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace databasepmapilearn6.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase 
    {
        private readonly DatabasePmContext _context;
        public AuthController(DatabasePmContext context)
        {
            _context = context;
        }

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
            
            // create refresh token (class blm dibuat)
            
            // update data user
            user.RetryCount = 0;
            user.LockedUntil = null;

            try
            {
                _context.MUser.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest($"Error on the Login right password API : {e}");
            }
            
            return Ok("Sukses Login");
        }
    }
}