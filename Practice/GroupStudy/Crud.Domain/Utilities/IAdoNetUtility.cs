using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Domain.Utilities
{
    public interface IAdoNetUtility
    {
         IDictionary<string, object> ExecuteStoredProcedure(string storedProcedureName,
        IDictionary<string, object> parameters = null, IDictionary<string, Type> outParameters = null);
        Task<IDictionary<string, object>> ExecuteStoredProcedureAsync(string storedProcedureName,
            IDictionary<string, object> parameters = null, IDictionary<string, Type> outParameters = null);
        Task<(IList<TReturn> result, IDictionary<string, object> outValues)>
            QueryWithStoredProcedureAsync<TReturn>(string storedProcedureName,
            IDictionary<string, object> parameters = null, IDictionary<string, Type> outParameters = null)
            where TReturn : class, new();
        Task<TReturn> ExecuteScalarAsync<TReturn>(string storedProcedureName,
            IDictionary<string, object> parameters = null);
    }
}
