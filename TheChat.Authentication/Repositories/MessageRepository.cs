using System;
using System.Collections.Generic;
using System.Linq;
using TheChat.Business.Entities;
using TheChat.Business.Interfaces.Repositories;

namespace TheChat.Business.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ChatDbContext _datacontext;
        public MessageRepository(ChatDbContext datacontext)
        {
            _datacontext = datacontext;
        }

        public Message Create(int chatId, string messageContent, User sender)
        {
            Message newMessage = new()
            {
                Chat = _datacontext.Chats.Find(chatId),
                Content = messageContent,
                Sender = sender
            };
            _datacontext.Add(newMessage);
            _datacontext.SaveChanges();

            return newMessage;
        }

        public bool Delete(int messageId)
        {
            try
            {
                Message yeetMessage = _datacontext.Messages.Find(messageId);
                _datacontext.Remove(yeetMessage);
                _datacontext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Message Edit(int messageId, string messageContent)
        {
            Message editMessage = _datacontext.Messages.Find(messageId);
            editMessage.Content = messageContent;
            _datacontext.SaveChanges();

            return editMessage;
        }

        public IEnumerable<Message> GetLastMessages(int chatId, int limit)
        {
            return _datacontext.Messages
                .Where(m => m.Chat.Id == chatId)
                .OrderByDescending(m => m.Timestamp)
                .Take(limit);
        }
    }
}
