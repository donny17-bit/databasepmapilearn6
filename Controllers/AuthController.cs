using databasepmapilearn6.models;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult> Login([FromBody] MUser mUser)
        {
            // lanjut bsk 
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}