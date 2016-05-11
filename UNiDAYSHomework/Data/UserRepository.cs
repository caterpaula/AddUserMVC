using System;
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

        public void CreateUser(User newUser)
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

            if (newUser.EmailAddress != "bob@myunidays.com")
            {
                gateway.ExecuteDbQueryWithParams(query, queryParameters);
            }
        }
            
        public List<User> ListAllUsers()
        {
            string query = @"
                SELECT UserID
                    , EmailAddress
                FROM Users
                ";


            return gateway.ReturnQueryResults(query, null, ReadUser);
        }

        public User FindUserByEmail(string userEmail)
        {
            string query = @"
                SELECT UserID
                    , EmailAddress
                FROM Users WHERE
                    EmailAddress = @EmailAddress
                ";

            var queryParameters = new Dictionary<string, object>()
            {
                { "@EmailAddress", userEmail }
            };

            var users = gateway.ReturnQueryResults(query, queryParameters, ReadUser);

            return users[0];
        }

        public User ReadUser(DbDataReader reader)
        {
            var user = new User
            {
                UserID = (Guid)reader["UserID"],
                EmailAddress = (string)reader["EmailAddress"]
            };
            return user;
        }

    }
}