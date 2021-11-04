using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheChat.Business.Interfaces.Entities;

namespace TheChat.Business.Interfaces.Services
{
    public interface IUserService
    {
        void RegisterUser(string userName, string email, string password);
        public IUser GetUserById(int id);
        public IUser GetUserByRole(string role);
    }
}
