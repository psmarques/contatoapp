using CadContato.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CadContato.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByEmail(string email);
    }
}
