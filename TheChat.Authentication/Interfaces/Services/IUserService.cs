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
        public User RegisterUser(string userName, string email, string password);
        public User GetUserById(int id);
        public User GetUserByUsername(string name);
        public IEnumerable<SimpleUserData> GetUsersByActivity(DateTime timeToCheck);
        public DateTime UpdateActivity(User user);
        public User GetUserByRole(string role);
        public bool ValidateUser(string userName, string password);
        public string GenerateJWT(string userName, string password);
    }
}
