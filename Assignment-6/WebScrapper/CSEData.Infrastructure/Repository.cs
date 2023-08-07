using CSEData.Domain.Repositories;
using CSEData.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CSEData.Infrastructure
{
    public abstract class Repository<TEntity, TKey>
        : IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        protected DbContext _dbContext;
        protected DbSet<TEntity> _dbSet;
        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }
        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public IList<TEntity> GetAll()
        {
            IQueryable<TEntity> query = _dbSet;
            return query.ToList();
        }

        public TEntity GetById(TKey id)
        {
            return _dbSet.Find(id);
        }

        public void Remove(TEntity entityToDelete)
        {
            if (_dbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }

        public void Remove(TKey id)
        {
            var entityToDelete = _dbSet.Find(id);
            Remove(entityToDelete);
        }
    }
}
