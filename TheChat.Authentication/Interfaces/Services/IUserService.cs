using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheChat.Business.Entities;

namespace TheChat.Business.Interfaces.Services
{
    public interface IUserService
    {
        void RegisterUser(string userName, string email, string password);
        public User GetUserById(int id);
        public User GetUserByRole(string role);
        void ValidateUser(string userName, string password);
    }
}
