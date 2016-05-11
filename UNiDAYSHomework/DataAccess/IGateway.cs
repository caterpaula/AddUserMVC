using System.Collections.Generic;
using UNiDAYSHomework.Models;

namespace UNiDAYSHomework.DataAccess
{
    public interface IGateway
    {
        int ExecuteDbQueryWithParams(string query, Dictionary<string, object> queryParams);
        List<User> ReturnUsers(string query);
    }
}
