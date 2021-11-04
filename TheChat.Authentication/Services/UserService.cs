using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheChat.Business.Entities;
using TheChat.Business.Interfaces.Entities;
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

        public IUser GetUserById(int id)
        {
            return _repository.GetById(id);
        }

        public IUser GetUserByRole(string role)
        {
            return _repository.GetByRole(role);
        }

        public void RegisterUser(string userName, string email, string password)
        {
            User newUser = new();
            _repository.Add(newUser);
        }
    }
}
