using CadContato.Domain.Entities;
using CadContato.Domain.Infra.Contexts;
using CadContato.Domain.Queries;
using CadContato.Domain.Repositories;
using CadContato.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CadContato.Domain.Infra.Repositories
{
    public class ContatoRepository : GenericRepository<Contato>, IContatoRepository
    {
        public ContatoRepository(DataContext ctx) : base(ctx)
        {

        }

        public IEnumerable<Contato> GetAll()
        {
            return ctx.Contatos.Include(x => x.User).AsNoTracking();
        }

        public IEnumerable<Contato> GetAllByMail(string email)
        {
            //Bug no EF Core não suporta a expressao string.Compare(x, y, IgnoreCase)
            //Devemos explicitar a comparação das strings
            return ctx.Contatos.Where(x => email.ToUpper() == x.User.Email.Address.ToUpper()).Include(y => y.User).AsNoTracking();
        }

        public IEnumerable<Contato> GetLike(string text)
        {
            return ctx.Contatos.Where(ContatoQueries.GetLike(text)).Include(y => y.User).AsNoTracking();
        }
    }
}
