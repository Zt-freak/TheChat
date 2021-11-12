using Microsoft.AspNetCore.Identity;
using System;

namespace TheChat.Business.Entities
{
    public class User : IdentityUser
    {
        public bool? IsBanned { get; set; }
        public DateTime? LastActive { get; set; }
    }
}
