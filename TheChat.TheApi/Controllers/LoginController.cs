using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TheChat.Business.Entities;
using TheChat.Business.Interfaces.Services;
using TheChat.TheApi.Models;

namespace TheChat.TheApi.Controllers
{
    [ApiController]
    public class LoginController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IUserService _service;
        public LoginController(IConfiguration config, IUserService service)
        {
            _config = config;
            _service = service;
        }

        [Route("login")]
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserModel login)
        {
            IActionResult response = Unauthorized();

            if (AuthenticateUser(login))
            {
                var tokenString = GenerateJSONWebToken(login);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        private string GenerateJSONWebToken(UserModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var user = _service.GetUserByUsername(userInfo.Username);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private bool AuthenticateUser(UserModel login)
        {
            return _service.ValidateUser(login.Username, login.Password);
        }

        [HttpPost]
        [Route("register")]
        public ActionResult Register([FromBody] UserModel registration)
        {
            try
            {
                User newUser = _service.RegisterUser(registration.Username, registration.Email, registration.Password);
                return Ok();
            }
            catch (Exception)
            {
                return new BadRequestResult();
            }
        }
    }
}
