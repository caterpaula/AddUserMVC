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
        public void ShouldExecuteDbQueryWithParams()
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
            var queryParameters = new Dictionary<string, object>()
            {
                { "@UserID", Guid.NewGuid() },
                { "@EmailAddress", "paula.besson@myunidays.com" },
                { "@Password", "testpassword" }
            };

            var expectedResult = gateway.ExecuteDbQuery(query, queryParameters);

            Assert.That(expectedResult.Results, Is.EqualTo(1));
        }

    }
}
