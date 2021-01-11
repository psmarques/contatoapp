using CadContato.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CadContato.Domain.Queries
{
    public static class ContatoQueries
    {

        public static Expression<Func<Contato, bool>> GetLike(string text)
        {
            return x => x.Nome.PrimeiroNome.Contains(text) ||
                        x.Nome.UltimoNome.Contains(text);
        }
    }
}
