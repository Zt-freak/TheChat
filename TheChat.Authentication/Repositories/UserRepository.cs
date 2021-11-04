using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheChat.Business.Interfaces.Entities;
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

        public void Add(IUser user)
        {
            _datacontext.Add(user);
            _datacontext.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IUser> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public IUser GetById(int id)
        {
            return _datacontext.Users.Find(id);
        }

        public IUser GetByName(string name)
        {
            return _datacontext.Users.FirstOrDefault(u => u.UserName == name);
        }

        public IUser GetByRole(string role)
        {
            throw new NotImplementedException();
        }

        public void Update(IUser user)
        {
            throw new System.NotImplementedException();
        }
    }
}
