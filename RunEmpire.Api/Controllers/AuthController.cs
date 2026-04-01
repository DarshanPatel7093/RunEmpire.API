using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunEmpire.Data;
using RunEmpire.Services;

namespace RunEmpire.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly JwtService _jwt;

        public AuthController(AppDbContext context, JwtService jwt)
        {
            _context = context;
            _jwt = jwt;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string name, string email, string password)
        {
            if (await _context.Users.AnyAsync(x => x.Email == email))
                return BadRequest(new { errorCode = 400, errorMessage = "Email already exists" });

            var user = new User
            {
                Name = name,
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { token = _jwt.Generate(user) });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return Unauthorized(new { errorCode =401 ,errorMessage = "Invalid credentials" });

            return Ok(new { token = _jwt.Generate(user) });
        }

    }
}
