using CadContato.Domain.Entities;
using CadContato.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CadContato.Tests.Mocks
{
    public class FakeContatoRepository : IContatoRepository
    {
        private ICollection<Contato> lst;

        public FakeContatoRepository()
        {
            lst = new List<Contato>();
        }

        public void Create(Contato item)
        {
            lst.Add(item);
        }

        public void Delete(Contato item)
        {
            lst.Remove(item);
        }

        public IEnumerable<Contato> Get(Expression<Func<Contato, bool>> filter)
        {
            return lst.AsQueryable().Where(filter);
        }

        public IEnumerable<Contato> GetAll()
        {
            return lst;
        }

        public Contato GetById(Guid id)
        {
            return lst.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Contato> GetLike(string text)
        {
            return lst.Where(x => x.Nome.PrimeiroNome.Contains(text));
        }

        public void Update(Contato item)
        {
            var i = lst.First(x => x.Id == item.Id);
            i = item;
        }
    }
}
