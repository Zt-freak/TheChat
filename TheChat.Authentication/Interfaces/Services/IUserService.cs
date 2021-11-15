using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheChat.Business.Entities;

namespace TheChat.Business.Interfaces.Services
{
    public interface IUserService
    {
        public User RegisterUser(string userName, string email, string password);
        public Task<User> GetUserById(string id);
        public Task<User> GetUserByUsername(string name);
        public IEnumerable<SimpleUserData> GetUsersByActivity(DateTime timeToCheck);
        public DateTime UpdateActivity(User user);
        public Task<IList<User>> GetUserByRole(string role);
        public Task<IList<string>> GetRoles(User user);
        public Task<IdentityResult> AddRole(User user, string roleName);
        public Task<IdentityResult> RemoveRole(User user, string roleName);
        public bool ValidateUser(string userName, string password);
        public string GenerateJWT(string userName, string password);
    }
}
