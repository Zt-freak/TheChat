using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public User GetUserById(int id)
        {
            return _repository.GetById(id);
        }

        public User GetUserByRole(string role)
        {
            return _repository.GetByRole(role);
        }

        public void RegisterUser(string userName, string email, string password)
        {
            if (_repository.GetAll().Any(x => x.UserName == userName))
                throw new Exception($"Username is already taken");

            User newUser = new();
            PasswordHasher<User> hasher = new();
            newUser.PasswordHash = hasher.HashPassword(newUser, password);
            newUser.Email = email;
            newUser.UserName = userName;

            _repository.Add(newUser);
        }

        public void ValidateUser(string userName, string password)
        {
            User user = _repository.GetByName(userName);
            PasswordHasher<User> hasher = new();

            switch (hasher.VerifyHashedPassword(user, user.PasswordHash, password))
            {
                case PasswordVerificationResult.Failed:
                    throw new Exception();
                case PasswordVerificationResult.Success:
                case PasswordVerificationResult.SuccessRehashNeeded:
                    break;
            }
        }
    }
}
