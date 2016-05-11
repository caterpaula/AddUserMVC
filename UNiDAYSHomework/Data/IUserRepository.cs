using UNiDAYSHomework.Models;

namespace UNiDAYSHomework.Data
{
    public interface IUserRepository
    {
        void CreateUser(User newUser);
        void ListUsers();
    }
}
