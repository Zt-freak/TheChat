using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TheChat.Business.Entities;
using TheChat.Business.Interfaces.Services;
using TheChat.TheApi.Models;

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
        [Route("online")]
        public IActionResult GetOnlineUsers()
        {
            string username = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "sub").Value;
            User currentUser = _service.GetUserByUsername(username).Result;
            _service.UpdateActivity(currentUser);

            return Ok(_service.GetUsersByActivity(DateTime.Now.AddSeconds(-10)));
        }

        [HttpPost]
        [Route("get")]
        public async Task<IActionResult> GetUser(User inputUser)
        {
            var user = _service.GetUserById(inputUser.Id).Result;
            var roles = await _service.GetRoles(user);

            return Ok(new { User = user, Roles = roles });
        }

        [HttpPost]
        [Route("updateRoles")]
        public async Task<IActionResult> UpdateRoles(RoleEditModel model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (string userId in model.AddIds ?? Array.Empty<string>())
                {
                    User user = await _service.GetUserById(userId);
                    if (user != null)
                    {
                        result = await _service.AddRole(user, model.RoleName);
                        if (!result.Succeeded)
                            return BadRequest(result);
                    }
                }
                foreach (string userId in model.DeleteIds ?? Array.Empty<string>())
                {
                    User user = await _service.GetUserById(userId);
                    if (user != null)
                    {
                        result = await _service.RemoveRole(user, model.RoleName);
                        if (!result.Succeeded)
                            BadRequest(result);
                    }
                }
            }

            if (ModelState.IsValid)
                return Ok();
            else
                return BadRequest();
        }
    }
}
