using System.Collections.Generic;
using TheChat.Business.Entities;

namespace TheChat.Business.Interfaces.Repositories
{
    public interface IMessageRepository
    {
        public IEnumerable<Message> GetLastMessages(int chatId, int limit);
        public Message Create(int chatId, string messageContent, User sender);
        public Message Edit(int messageId, string messageContent);
        public bool Delete(int messageId);
    }
}
