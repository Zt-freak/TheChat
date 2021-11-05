using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheChat.Business.Entities;
using TheChat.Business.Interfaces.Repositories;

namespace TheChat.Business.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ChatDbContext _datacontext;

        public UserRepository(ChatDbContext datacontext)
        {
            _datacontext = datacontext;
        }

        public void Add(User user)
        {
            _datacontext.Add(user);
            _datacontext.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            return _datacontext.Users;
        }

        public User GetById(int id)
        {
            return _datacontext.Users.Find(id);
        }

        public User GetByName(string name)
        {
            return _datacontext.Users.FirstOrDefault(u => u.UserName == name);
        }

        public User GetByRole(string role)
        {
            throw new NotImplementedException();
        }

        public void Update(User user)
        {
            throw new System.NotImplementedException();
        }
    }
}
