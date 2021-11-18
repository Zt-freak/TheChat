using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TheChat.Business.Entities;
using TheChat.Business.Interfaces.Repositories;
using TheChat.Business.Interfaces.Services;

namespace TheChat.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;

        public UserService(IUserRepository repository, IConfiguration configuration, UserManager<User> userManager)
        {
            _repository = repository;
            _configuration = configuration;
            _userManager = userManager;
        }

        public Task<User> GetUserById(string id)
        {
            return _userManager.FindByIdAsync(id);
        }

        public Task<User> GetUserByUsername(string name)
        {
            return _userManager.FindByNameAsync(name);
        }

        public IEnumerable<SimpleUserData> GetUsersByActivity(DateTime timeToCheck)
        {
            return _repository.GetByActivity(timeToCheck);
        }

        public Task<IList<User>> GetUserByRole(string role)
        {
            return _userManager.GetUsersInRoleAsync(role);
        }

        public User RegisterUser(string userName, string email, string password)
        {
            if (_repository.GetAll().Any(x => x.UserName == userName))
                throw new Exception($"Username is already taken");

            User newUser = new();
            PasswordHasher<User> hasher = new();
            newUser.PasswordHash = hasher.HashPassword(newUser, password);
            newUser.Email = email;
            newUser.UserName = userName;
            newUser.IsBanned = false;

            _repository.Add(newUser);
            return newUser;
        }

        public bool ValidateUser(string userName, string password)
        {
            User user = _repository.GetByUsername(userName);
            PasswordHasher<User> hasher = new();

            return hasher.VerifyHashedPassword(user, user.PasswordHash, password) switch
            {
                PasswordVerificationResult.Success or PasswordVerificationResult.SuccessRehashNeeded => true,
                _ => false,
            };
        }

        public string GenerateJWT(string userName, string password)
        {
            if (ValidateUser(userName, password))
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                User currentUser = _repository.GetByUsername(userName);
                if ((bool)currentUser.IsBanned)
                    return String.Empty;

                var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, currentUser.UserName),
                    new Claim(JwtRegisteredClaimNames.Email, currentUser.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                  _configuration["Jwt:Issuer"],
                  claims,
                  expires: DateTime.Now.AddMinutes(120),
                  signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            return String.Empty;
        }

        public DateTime UpdateActivity(User user)
        {
            return _repository.UpdateActivity(user);
        }

        public Task<IList<string>> GetRoles(User user)
        {
            return _userManager.GetRolesAsync(user);
        }

        public Task<IdentityResult> AddRole(User user, string roleName)
        {
            return _userManager.AddToRoleAsync(user, roleName);
        }

        public Task<IdentityResult> RemoveRole(User user, string roleName)
        {
            return _userManager.RemoveFromRoleAsync(user, roleName);
        }
    }
}
