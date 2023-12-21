using FDB_backend.Data;
using FDB_backend.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FDB_backend.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public AuthController(AppDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            // Validate credentials
            var user = await _dbContext.Users
                .Where(u => u.Email == request.Email)
                .FirstOrDefaultAsync();

            if (user == null || !(await VerifyPassword(request.Password, user.PasswordHash)))
            {
                return Unauthorized(new { Message = "Invalid email or password" });
            }

            // Generate JWT token
            var token = GenerateJwtToken(user);

            return Ok(new { Token = token });
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            // Validate input
            if (await _dbContext.Users.AnyAsync(u => u.Email == request.Email))
            {
                return BadRequest(new { Message = "Email is already registered" });
            }

            // Hash password
            var passwordHash = HashPassword(request.Password);

            // Create new user
            var newUser = new User
            {
                Email = request.Email,
                PasswordHash = passwordHash,
                // Additional fields as needed
            };

            // Save user to the database
            _dbContext.Users.Add(newUser);
            await _dbContext.SaveChangesAsync();

            return Ok(new { Message = "User registered successfully" });
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email)
                    // Additional claims as needed
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                                                            SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string HashPassword(string password)
        {
            // Implement a secure password hashing algorithm (e.g., bcrypt)
            // For simplicity, this example uses a basic hash (not recommended for production)
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
        }

        private async Task<bool> VerifyPassword(string inputPassword, string hashedPassword)
        {
            // Implement password verification logic
            // For simplicity, this example uses a basic comparison (not recommended for production)
            return await Task.FromResult(inputPassword == Encoding.UTF8.GetString(Convert.FromBase64String(hashedPassword)));
        }
    }
}
