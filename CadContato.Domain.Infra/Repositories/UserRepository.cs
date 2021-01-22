using CadContato.Domain.Entities;
using CadContato.Domain.Infra.Contexts;
using CadContato.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CadContato.Domain.Infra.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DataContext ctx) : base(ctx)
        {
        }

        public User GetByEmail(string email)
        {
            return ctx.Users.FirstOrDefault(x => x.Email.Address == email);
        }
    }
}
