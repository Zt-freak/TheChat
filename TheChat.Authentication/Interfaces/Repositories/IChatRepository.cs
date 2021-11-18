using System.Collections.Generic;
using TheChat.Business.Entities;

namespace TheChat.Business.Interfaces.Repositories
{
    public interface IChatRepository
    {
        IEnumerable<Chat> GetAll();
        IEnumerable<Chat> GetAllFromUser(string userId);
        Chat GetById(int chatId);
        Chat Create(string title);
        Chat Rename(int chatId, string newTitle);
        bool Delete(int chatId);
    }
}
