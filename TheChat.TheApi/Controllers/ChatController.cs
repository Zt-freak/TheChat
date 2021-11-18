using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TheChat.Business.Entities;
using TheChat.Business.Interfaces.Services;
using TheChat.TheApi.Models;

namespace TheChat.TheApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatController : Controller
    {
        private readonly IChatService _chatService;
        private readonly IUserService _userService;
        public ChatController(IChatService service, IUserService userService)
        {
            _chatService = service;
            _userService = userService;
        }

        [Authorize]
        [HttpPost]
        [Route("/getChat")]
        public async Task<IActionResult> GetChat(Chat inputChat)
        {
            string currentUserName = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            User currentUser = await _userService.GetUserByUsername(currentUserName);

            if (!_userService.GetRoles(currentUser).Result.Contains("Admin") && !_chatService.GetMods(inputChat.Id).Any(cm => cm.User == currentUser))
                return Unauthorized();

            return Ok(_chatService.Get(inputChat.Id));
        }

        [Authorize]
        [HttpGet]
        [Route("/getMyChats")]
        public async Task<IActionResult> GetMyChats()
        {
            string currentUserName = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            User currentUser = await _userService.GetUserByUsername(currentUserName);

            return Ok(_chatService.FetchUsersChats(currentUser.Id));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("/getUsersChats")]
        public IActionResult GetUsersChats(User user)
        {
            return Ok(_chatService.FetchUsersChats(user.Id));
        }

        [Authorize]
        [HttpPost]
        [Route("/createChat")]
        public async Task<IActionResult> CreateChat(Chat chat)
        {
            string currentUserName = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            User currentUser = await _userService.GetUserByUsername(currentUserName);

            Chat newChat = _chatService.CreateChat(chat.Title);
            _chatService.AddMember(newChat.Id, currentUser.Id);
            _chatService.MakeMod(newChat.Id, currentUser.Id);
            return Ok(newChat);
        }

        [Authorize]
        [HttpPost]
        [Route("/deleteChat")]
        public async Task<IActionResult> DeleteChat(Chat inputChat)
        {
            string currentUserName = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            User currentUser = await _userService.GetUserByUsername(currentUserName);

            if (!_userService.GetRoles(currentUser).Result.Contains("Admin") && !_chatService.GetMods(inputChat.Id).Any(cm => cm.User == currentUser))
                return Unauthorized();

            return Ok(_chatService.DeleteChat(inputChat.Id));
        }

        [Authorize]
        [HttpPost]
        [Route("/addMod")]
        public IActionResult AddMod(ChatMemberModel chatChad)
        {
            string currentUserName = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            User currentUser = _userService.GetUserByUsername(currentUserName).Result;

            if (!_userService.GetRoles(currentUser).Result.Contains("Admin") && !_chatService.GetMods(chatChad.ChatId).Any(cm => cm.User == currentUser))
                return Unauthorized();

            foreach (var chad in chatChad.AddMods)
            {
                _chatService.MakeMod(chatChad.ChatId, chad);
            }

            return Ok(_chatService.GetMods(chatChad.ChatId));
        }

        [Authorize]
        [HttpPost]
        [Route("/revokeMod")]
        public IActionResult RevokeMod(ChatMemberModel chatUnmod)
        {
            string currentUserName = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            User currentUser = _userService.GetUserByUsername(currentUserName).Result;

            if (!_userService.GetRoles(currentUser).Result.Contains("Admin") && !_chatService.GetMods(chatUnmod.ChatId).Any(cm => cm.User == currentUser))
                return Unauthorized();

            foreach (var chad in chatUnmod.RevokeMods)
            {
                _chatService.MakeMod(chatUnmod.ChatId, chad);
            }

            return Ok(_chatService.GetMods(chatUnmod.ChatId));
        }

        [Authorize]
        [HttpPost]
        [Route("/addMember")]
        public async Task<IActionResult> AddMember(ChatMemberModel addMembers)
        {
            string currentUserName = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            User currentUser = _userService.GetUserByUsername(currentUserName).Result;

            if (!_userService.GetRoles(currentUser).Result.Contains("Admin") && !_chatService.GetMods(addMembers.ChatId).Any(cm => cm.User == currentUser))
                return Unauthorized();

            _chatService.AddMember(addMembers.ChatId, addMembers.AddMembers[0]);

            return Ok(_chatService.GetMembers(addMembers.ChatId));
        }

        [Authorize]
        [HttpPost]
        [Route("/removeMember")]
        public IActionResult RemoveMember(ChatMemberModel removeMembers)
        {
            string currentUserName = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            User currentUser = _userService.GetUserByUsername(currentUserName).Result;

            if (!_userService.GetRoles(currentUser).Result.Contains("Admin") && !_chatService.GetMods(removeMembers.ChatId).Any(cm => cm.User == currentUser))
                return Unauthorized();

            foreach (var chad in removeMembers.RemoveMembers)
            {
                _chatService.RemoveMember(removeMembers.ChatId, chad);
            }

            return Ok(_chatService.GetMembers(removeMembers.ChatId));
        }
    }
}
