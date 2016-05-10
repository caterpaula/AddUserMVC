using System;
using System.Collections.Generic;
using UNiDAYSHomework.Models;
using UNiDAYSHomework.DataAccess;

namespace UNiDAYSHomework.Data
{
    public class UserRepository : IUserRepository
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

            //create a dictionary of paramers and their values to pass to ExecuteDbQuery method
            Dictionary<string, object> queryParameters = new Dictionary<string, object>()
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

        public void GetUserByID(Guid userID)
        {
            string query = @"
                SELECT EmailAddress from Users
                WHERE 
                    UserID = " + userID;
        }

        public void UpdateUser(User currentUser)
        {

        }

    }
}