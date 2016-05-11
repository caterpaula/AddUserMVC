using System.Collections.Generic;
using UNiDAYSHomework.Models;

namespace UNiDAYSHomework.Data
{
    public interface IUserRepository
    {
        void CreateUser(User newUser);
        List<User> ListAllUsers();
    }
}
