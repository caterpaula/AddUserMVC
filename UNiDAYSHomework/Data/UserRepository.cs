using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
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
                gateway.ExecuteDbQuery(query, queryParameters);
            }
        }

        public List<User> ListAllUsers()
        {
            string query = @"
                SELECT UserID
                    , EmailAddress
                FROM Users
                ";

            var queryResult = gateway.ReturnQueryResults(query, null, ReadUser);

            if (!queryResult.WasSuccessful)
            {
                Debug.Write(queryResult.Feedback);
                return new List<User>();
            }

            return queryResult.Results;
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