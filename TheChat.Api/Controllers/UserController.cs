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
                _service.RegisterUser(registration.UserName, registration.Email, registration.Password);
                return Ok();
            }
            catch (Exception e)
            {
                return new BadRequestResult();
            }
        }

        [HttpPost]
        [Route("validate")]
        public ActionResult Validate([FromBody] UserRegister registration)
        {
            try
            {
                _service.ValidateUser(registration.UserName, registration.Password);
                return Ok();
            }
            catch (Exception e)
            {
                return new BadRequestResult();
            }
        }
    }
}
