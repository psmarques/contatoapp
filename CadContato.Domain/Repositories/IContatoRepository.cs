using CadContato.Domain.Entities;
using System.Collections.Generic;

namespace CadContato.Domain.Repositories
{
    public interface IContatoRepository : IRepository<Contato>
    {
        IEnumerable<Contato> GetAllByMail(string email);

        IEnumerable<Contato> GetAll();

        IEnumerable<Contato> GetLike(string text);
    }
}
