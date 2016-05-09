using System;
using System.Collections.Generic;
using UNiDAYSHomework.Models;
using UNiDAYSHomework.Utilities;

namespace UNiDAYSHomework.Data
{
    public class UserRepository
    {
        public void CreateUser(User newUser)
        {
            string query =
            "insert into Users (UserID, EmailAddress, Password) values (@UserID, @EmailAddress, @Password)";

            //create a dictionary of paramers and their values to pass to ExecuteDbQuery method
            Dictionary<string, object> queryParameters = new Dictionary<string, object>()
            {
                { "@UserID", newUser.UserID},
                { "@EmailAddress", newUser.EmailAddress },
                { "@Password", newUser.EncryptedPassword }
            };

            DataAccessUtils.ExecuteDbQuery(query, queryParameters);
        }

        public void GetUserByID(Guid userID)
        {

        }

        public void UpdateUser(User currentUser)
        {

        }

    }
}