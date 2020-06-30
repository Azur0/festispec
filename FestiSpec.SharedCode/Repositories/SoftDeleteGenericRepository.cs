using Microsoft.EntityFrameworkCore;
using SharedCode.Models;
using SharedCode.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace FestiSpec.SharedCode.Repositories
{
    public class SoftDeleteGenericRepository<TEntity> : GenericRepository<TEntity> where TEntity : class
    {
        public SoftDeleteGenericRepository(FestispecContext context) : base(context)
        {
        }

        public override void Delete(TEntity entityToDelete)
        {
            if (entityToDelete is ISoftDeletable Deletable)
            {
                Deletable.Delete();
                if (context.Entry(entityToDelete).State == EntityState.Detached)
                {
                    dbSet.Attach(entityToDelete);
                }
            }
        }       

        public override IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            if (filter != null)
            {
                var compiled = filter.Compile();
                filter = o => compiled(o) && (o as ISoftDeletable).DeletedAt == null;
            }
            return base.Get(filter, orderBy, includeProperties);
        }

        public override IEnumerable<TEntity> GetPaginable(int pageAmount, int currentPage, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            if (filter != null)
            {
                var compiled = filter.Compile();
                filter = o => compiled(o) && (o as ISoftDeletable).DeletedAt == null;
            }
            return base.GetPaginable(pageAmount, currentPage, filter, orderBy, includeProperties);
        }

        public override IEnumerable<TEntity> GetIncludes(string children, Expression<Func<TEntity, bool>> filter = null)
        {
            if (filter != null)
            {
                var compiled = filter.Compile();
                filter = o => compiled(o) && (o as ISoftDeletable).DeletedAt == null;
            }
            return base.GetIncludes(children, filter);
        }

        public override void Update(TEntity entityToUpdate)
        {
            (entityToUpdate as ISoftDeletable).UpdatedAt = DateTime.Now;
            base.Update(entityToUpdate);
        }
    }
}
