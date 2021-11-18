using System.Collections.Generic;
using System.Threading.Tasks;
using TheChat.Business.Entities;

namespace TheChat.Business.Interfaces.Services
{
    public interface IMessageService
    {
        public Task<bool> SendMessage(string messageContent);
        public Task<bool> EditMessage(int messageId, string messageContent);
        public Task<IList<Message>> FetchMessages(string messageContent);
        public Task<bool> DeleteMessage(int messageId);
    }
}
