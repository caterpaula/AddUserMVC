using System;
using System.Collections.Generic;
using System.Data.Common;
using UNiDAYSHomework.Models;

namespace UNiDAYSHomework.DataAccess
{
    public interface IGateway
    {
        QueryResult<int> ExecuteDbQuery(string query, Dictionary<string, object> queryParams);
        QueryResult<List<T>> ReturnQueryResults<T>(string query, Dictionary<string, object> queryParams, Func<DbDataReader, T> userFunc);
    }
}
