using System;
using System.Collections.Generic;
using System.Data.Common;
using UNiDAYSHomework.Models;

namespace UNiDAYSHomework.DataAccess
{
    public interface IGateway
    {
        int ExecuteDbQueryWithParams(string query, Dictionary<string, object> queryParams);
        List<T> ReturnQueryResults<T>(string query, Func<DbDataReader, T> userFunc);
    }
}
