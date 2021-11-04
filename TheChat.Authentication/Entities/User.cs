using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheChat.Business.Interfaces.Entities;

namespace TheChat.Business.Entities
{
    public class User : IdentityUser, IUser
    {
        public bool? IsBanned { get; set; }
    }
}
