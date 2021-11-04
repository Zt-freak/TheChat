using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        [ValidateAntiForgeryToken]
        [Route("register")]
        public ActionResult Register([FromBody] User user)
        {
            try
            {
                _service.SaveUser(user);
                return Ok();
            }
            catch (Exception)
            {
                return new BadRequestResult();
            }
        }
    }
}
