
using Crud.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Persistance
{
    public class AdoNetUtility : IAdoNetUtility
    {
        private readonly DbConnection _connection;
        private readonly int _timeout;
        public AdoNetUtility(DbConnection connection, int timeout) {
            _connection = connection;
            _timeout = timeout;
        }



        public virtual IDictionary<string, object> ExecuteStoredProcedure(string storedProcedureName,
            IDictionary<string, object> parameters = null, IDictionary<string, Type> outParameters = null)
        {
            var command = CreateCommand(storedProcedureName, parameters, outParameters);
            command = ConvertNullToDbNull(command);

            var connectionOpened = false;
            if (command.Connection.State == ConnectionState.Closed)
            {
                command.Connection.Open();
                connectionOpened = true;
            }

            try
            {
                command.ExecuteNonQuery();
            }
            finally
            {
                if (connectionOpened)
                    command.Connection.Close();
            }

            return CopyOutParams(command, outParameters);
        }

        public virtual async Task<IDictionary<string, object>> ExecuteStoredProcedureAsync(string storedProcedureName,
            IDictionary<string, object> parameters = null, IDictionary<string, Type> outParameters = null)
        {
            var command = CreateCommand(storedProcedureName, parameters, outParameters);
            command = ConvertNullToDbNull(command);

            var connectionOpened = false;
            if (command.Connection.State == ConnectionState.Closed)
            {
                await command.Connection.OpenAsync();
                connectionOpened = true;
            }

            try
            {
                await command.ExecuteNonQueryAsync();
            }
            finally
            {
                if (connectionOpened)
                    await command.Connection.CloseAsync();
            }

            return CopyOutParams(command, outParameters);
        }

        public virtual async Task<(IList<TReturn> result, IDictionary<string, object> outValues)>
            QueryWithStoredProcedureAsync<TReturn>(string storedProcedureName,
            IDictionary<string, object> parameters = null, IDictionary<string, Type> outParameters = null)
            where TReturn : class, new()
        {
            var command = CreateCommand(storedProcedureName, parameters, outParameters);

            var connectionOpened = false;
            if (command.Connection.State == ConnectionState.Closed)
            {
                await command.Connection.OpenAsync();
                connectionOpened = true;
            }

            IList<TReturn> result = null;
            try
            {
                result = await ExecuteQueryAsync<TReturn>(command);
            }
            finally
            {
                if (connectionOpened)
                    await command.Connection.CloseAsync();
            }

            var outValues = CopyOutParams(command, outParameters);

            return (result, outValues);
        }

        public virtual async Task<TReturn> ExecuteScalarAsync<TReturn>(string storedProcedureName,
            IDictionary<string, object> parameters = null)
        {
            var command = CreateCommand(storedProcedureName, parameters);

            var connectionOpened = false;
            if (command.Connection.State == ConnectionState.Closed)
            {
                await command.Connection.OpenAsync();
                connectionOpened = true;
            }

            TReturn result;

            try
            {
                result = await ExecuteScalarAsync<TReturn>(command);
            }
            finally
            {
                if (connectionOpened)
                    await command.Connection.CloseAsync();
            }

            return result;
        }

        private DbCommand CreateCommand(string storedProcedureName,
            IDictionary<string, object> parameters = null,
            IDictionary<string, Type> outParameters = null)
        {
           
            var command = _connection.CreateCommand();
            command.CommandText = storedProcedureName;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = _timeout;

            if (parameters != null)
            {
                foreach (var item in parameters)
                {
                    command.Parameters.Add(CreateParameter(item.Key, item.Value));
                }
            }

            if (outParameters != null)
            {
                foreach (var item in outParameters)
                {
                    command.Parameters.Add(CreateOutputParameter(item.Key,
                        item.Value));
                }
            }

            return command;
        }
        private DbParameter CreateParameter(string name, object value)
        {
            return new SqlParameter(name, CorrectSqlDateTime(value));
        }

        private DbParameter CreateOutputParameter(string name, DbType dbType)
        {
            var outParam = new SqlParameter(name, CorrectSqlDateTime(dbType));
            outParam.Direction = ParameterDirection.Output;
            return outParam;
        }

        private DbParameter CreateOutputParameter(string name, Type type)
        {
            var outParam = new SqlParameter(name, GetDbTypeFromType(type));
            outParam.Direction = ParameterDirection.Output;
            return outParam;
        }

        private SqlDbType GetDbTypeFromType(Type type)
        {
            if (type == typeof(int) ||
                type == typeof(uint) ||
                type == typeof(short) ||
                type == typeof(ushort))
                return SqlDbType.Int;
            else if (type == typeof(long) || type == typeof(ulong))
                return SqlDbType.BigInt;
            else if (type == typeof(double) || type == typeof(decimal))
                return SqlDbType.Decimal;
            else if (type == typeof(string))
                return SqlDbType.NVarChar;
            else if (type == typeof(DateTime))
                return SqlDbType.DateTime;
            else if (type == typeof(bool))
                return SqlDbType.Bit;
            else if (type == typeof(Guid))
                return SqlDbType.UniqueIdentifier;
            else if (type == typeof(char))
                return SqlDbType.NVarChar;
            else
                return SqlDbType.NVarChar;
        }

        private object ChangeType(Type propertyType, object itemValue)
        {
            if (itemValue is DBNull)
                return null;

            return itemValue is decimal && propertyType == typeof(double) ?
                Convert.ToDouble(itemValue) : itemValue;
        }

        private object CorrectSqlDateTime(object parameterValue)
        {
            if (parameterValue != null && parameterValue.GetType().Name == "DateTime")
            {
                if (Convert.ToDateTime(parameterValue) < SqlDateTime.MinValue.Value)
                    return SqlDateTime.MinValue.Value;
                else
                    return parameterValue;
            }
            else
                return parameterValue;
        }

        private async Task<IList<TReturn>> ExecuteQueryAsync<TReturn>(DbCommand command)
        {
            var reader = await command.ExecuteReaderAsync();
            var result = new List<TReturn>();

            while (await reader.ReadAsync())
            {
                var type = typeof(TReturn);
                var constructor = type.GetConstructor(new Type[] { });
                if (constructor == null)
                    throw new InvalidOperationException("An empty contructor is required for the return type");

                var instance = constructor.Invoke(new object[] { });

                for (var i = 0; i < reader.FieldCount; i++)
                {
                    var property = type.GetProperty(reader.GetName(i));
                    property?.SetValue(instance, ChangeType(property.PropertyType, reader.GetValue(i)));
                }

                result.Add((TReturn)instance);
            }

            return result;
        }

        private async Task<TReturn> ExecuteScalarAsync<TReturn>(DbCommand command)
        {
            command = ConvertNullToDbNull(command);

            if (command.Connection.State != ConnectionState.Open)
                command.Connection.Open();

            var result = await command.ExecuteScalarAsync();

            if (result == DBNull.Value)
                return default;
            else
                return (TReturn)result;
        }

        private DbCommand ConvertNullToDbNull(DbCommand command)
        {
            for (int i = 0; i < command.Parameters.Count; i++)
            {
                if (command.Parameters[i].Value == null)
                    command.Parameters[i].Value = DBNull.Value;
            }

            return command;
        }

        private IDictionary<string, object> CopyOutParams(DbCommand command,
            IDictionary<string, Type> outParameters)
        {
            Dictionary<string, object> result = null;
            if (outParameters != null)
            {
                result = new Dictionary<string, object>();
                foreach (var item in outParameters)
                {
                    result.Add(item.Key, command.Parameters[item.Key].Value);
                }
            }

            return result;
        }
    }
}
