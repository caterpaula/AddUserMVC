using NUnit.Framework;
using System;
using System.Collections.Generic;
using UNiDAYSHomework.DataAccess;

namespace UNiDAYSHomework.Test
{
    [TestFixture]
    public class GatewayTest
    {
        [Test]
        public void ShouldExecuteDBQueryWithParams()
        {
            IGateway gateway = new Gateway("Server=localhost;Database=UNiDAYSHomeworkTest;Trusted_Connection=True;");

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
                { "@UserID", Guid.NewGuid() },
                { "@EmailAddress", "paula.besson@myunidays.com" },
                { "@Password", "testpassword" }
            };

            int expectedResult = gateway.ExecuteDbQueryWithParams(query, queryParameters);

            Assert.That(expectedResult, Is.EqualTo(1));
        }

    }
}
