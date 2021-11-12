using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheChat.Api.Models;
using TheChat.Business.Entities;
using TheChat.Business.Interfaces.Services;

namespace TheChat.Api.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("register")]
        public ActionResult Register([FromBody] UserRegister registration)
        {
            try
            {
                User newUser = _service.RegisterUser(registration.UserName, registration.Email, registration.Password);
                return Ok();
            }
            catch (Exception e)
            {
                return new BadRequestResult();
            }
        }

        [HttpPost]
        [Route("validateUser")]
        public ActionResult Validate([FromBody] UserRegister registration)
        {
            try
            {
                bool isValid = _service.ValidateUser(registration.UserName, registration.Password);
                return Ok();
            }
            catch (Exception e)
            {
                return new BadRequestResult();
            }
        }

        [HttpPost]
        [Route("getJWT")]
        public ActionResult GetJWT([FromBody] UserLogin login)
        {
            try
            {
                if (_service.ValidateUser(login.UserName, login.Password))
                {
                    string JWT = _service.GenerateJWT(login.UserName, login.Password);
                    return Ok(JWT);
                }
                return new BadRequestResult();
            }
            catch (Exception e)
            {
                return new BadRequestResult();
            }
        }
    }
}
