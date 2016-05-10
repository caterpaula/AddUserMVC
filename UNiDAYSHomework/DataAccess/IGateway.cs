using System.Collections.Generic;

namespace UNiDAYSHomework.DataAccess
{
    public interface IGateway
    {
        int ExecuteDbQueryWithParams(string query, Dictionary<string, object> queryParams);
    }
}
