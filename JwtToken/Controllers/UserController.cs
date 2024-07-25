using JwtToken.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtToken.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IConfiguration _config;
        private readonly CloneDbContext _context;

        public UserController(IConfiguration config, CloneDbContext context)
        {
            _config = config;
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserCred model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == model.username && x.Password == model.password);

            if (user != null)
            {

                var roleData = from ur in _context.UserRoles
                               join r in _context.Roles on ur.RoleId equals r.Id
                               where ur.UserId == user.Id
                               select r.Name;

                List<string> roles = roleData.ToList();
                var token = GenerateToken(user, roles);

                return Ok(new { Token = token });
            }

            return Unauthorized();
        }

        private string GenerateToken(User user, IList<string> roles)
        {
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["JwtSettings:SecretKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
        {
            new Claim("UserId",user.Id.ToString()),
            new Claim("Username",user.Username),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                issuer: _config["JwtSettings:Issuer"],
                audience: _config["JwtSettings:Audience"],
                 claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
