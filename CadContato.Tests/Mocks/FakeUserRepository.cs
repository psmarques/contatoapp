using CadContato.Domain.Entities;
using CadContato.Domain.Repositories;
using CadContato.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CadContato.Tests.Mocks
{
    public class FakeUserRepository : IUserRepository
    {
        private readonly List<User> lst;

        public FakeUserRepository()
        {
            lst = new List<User>();
        }

        public void Create(User item)
        {
            lst.Add(item);
        }

        public void Delete(User item)
        {
            lst.Remove(item);
        }

        public IEnumerable<User> Get(Expression<Func<User, bool>> filter)
        {
            return lst;
        }

        public User GetByEmail(string email)
        {
            return lst.FirstOrDefault(x => x.Email.Address == email);
        }

        public User GetByEmail(Email email)
        {
            return lst.FirstOrDefault(x => x.Email.Address == email.Address);
        }

        public User GetById(Guid id)
        {
            return lst.FirstOrDefault(x => x.Id == id);
        }

        public void Update(User item)
        {
            var i = lst.FirstOrDefault(x => x.Id == item.Id);
            i = item;
        }
    }
}
