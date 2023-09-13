using Crud.Domain.Entities;
using Crud.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;

namespace Crud.Persistance
{
    public abstract class Repository<TEntity, TKey> 
        : IRepository<TEntity, TKey> where TKey : IComparable
        where TEntity : class, IEntity<TKey>
    {
        protected  DbContext _dbContext;
        protected  DbSet<TEntity> _dbSet;
      
        public Repository(DbContext dbContext)
        {
           
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>(); 
        }
        public virtual void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }
        public virtual void Remove(TKey id)
        {
            var entityToDelete = _dbSet.Find(id);
            Remove(entityToDelete);
        }
        public virtual void Remove(TEntity entityToDelete)
        {
            if (_dbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }
        //used for GetPagedUserAsync in service page
        public virtual (IList<TEntity> data, int total, int totalDisplay) GetDynamic(
        Expression<Func<TEntity, bool>> filter = null,
        string orderBy = null,
        string includeProperties = "", int pageIndex = 1, int pageSize = 10, bool isTrackingOff = false)
        {
            IQueryable<TEntity> query = _dbSet;
            var total = query.Count();
            var totalDisplay = query.Count();

            if (filter != null)
            {
                query = query.Where(filter);
                totalDisplay = query.Count();
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                var result = query.OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                if (isTrackingOff)
                    return (result.AsNoTracking().ToList(), total, totalDisplay);
                else
                    return (result.ToList(), total, totalDisplay);
            }
            else
            {
                var result = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                if (isTrackingOff)
                    return (result.AsNoTracking().ToList(), total, totalDisplay);
                else
                    return (result.ToList(), total, totalDisplay);
            }
        }
        //public virtual void Remove(Expression<Func<TEntity, bool>> filter)
        //{
        //    _dbSet.RemoveRange(_dbSet.Where(filter));
        //}

        public virtual void Edit(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _dbContext.Entry(entityToUpdate).State = EntityState.Modified;
        }
        public virtual IList<TEntity> GetAll()
        {
            IQueryable<TEntity> query = _dbSet;
            return query.ToList();
        }

        public virtual TEntity GetById(TKey id)
        {
            return _dbSet.Find(id);
        }
        //Used for IsDuplicate value check validation in creation
        public virtual int GetCount(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = _dbSet;
            var count = 0;

            if (filter != null)
            {
                query = query.Where(filter);
                count = query.Count();
            }
            else
                count = query.Count();

            return count;
        }

        //public virtual async Task AddAsync(TEntity entity)
        //{
        //    await _dbSet.AddAsync(entity);
        //}

        //public virtual async Task RemoveAsync(TKey id)
        //{
        //    var entityToDelete = _dbSet.Find(id);
        //    await RemoveAsync(entityToDelete);
        //}

        //public virtual async Task RemoveAsync(TEntity entityToDelete)
        //{
        //    await Task.Run(() =>
        //    {
        //        if (_dbContext.Entry(entityToDelete).State == EntityState.Detached)
        //        {
        //            _dbSet.Attach(entityToDelete);
        //        }
        //        _dbSet.Remove(entityToDelete);
        //    });
        //}

        //public virtual async Task RemoveAsync(Expression<Func<TEntity, bool>> filter)
        //{
        //    await Task.Run(() =>
        //    {
        //        _dbSet.RemoveRange(_dbSet.Where(filter));
        //    });
        //}

        //public virtual async Task EditAsync(TEntity entityToUpdate)
        //{
        //    await Task.Run(() =>
        //    {
        //        _dbSet.Attach(entityToUpdate);
        //        _dbContext.Entry(entityToUpdate).State = EntityState.Modified;
        //    });
        //}

        //public virtual async Task<TEntity> GetByIdAsync(TKey id)
        //{
        //    return await _dbSet.FindAsync(id);
        //}

        //public virtual async Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null)
        //{
        //    IQueryable<TEntity> query = _dbSet;
        //    var count = 0;

        //    if (filter != null)
        //    {
        //        query = query.Where(filter);
        //    }

        //    count = await query.CountAsync();
        //    return count;
        //}

        //public virtual async Task<IList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter,
        //    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        //{
        //    IQueryable<TEntity> query = _dbSet;

        //    if (filter != null)
        //    {
        //        query = query.Where(filter);
        //    }

        //    if (include != null)
        //        query = include(query);

        //    return await query.ToListAsync();
        //}

        //public virtual async Task<IList<TEntity>> GetAllAsync()
        //{
        //    IQueryable<TEntity> query = _dbSet;
        //    return await query.ToListAsync();
        //}

        //public virtual async Task<(IList<TEntity> data, int total, int totalDisplay)> GetAsync(
        //    Expression<Func<TEntity, bool>> filter = null,
        //    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        //    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        //    int pageIndex = 1,
        //    int pageSize = 10,
        //    bool isTrackingOff = false)
        //{
        //    IQueryable<TEntity> query = _dbSet;
        //    var total = query.Count();
        //    var totalDisplay = query.Count();

        //    if (filter != null)
        //    {
        //        query = query.Where(filter);
        //        totalDisplay = query.Count();
        //    }

        //    if (include != null)
        //        query = include(query);

        //    IList<TEntity> data;

        //    if (orderBy != null)
        //    {
        //        var result = orderBy(query).Skip((pageIndex - 1) * pageSize).Take(pageSize);

        //        if (isTrackingOff)
        //            data = await result.AsNoTracking().ToListAsync();
        //        else
        //            data = await result.ToListAsync();
        //    }
        //    else
        //    {
        //        var result = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

        //        if (isTrackingOff)
        //            data = await result.AsNoTracking().ToListAsync();
        //        else
        //            data = await result.ToListAsync();
        //    }

        //    return (data, total, totalDisplay);
        //}

        //public virtual async Task<(IList<TEntity> data, int total, int totalDisplay)> GetDynamicAsync(
        //    Expression<Func<TEntity, bool>> filter = null,
        //    string orderBy = null,
        //    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        //    int pageIndex = 1,
        //    int pageSize = 10,
        //    bool isTrackingOff = false)
        //{
        //    IQueryable<TEntity> query = _dbSet;
        //    var total = query.Count();
        //    var totalDisplay = query.Count();

        //    if (filter != null)
        //    {
        //        query = query.Where(filter);
        //        totalDisplay = query.Count();
        //    }

        //    if (include != null)
        //        query = include(query);

        //    IList<TEntity> data;

        //    if (orderBy != null)
        //    {
        //        var result = query.OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize);

        //        if (isTrackingOff)
        //            data = await result.AsNoTracking().ToListAsync();
        //        else
        //            data = await result.ToListAsync();
        //    }
        //    else
        //    {
        //        var result = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

        //        if (isTrackingOff)
        //            data = await result.AsNoTracking().ToListAsync();
        //        else
        //            data = await result.ToListAsync();
        //    }

        //    return (data, total, totalDisplay);
        //}

        //public virtual async Task<IList<TEntity>> GetAsync(
        //    Expression<Func<TEntity, bool>> filter = null,
        //    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        //    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        //    bool isTrackingOff = false)
        //{
        //    IQueryable<TEntity> query = _dbSet;

        //    if (filter != null)
        //    {
        //        query = query.Where(filter);
        //    }

        //    if (include != null)
        //        query = include(query);

        //    if (orderBy != null)
        //    {
        //        var result = orderBy(query);

        //        if (isTrackingOff)
        //            return await result.AsNoTracking().ToListAsync();
        //        else
        //            return await result.ToListAsync();
        //    }
        //    else
        //    {
        //        if (isTrackingOff)
        //            return await query.AsNoTracking().ToListAsync();
        //        else
        //            return await query.ToListAsync();
        //    }
        //}

        //public virtual async Task<IList<TEntity>> GetDynamicAsync(
        //    Expression<Func<TEntity, bool>> filter = null,
        //    string orderBy = null,
        //    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        //    bool isTrackingOff = false)
        //{
        //    IQueryable<TEntity> query = _dbSet;

        //    if (filter != null)
        //    {
        //        query = query.Where(filter);
        //    }

        //    if (include != null)
        //        query = include(query);

        //    if (orderBy != null)
        //    {
        //        var result = query.OrderBy(orderBy);

        //        if (isTrackingOff)
        //            return await result.AsNoTracking().ToListAsync();
        //        else
        //            return await result.ToListAsync();
        //    }
        //    else
        //    {
        //        if (isTrackingOff)
        //            return await query.AsNoTracking().ToListAsync();
        //        else
        //            return await query.ToListAsync();
        //    }
        //}



        //public virtual IList<TEntity> Get(Expression<Func<TEntity, bool>> filter,
        //    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        //{
        //    IQueryable<TEntity> query = _dbSet;

        //    if (filter != null)
        //    {
        //        query = query.Where(filter);
        //    }

        //    if (include != null)
        //        query = include(query);

        //    return query.ToList();
        //}



        //public virtual (IList<TEntity> data, int total, int totalDisplay) Get(
        //    Expression<Func<TEntity, bool>> filter = null,
        //    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        //    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        //    int pageIndex = 1, int pageSize = 10, bool isTrackingOff = false)
        //{
        //    IQueryable<TEntity> query = _dbSet;
        //    var total = query.Count();
        //    var totalDisplay = query.Count();

        //    if (filter != null)
        //    {
        //        query = query.Where(filter);
        //        totalDisplay = query.Count();
        //    }

        //    if (include != null)
        //        query = include(query);

        //    if (orderBy != null)
        //    {
        //        var result = orderBy(query).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        //        if (isTrackingOff)
        //            return (result.AsNoTracking().ToList(), total, totalDisplay);
        //        else
        //            return (result.ToList(), total, totalDisplay);
        //    }
        //    else
        //    {
        //        var result = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        //        if (isTrackingOff)
        //            return (result.AsNoTracking().ToList(), total, totalDisplay);
        //        else
        //            return (result.ToList(), total, totalDisplay);
        //    }
        //}

        //public virtual (IList<TEntity> data, int total, int totalDisplay) GetDynamic(
        //    Expression<Func<TEntity, bool>> filter = null,
        //    string orderBy = null,
        //    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        //    int pageIndex = 1, int pageSize = 10, bool isTrackingOff = false)
        //{
        //    IQueryable<TEntity> query = _dbSet;
        //    var total = query.Count();
        //    var totalDisplay = query.Count();

        //    if (filter != null)
        //    {
        //        query = query.Where(filter);
        //        totalDisplay = query.Count();
        //    }

        //    if (include != null)
        //        query = include(query);

        //    if (orderBy != null)
        //    {
        //        var result = query.OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        //        if (isTrackingOff)
        //            return (result.AsNoTracking().ToList(), total, totalDisplay);
        //        else
        //            return (result.ToList(), total, totalDisplay);
        //    }
        //    else
        //    {
        //        var result = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        //        if (isTrackingOff)
        //            return (result.AsNoTracking().ToList(), total, totalDisplay);
        //        else
        //            return (result.ToList(), total, totalDisplay);
        //    }
        //}

        //public virtual IList<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
        //    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        //    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        //    bool isTrackingOff = false)
        //{
        //    IQueryable<TEntity> query = _dbSet;

        //    if (filter != null)
        //    {
        //        query = query.Where(filter);
        //    }

        //    if (include != null)
        //        query = include(query);

        //    if (orderBy != null)
        //    {
        //        var result = orderBy(query);

        //        if (isTrackingOff)
        //            return result.AsNoTracking().ToList();
        //        else
        //            return result.ToList();
        //    }
        //    else
        //    {
        //        if (isTrackingOff)
        //            return query.AsNoTracking().ToList();
        //        else
        //            return query.ToList();
        //    }
        //}

        //public virtual IList<TEntity> GetDynamic(Expression<Func<TEntity, bool>> filter = null,
        //    string orderBy = null,
        //    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        //    bool isTrackingOff = false)
        //{
        //    IQueryable<TEntity> query = _dbSet;

        //    if (filter != null)
        //    {
        //        query = query.Where(filter);
        //    }

        //    if (include != null)
        //        query = include(query);

        //    if (orderBy != null)
        //    {
        //        var result = query.OrderBy(orderBy);

        //        if (isTrackingOff)
        //            return result.AsNoTracking().ToList();
        //        else
        //            return result.ToList();
        //    }
        //    else
        //    {
        //        if (isTrackingOff)
        //            return query.AsNoTracking().ToList();
        //        else
        //            return query.ToList();
        //    }
        //}

        //public async Task<IEnumerable<TResult>> GetAsync<TResult>(Expression<Func<TEntity, TResult>>? selector,
        //    Expression<Func<TEntity, bool>>? predicate = null,
        //    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        //    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        //    bool disableTracking = true,
        //    CancellationToken cancellationToken = default) where TResult : class
        //{
        //    var query = _dbSet.AsQueryable();
        //    if (disableTracking) query.AsNoTracking();
        //    if (include is not null) query = include(query);
        //    if (predicate is not null) query = query.Where(predicate);
        //    return orderBy is not null
        //        ? await orderBy(query).Select(selector!).ToListAsync(cancellationToken)
        //        : await query.Select(selector!).ToListAsync(cancellationToken);
        //}

        //public async Task<TResult> SingleOrDefaultAsync<TResult>(Expression<Func<TEntity, TResult>>? selector,
        //    Expression<Func<TEntity, bool>>? predicate = null,
        //    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        //    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        //    bool disableTracking = true)
        //{
        //    var query = _dbSet.AsQueryable();
        //    if (disableTracking) query.AsNoTracking();
        //    if (include is not null) query = include(query);
        //    if (predicate is not null) query = query.Where(predicate);
        //    return (orderBy is not null
        //        ? await orderBy(query).Select(selector!).FirstOrDefaultAsync()
        //        : await query.Select(selector!).FirstOrDefaultAsync())!;
        //}

       
        

    }
}
