using System.Collections.Generic;
using System.Data.Common;
using UNiDAYSHomework.Models;
using UNiDAYSHomework.DataAccess;

namespace UNiDAYSHomework.Data
{
    public sealed class UserRepository : IUserRepository
    {
        IGateway gateway;

        public UserRepository(IGateway gateway)
        {
            this.gateway = gateway;
        }

        public QueryResult<int> CreateUser<T>(User newUser)
        {
            string query = @"
                insert into Users (
                    UserID
                    , EmailAddress
                    , Password
                ) values (
                    @UserID
                    , @EmailAddress
                    , @Password
                )";

            //create a dictionary of parameters and their values to pass to ExecuteDbQuery method
            var queryParameters = new Dictionary<string, object>()
            {
                { "@UserID", newUser.UserID},
                { "@EmailAddress", newUser.EmailAddress },
                { "@Password", newUser.EncryptedPassword }
            };

            var queryResult = new QueryResult<int>();

            if (newUser.EmailAddress != "bob@myunidays.com")
            {
                queryResult = gateway.ExecuteDbQuery(query, queryParameters);
            }

            return queryResult;
        }

        public QueryResult<List<User>> ListAllUsers<T>()
        {
            string query = @"
                SELECT UserID
                    , EmailAddress
                FROM Users
                ";

            return gateway.ReturnQueryResults(query, null, ReadUser);

        }

        public User ReadUser(DbDataReader reader)
        {
            var user = new User
            {
                UserID = reader.GetGuid(0),
                EmailAddress = reader.IsDBNull(1) ? null : reader.GetString(1)
            };
            return user;
        }
    }
}