using CadContato.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CadContato.Domain.Repositories
{
    public interface IContatoRepository : IRepository<Contato>
    {
        IEnumerable<Contato> GetAllByMail(string email);

        IEnumerable<Contato> GetAll();

        IEnumerable<Contato> GetLike(string text);
    }
}
