using System.Collections.Generic;
using TheChat.Business.Entities;

namespace TheChat.Business.Interfaces.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
        User GetByName(string name);
        void Add(User user);
        void Update(User user);
        void Delete(int id);
        User GetByRole(string role);
    }
}
