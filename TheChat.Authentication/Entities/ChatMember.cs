using System.ComponentModel.DataAnnotations;

namespace TheChat.Business.Entities
{
    public class ChatMember
    {
        public int Id { get; set; }
        public Chat Chat { get; set; }
        public User User { get; set; }
        public bool Moderator { get; set; }
    }
}
