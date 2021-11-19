using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheChat.Business.Entities;
using TheChat.Business.Interfaces.Repositories;

namespace TheChat.Business.Repositories
{
    public class ChatMemberRepository : IChatMemberRepository
    {
        private readonly ChatDbContext _datacontext;
        private readonly UserManager<User> _userManager;
        public ChatMemberRepository(ChatDbContext datacontext, UserManager<User> userManager)
        {
            _datacontext = datacontext;
            _userManager = userManager;
        }

        public IEnumerable<ChatMember> AddMember(int chatId, string userId)
        {
            ChatMember newMember = new()
            {
                ChatId = chatId,
                UserId = userId,
                Moderator = false
            };

            _datacontext.Add(newMember);
            _datacontext.SaveChanges();

            return GetAllMembers(chatId);
        }

        public IEnumerable<ChatMember> GetAllMembers(int chatId) => _datacontext.ChatMembers.Where(m => m.Chat.Id == chatId);

        public ChatMember SetMod(int chatId, string userId, bool isMod)
        {
            ChatMember member = _datacontext.ChatMembers
                .Where(m => m.Chat.Id == chatId)
                .Where(m => m.User.Id == userId)
                .SingleOrDefault();

            member.Moderator = isMod;
            _datacontext.SaveChanges();

            return member;
        }

        public IEnumerable<ChatMember> RemoveMember(int chatId, string userId)
        {
            ChatMember member = _datacontext.ChatMembers
                .Where(m => m.Chat.Id == chatId)
                .Where(m => m.User.Id == userId)
                .SingleOrDefault();
            _datacontext.Remove(member);
            _datacontext.SaveChanges();

            return GetAllMembers(chatId);
        }

        public IEnumerable<ChatMember> GetMods(int chatId) => GetAllMembers(chatId).Where(cm => cm.Moderator);
    }
}
