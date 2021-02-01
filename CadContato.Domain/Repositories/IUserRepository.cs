using CadContato.Domain.Entities;
using CadContato.Domain.ValueObjects;

namespace CadContato.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByEmail(string email);

        User GetByEmail(Email email);
    }
}
