using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<User> _userManager;
        public UserController(IUserService service, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _service = service;
            _roleManager = roleManager;
            _userManager = userManager;
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

        [HttpPost]
        [Route("/updateRoles")]
        public async Task<IActionResult> Update(RoleEditModel model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (string userId in model.AddIds ?? new string[] { })
                {
                    User user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await _userManager.AddToRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                            return BadRequest(result);
                    }
                }
                foreach (string userId in model.DeleteIds ?? new string[] { })
                {
                    User user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
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
