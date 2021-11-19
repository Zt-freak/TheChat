using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheChat.Business.Entities
{
    public class ChatMember
    {
        public int Id { get; set; }
        public int? ChatId { get; set; }
        [ForeignKey("ChatId")]
        public Chat Chat { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public bool Moderator { get; set; }
    }
}
