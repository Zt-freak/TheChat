using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheChat.Business.Entities;

namespace TheChat.Business.Interfaces.Services
{
    public interface IChatService
    {
        public IList<Chat> FetchUsersChats(string userId);
        public Chat Get(int chatId);
        public Chat CreateChat(string title);
        public bool DeleteChat(int chatId);
        public IEnumerable<ChatMember> GetMembers(int chatId);
        public bool AddMember(int chatId, string userId);
        public IList<ChatMember> RemoveMember(int chatId, string userId);
        public User MakeMod(int chatId, string userId);
        public User RevokeMod(int chatId, string userId);
        public Chat Rename(int chatId, string newTitle);
        public IEnumerable<ChatMember> GetMods(int chatId);
    }
}
