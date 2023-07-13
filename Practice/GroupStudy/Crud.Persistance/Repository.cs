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
        protected int CommandTimeout { get; set; }
        public Repository(DbContext dbContext)
        {
            CommandTimeout = 300;
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

        //protected virtual IDictionary<string, object> ExecuteStoredProcedure(string storedProcedureName,
        //    IDictionary<string, object> parameters = null, IDictionary<string, Type> outParameters = null)
        //{
        //    var command = CreateCommand(storedProcedureName, parameters, outParameters);
        //    command = ConvertNullToDbNull(command);

        //    var connectionOpened = false;
        //    if (command.Connection.State == ConnectionState.Closed)
        //    {
        //        command.Connection.Open();
        //        connectionOpened = true;
        //    }

        //    try
        //    {
        //        command.ExecuteNonQuery();
        //    }
        //    finally
        //    {
        //        if (connectionOpened)
        //            command.Connection.Close();
        //    }

        //    return CopyOutParams(command, outParameters);
        //}

        //protected virtual async Task<IDictionary<string, object>> ExecuteStoredProcedureAsync(string storedProcedureName,
        //    IDictionary<string, object> parameters = null, IDictionary<string, Type> outParameters = null)
        //{
        //    var command = CreateCommand(storedProcedureName, parameters, outParameters);
        //    command = ConvertNullToDbNull(command);

        //    var connectionOpened = false;
        //    if (command.Connection.State == ConnectionState.Closed)
        //    {
        //        await command.Connection.OpenAsync();
        //        connectionOpened = true;
        //    }

        //    try
        //    {
        //        await command.ExecuteNonQueryAsync();
        //    }
        //    finally
        //    {
        //        if (connectionOpened)
        //            await command.Connection.CloseAsync();
        //    }

        //    return CopyOutParams(command, outParameters);
        //}

        //protected virtual async Task<(IList<TReturn> result, IDictionary<string, object> outValues)>
        //    QueryWithStoredProcedureAsync<TReturn>(string storedProcedureName,
        //    IDictionary<string, object> parameters = null, IDictionary<string, Type> outParameters = null)
        //    where TReturn : class, new()
        //{
        //    var command = CreateCommand(storedProcedureName, parameters, outParameters);

        //    var connectionOpened = false;
        //    if (command.Connection.State == ConnectionState.Closed)
        //    {
        //        await command.Connection.OpenAsync();
        //        connectionOpened = true;
        //    }

        //    IList<TReturn> result = null;
        //    try
        //    {
        //        result = await ExecuteQueryAsync<TReturn>(command);
        //    }
        //    finally
        //    {
        //        if (connectionOpened)
        //            await command.Connection.CloseAsync();
        //    }

        //    var outValues = CopyOutParams(command, outParameters);

        //    return (result, outValues);
        //}

        //protected virtual async Task<TReturn> ExecuteScalarAsync<TReturn>(string storedProcedureName,
        //    IDictionary<string, object> parameters = null)
        //{
        //    var command = CreateCommand(storedProcedureName, parameters);

        //    var connectionOpened = false;
        //    if (command.Connection.State == ConnectionState.Closed)
        //    {
        //        await command.Connection.OpenAsync();
        //        connectionOpened = true;
        //    }

        //    TReturn result;

        //    try
        //    {
        //        result = await ExecuteScalarAsync<TReturn>(command);
        //    }
        //    finally
        //    {
        //        if (connectionOpened)
        //            await command.Connection.CloseAsync();
        //    }

        //    return result;
        //}

        //private DbCommand CreateCommand(string storedProcedureName,
        //    IDictionary<string, object> parameters = null,
        //    IDictionary<string, Type> outParameters = null)
        //{
        //    var connection = _dbContext.Database.GetDbConnection();
        //    var command = connection.CreateCommand();
        //    command.CommandText = storedProcedureName;
        //    command.CommandType = CommandType.StoredProcedure;
        //    command.CommandTimeout = CommandTimeout;

        //    if (parameters != null)
        //    {
        //        foreach (var item in parameters)
        //        {
        //            command.Parameters.Add(CreateParameter(item.Key, item.Value));
        //        }
        //    }

        //    if (outParameters != null)
        //    {
        //        foreach (var item in outParameters)
        //        {
        //            command.Parameters.Add(CreateOutputParameter(item.Key,
        //                item.Value));
        //        }
        //    }

        //    return command;
        //}

        //private DbParameter CreateParameter(string name, object value)
        //{
        //    return new SqlParameter(name, CorrectSqlDateTime(value));
        //}

        //private DbParameter CreateOutputParameter(string name, DbType dbType)
        //{
        //    var outParam = new SqlParameter(name, CorrectSqlDateTime(dbType));
        //    outParam.Direction = ParameterDirection.Output;
        //    return outParam;
        //}

        //private DbParameter CreateOutputParameter(string name, Type type)
        //{
        //    var outParam = new SqlParameter(name, GetDbTypeFromType(type));
        //    outParam.Direction = ParameterDirection.Output;
        //    return outParam;
        //}

        //private SqlDbType GetDbTypeFromType(Type type)
        //{
        //    if (type == typeof(int) ||
        //        type == typeof(uint) ||
        //        type == typeof(short) ||
        //        type == typeof(ushort))
        //        return SqlDbType.Int;
        //    else if (type == typeof(long) || type == typeof(ulong))
        //        return SqlDbType.BigInt;
        //    else if (type == typeof(double) || type == typeof(decimal))
        //        return SqlDbType.Decimal;
        //    else if (type == typeof(string))
        //        return SqlDbType.NVarChar;
        //    else if (type == typeof(DateTime))
        //        return SqlDbType.DateTime;
        //    else if (type == typeof(bool))
        //        return SqlDbType.Bit;
        //    else if (type == typeof(Guid))
        //        return SqlDbType.UniqueIdentifier;
        //    else if (type == typeof(char))
        //        return SqlDbType.NVarChar;
        //    else
        //        return SqlDbType.NVarChar;
        //}

        //private object ChangeType(Type propertyType, object itemValue)
        //{
        //    if (itemValue is DBNull)
        //        return null;

        //    return itemValue is decimal && propertyType == typeof(double) ?
        //        Convert.ToDouble(itemValue) : itemValue;
        //}

        //private object CorrectSqlDateTime(object parameterValue)
        //{
        //    if (parameterValue != null && parameterValue.GetType().Name == "DateTime")
        //    {
        //        if (Convert.ToDateTime(parameterValue) < SqlDateTime.MinValue.Value)
        //            return SqlDateTime.MinValue.Value;
        //        else
        //            return parameterValue;
        //    }
        //    else
        //        return parameterValue;
        //}

        //private async Task<IList<TReturn>> ExecuteQueryAsync<TReturn>(DbCommand command)
        //{
        //    var reader = await command.ExecuteReaderAsync();
        //    var result = new List<TReturn>();

        //    while (await reader.ReadAsync())
        //    {
        //        var type = typeof(TReturn);
        //        var constructor = type.GetConstructor(new Type[] { });
        //        if (constructor == null)
        //            throw new InvalidOperationException("An empty contructor is required for the return type");

        //        var instance = constructor.Invoke(new object[] { });

        //        for (var i = 0; i < reader.FieldCount; i++)
        //        {
        //            var property = type.GetProperty(reader.GetName(i));
        //            property?.SetValue(instance, ChangeType(property.PropertyType, reader.GetValue(i)));
        //        }

        //        result.Add((TReturn)instance);
        //    }

        //    return result;
        //}

        //private async Task<TReturn> ExecuteScalarAsync<TReturn>(DbCommand command)
        //{
        //    command = ConvertNullToDbNull(command);

        //    if (command.Connection.State != ConnectionState.Open)
        //        command.Connection.Open();

        //    var result = await command.ExecuteScalarAsync();

        //    if (result == DBNull.Value)
        //        return default;
        //    else
        //        return (TReturn)result;
        //}

        //private DbCommand ConvertNullToDbNull(DbCommand command)
        //{
        //    for (int i = 0; i < command.Parameters.Count; i++)
        //    {
        //        if (command.Parameters[i].Value == null)
        //            command.Parameters[i].Value = DBNull.Value;
        //    }

        //    return command;
        //}

        //private IDictionary<string, object> CopyOutParams(DbCommand command,
        //    IDictionary<string, Type> outParameters)
        //{
        //    Dictionary<string, object> result = null;
        //    if (outParameters != null)
        //    {
        //        result = new Dictionary<string, object>();
        //        foreach (var item in outParameters)
        //        {
        //            result.Add(item.Key, command.Parameters[item.Key].Value);
        //        }
        //    }

        //    return result;
        //}

    }
}
