using Microsoft.AspNetCore.Identity;

namespace TheChat.Business.Entities
{
    public class User : IdentityUser
    {
        public bool? IsBanned { get; set; }
    }
}
