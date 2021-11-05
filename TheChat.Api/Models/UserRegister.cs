using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheChat.Api.Models
{
    public class UserRegister
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
