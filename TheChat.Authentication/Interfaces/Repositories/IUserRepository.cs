using System;
using System.Collections.Generic;
using TheChat.Business.Entities;

namespace TheChat.Business.Interfaces.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
        User GetByUsername(string name);
        IEnumerable<SimpleUserData> GetByActivity(DateTime dateToCheck);
        void Add(User user);
        void Delete(int id);
        User GetByRole(string role);
        DateTime UpdateActivity(User user);
    }
}
