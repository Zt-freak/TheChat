using System.Collections.Generic;
using TheChat.Business.Entities;
using System.Threading.Tasks;

namespace TheChat.Business.Interfaces.Repositories
{
    public interface IChatMemberRepository
    {
        public IEnumerable<ChatMember> GetAllMembers(int chatId);
        public Task<IEnumerable<ChatMember>> AddMember(int chatId, string userId);
        public IEnumerable<ChatMember> RemoveMember(int chatId, string userId);
        public ChatMember SetMod(int chatId, string userId, bool isMod);
        public IEnumerable<ChatMember> GetMods(int chatId);
    }
}
