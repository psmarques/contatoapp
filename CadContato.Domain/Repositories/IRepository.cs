using CadContato.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CadContato.Domain.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        void Create(T item);

        void Update(T item);

        void Delete(T item);

        T GetById(Guid id);

        IEnumerable<T> Get(Expression<Func<T, bool>> filter);
    }
}
