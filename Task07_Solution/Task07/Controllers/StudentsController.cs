using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Task07.Model;

namespace Task07.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        public IConfiguration Configuration { get; set; }
        public StudentsController(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult GetStudents()
        {
            return Ok("secret string");
        }

        [HttpPost]
        public IActionResult Login(LoginRequestDTO request)
        {
            var claims = new[]
          {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Name, "John"),
                new Claim(ClaimTypes.Role, "admin"),
                new Claim(ClaimTypes.Role, "everyone")
            };

            var key = new SymmetricSecurityKey
                        (Encoding.UTF8.GetBytes(Configuration["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                issuer: "Asus",
                audience: "everyone",
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds

             );

            return Ok(new
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(token),
                refreshToken = Guid.NewGuid()
            }
                ); ;

        }

        [HttpPost("refresh-token/{token}")]
        public IActionResult RefreshToken(string requestToken)
        {

            return Ok();
        }
    }
}