using Microsoft.EntityFrameworkCore;
using SharedCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace FestiSpec.SharedCode.Repositories
{
    class FileLinkRepository
    {
        internal FestispecContext context;
        internal DbSet<FileLink> dbSet;

        public FileLinkRepository(FestispecContext context)
        {
            this.context = context;
            this.dbSet = context.Set<FileLink>();
        }

        public virtual IEnumerable<FileLink> Get(Expression<Func<FileLink, bool>> filter = null, Func<IQueryable<FileLink>, IOrderedQueryable<FileLink>> orderBy = null, string includeProperties = "")
        {
            IQueryable<FileLink> query = dbSet;

            query = query.Where(f => f.DeletedAt != null);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual void Insert(FileLink entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(FileLink entityToDelete)
        {
            entityToDelete.Delete();
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }            
        }

        public virtual void Update(FileLink entityToUpdate)
        {
            entityToUpdate.UpdatedAt = DateTime.Now;
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}