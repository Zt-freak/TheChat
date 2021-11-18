using System.Collections.Generic;

namespace TheChat.TheApi.Models
{
    public class ChatMemberModel
    {
        public int ChatId { get; set; }
        public string[] AddMods { get; set; }
        public string[] RevokeMods { get; set; }
        public string[] AddMembers { get; set; }
        public string[] RemoveMembers { get; set; }
    }
}
