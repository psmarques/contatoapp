using CadContato.Domain.Entities;
using CadContato.Domain.Infra.Contexts;
using CadContato.Domain.Repositories;
using CadContato.Domain.ValueObjects;
using System.Linq;

namespace CadContato.Domain.Infra.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DataContext ctx) : base(ctx)
        {
        }

        public User GetByEmail(string email)
        {
            //Não suporta string.Compare(... ordinalIgnoreCase);
            //Bug do EF Core forçar a obter o a query correta
            return ctx.Users.FirstOrDefault(x => x.Email.Address == email);
        }

        public User GetByEmail(Email email)
        {
            //Não suporta string.Compare(... ordinalIgnoreCase);
            //Bug do EF Core forçar a obter o a query correta
            return ctx.Users.FirstOrDefault(x => x.Email.Address == email.Address);
        }
    }
}
