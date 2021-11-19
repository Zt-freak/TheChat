using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TheChat.Business.Entities;
using TheChat.Business.Interfaces.Services;

namespace TheChat.TheApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ChatController : Controller
    {
        private readonly IChatService _chatService;
        private readonly IUserService _userService;
        public ChatController(IChatService chatService, IUserService userService)
        {
            _chatService = chatService;
            _userService = userService;
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            string currentUserName = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            User currentUser = _userService.GetUserByUsername(currentUserName).Result;

            if (!_userService.GetRoles(currentUser).Result.Contains("Admin") && !_chatService.GetMods(id).Any(cm => cm.User == currentUser))
                return Unauthorized();

            Chat fetchedChat = _chatService.Get(id);

            if (fetchedChat == null)
            {
                return NotFound();
            }

            return Ok(fetchedChat);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(Chat inputChat)
        {
            string currentUserName = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            User currentUser = _userService.GetUserByUsername(currentUserName).Result;

            Chat newChat = _chatService.CreateChat(inputChat.Title);

            _chatService.AddMember(newChat.Id, currentUser.Id);
            _chatService.MakeMod(newChat.Id, currentUser.Id);

            return Ok(newChat);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            string currentUserName = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            User currentUser = _userService.GetUserByUsername(currentUserName).Result;

            if (!_userService.GetRoles(currentUser).Result.Contains("Admin") && !_chatService.GetMods(id).Any(cm => cm.User == currentUser))
                return Unauthorized();

            _chatService.DeleteChat(id);

            return Ok("chat deleted");
        }
    }
}
