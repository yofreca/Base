using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILoginService _loginService;
        public TokenController(IConfiguration configuration, ILoginService loginService)
        {
            _configuration = configuration;
            _loginService = loginService;
        }


        [HttpPost]
        public async Task<IActionResult> Authentication(UserLoginDto login)
        {
            var validation = await IsValidUser(login);
            if (validation.Item1)
            {
                var token = GenerateToken(validation.Item2);
                return Ok(new {token});
            }

            return NotFound();
        }

        private async Task<(bool,Login)> IsValidUser(UserLoginDto login)
        {
            var user = await _loginService.GetLoginByCredentials(login);
            return (user!=null,user);
        }


        private string GenerateToken(Login login)
        {
            var symmetricSecurityKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, login.UserName),
                new Claim("UserId",login.UserId.ToString())
            };

            var payload = new JwtPayload(
                null,
                null,
                claims,
                DateTime.Now, 
                DateTime.Now.AddMinutes(10)
                );
            var token = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
