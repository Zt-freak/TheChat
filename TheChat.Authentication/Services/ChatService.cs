using System.Collections.Generic;
using TheChat.Business.Entities;
using TheChat.Business.Interfaces.Repositories;
using TheChat.Business.Interfaces.Services;

namespace TheChat.Business.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;
        private readonly IChatMemberRepository _chatMemberRepository;

        public ChatService(IChatRepository chatRepository, IChatMemberRepository chatMemberRepository)
        {
            _chatRepository = chatRepository;
            _chatMemberRepository = chatMemberRepository;
        }
        public bool AddMember(int chatId, string userId)
        {
            try { _chatMemberRepository.AddMember(chatId, userId); }
            catch
            {
                return false;
            }
            return true;
        }

        public Chat CreateChat(string title) => _chatRepository.Create(title);

        public bool DeleteChat(int chatId) => _chatRepository.Delete(chatId);

        public IList<Chat> FetchUsersChats(string userId) => (IList<Chat>)_chatRepository.GetAllFromUser(userId);

        public Chat Get(int chatId) => _chatRepository.GetById(chatId);

        public IEnumerable<ChatMember> GetMembers(int chatId) => _chatMemberRepository.GetAllMembers(chatId);

        public IEnumerable<ChatMember> GetMods(int chatId) => _chatMemberRepository.GetMods(chatId);

        public User MakeMod(int chatId, string userId) =>  _chatMemberRepository.SetMod(chatId, userId, true).User;

        public IList<ChatMember> RemoveMember(int chatId, string userId) => (IList<ChatMember>)_chatMemberRepository.RemoveMember(chatId, userId);

        public Chat Rename(int chatId, string newTitle) => _chatRepository.Rename(chatId, newTitle);

        public User RevokeMod(int chatId, string userId) => _chatMemberRepository.SetMod(chatId, userId, false).User;
    }
}
