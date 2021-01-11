using CadContato.Domain.Entities;
using CadContato.Domain.Infra.Contexts;
using CadContato.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CadContato.Domain.Infra.Repositories
{
    public abstract class GenericRepository<T> : IRepository<T> where T : Entity
    {
        protected readonly DataContext ctx;

        public bool AutoSaveChanges { get; set; }

        protected GenericRepository(DataContext ctx)
        {
            this.ctx = ctx;
            AutoSaveChanges = true;
        }

        public void Create(T item)
        {
            ctx.Set<T>().Add(item);

            if (AutoSaveChanges)
                ctx.SaveChanges();
        }

        public void Delete(T item)
        {
            ctx.Set<T>().Remove(item);

            if (AutoSaveChanges)
                ctx.SaveChanges();
        }

        public void Update(T item)
        {
            ctx.Entry<T>(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            if (AutoSaveChanges)
                ctx.SaveChanges();
        }

        public T GetById(Guid id)
        {
            return ctx.Set<T>().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter)
        {
            return ctx.Set<T>().Where(filter);
        }

        public void Save()
        {
            ctx.SaveChanges();
        }
    }
}
