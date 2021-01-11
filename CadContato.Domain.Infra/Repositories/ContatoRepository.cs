using CadContato.Domain.Entities;
using CadContato.Domain.Infra.Contexts;
using CadContato.Domain.Queries;
using CadContato.Domain.Repositories;
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
            return ctx.Contatos.AsNoTracking();
        }

        public IEnumerable<Contato> GetLike(string text)
        {
            return ctx.Contatos.Where(ContatoQueries.GetLike(text)).AsNoTracking();
            //.Include(x => x.Usuario) popular usuario
        }
    }
}
