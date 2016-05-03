using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UNiDAYSHomework.Models;
using UNiDAYSHomework.Utilities;

namespace UNiDAYSHomework.DataAccess
{
    public class SQLAccess
    {
        public static void CreateUserQuery(User newUser)
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


    }
}