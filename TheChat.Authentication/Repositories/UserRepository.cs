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

        public User GetByUsername(string name)
        {
            return _datacontext.Users.Single(u => u.UserName == name);
        }

        public User GetByRole(string role)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SimpleUserData> GetByActivity(DateTime dateToCheck)
        {
            return _datacontext.Users
                .Where(u => u.LastActive >= dateToCheck)
                .Select(
                u => new SimpleUserData()
                    {
                        Id = u.Id,
                        Username = u.UserName
                    }
                );
        }

        public DateTime UpdateActivity(User user)
        {
            user.LastActive = DateTime.Now;
            _datacontext.SaveChanges();
            return (DateTime)user.LastActive;
        }
    }
}
