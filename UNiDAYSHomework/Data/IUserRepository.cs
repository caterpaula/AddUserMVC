using System.Collections.Generic;
using System.Data.Common;
using UNiDAYSHomework.Models;

namespace UNiDAYSHomework.Data
{
    public interface IUserRepository
    {
        void CreateUser(User newUser);
        List<User> ListAllUsers();
        User ReadUser(DbDataReader reader);
    }
}
