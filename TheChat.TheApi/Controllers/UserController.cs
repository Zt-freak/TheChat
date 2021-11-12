using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using TheChat.Business.Entities;
using TheChat.Business.Interfaces.Services;

namespace TheChat.TheApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        [Route("/online")]
        public IActionResult GetOnlineUsers()
        {
            string username = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "sub").Value;
            User currentUser = _service.GetUserByUsername(username);
            _service.UpdateActivity(currentUser);

            return Ok(_service.GetUsersByActivity(DateTime.Now.AddSeconds(-10)));
        }
    }
}
