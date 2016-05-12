using System.Collections.Generic;
using System.Data.Common;
using UNiDAYSHomework.DataAccess;
using UNiDAYSHomework.Models;

namespace UNiDAYSHomework.Data
{
    public interface IUserRepository
    {
        QueryResult<int> CreateUser<T>(User newUser);
        QueryResult<List<User>> ListAllUsers<T>();
        User ReadUser(DbDataReader reader);
    }
}
