using Library.Domain.Entities;
using System.Linq.Expressions;


namespace Library.Domain.Repositories
{
    public interface IRepositoryBase<TEntity, TKey> where TEntity : class, IEntity<TKey> where
        TKey : IComparable
    {
        public void Add(TEntity entity);    
        public void Edit(TEntity entityToUpdate);
        public void Remove(TEntity entityToRemove);
        public void Remove(TKey id);
        public IList<TEntity> GetAll();
        public TEntity GetById(TKey id);
        public int GetCount(Expression<Func<TEntity, bool>> filter = null);

        //Backup Code Not Used Here
        Task AddAsync(TEntity entity);
        Task EditAsync(TEntity entityToUpdate);
        Task<IList<TEntity>> GetAllAsync();
        Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null);
        void Remove(Expression<Func<TEntity, bool>> filter);
        Task RemoveAsync(Expression<Func<TEntity, bool>> filter);
        Task RemoveAsync(TEntity entityToDelete);
        Task RemoveAsync(TKey id);
    }
}
