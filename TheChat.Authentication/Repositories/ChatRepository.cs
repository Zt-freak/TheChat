using System;
using System.Collections.Generic;
using System.Linq;
using TheChat.Business.Entities;
using TheChat.Business.Interfaces.Repositories;

namespace TheChat.Business.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly ChatDbContext _datacontext;
        public ChatRepository(ChatDbContext datacontext)
        {
            _datacontext = datacontext;
        }

        public Chat Create(string title)
        {
            Chat newChat = new()
            {
                Title = title
            };
            _datacontext.Add(newChat);
            _datacontext.SaveChanges();

            return newChat;
        }

        public bool Delete(int chatId)
        {
            try
            {
                Chat yeetChat = _datacontext.Chats.Find(chatId);
                _datacontext.Remove(yeetChat);
                _datacontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Chat> GetAll()
        {
            return _datacontext.Chats;
        }

        public IEnumerable<Chat> GetAllFromUser(string userId)
        {
            return _datacontext.Chats
                .Join(
                    _datacontext.ChatMembers.Where(
                        cm => cm.User.Id == userId
                    ),
                    c => c,
                    cm => cm.Chat,
                    (c, u) => c
                );
        }

        public Chat GetById(int chatId)
        {
            return _datacontext.Chats.Find(chatId);
        }

        public Chat Rename(int chatId, string newTitle)
        {
            Chat renameChat = GetById(chatId);
            renameChat.Title = newTitle;
            _datacontext.SaveChanges();

            return renameChat;
        }
    }
}
