using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNiDAYSHomework.Data;
using UNiDAYSHomework.DataAccess;
using UNiDAYSHomework.Models;

namespace UNiDAYSHomework.Test
{
    [TestFixture]
    class UserRepositoryTest
    {
        [Test]
        public void ShouldCreateUser()
        {
            var mockGateway = new MockGateway();

            IUserRepository userRepository = new UserRepository(mockGateway);

            var user = new User();

            userRepository.CreateUser(user);

            Assert.IsTrue(mockGateway.functionCalled);

        }


        [Test]
        public void ShouldAcceptAlice()
        {
            var mockGateway = new MockGateway();

            IUserRepository userRepository = new UserRepository(mockGateway);

            var user = new User();

            user.EmailAddress = "alice@myunidays.com";

            userRepository.CreateUser(user);

            Assert.IsTrue(mockGateway.functionCalled);
        }

        [Test]
        public void ShouldRejectBob()
        {
            var mockGateway = new MockGateway();

            IUserRepository userRepository = new UserRepository(mockGateway);

            var user = new User();

            user.EmailAddress = "bob@myunidays.com";

            userRepository.CreateUser(user);

            Assert.IsFalse(mockGateway.functionCalled);
        }



        class MockGateway : IGateway
        {
            public bool functionCalled;

            public int ExecuteDbQueryWithParams(string query, Dictionary<string, object> queryParams)
            {
                functionCalled = true;
                return 1;
            }
        }

    }
}
