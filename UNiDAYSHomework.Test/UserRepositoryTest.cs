﻿using NUnit.Framework;
using System.Collections.Generic;
using UNiDAYSHomework.Data;
using UNiDAYSHomework.DataAccess;
using UNiDAYSHomework.Models;
using NSubstitute;

namespace UNiDAYSHomework.Test
{
    [TestFixture]
    class UserRepositoryTest
    {
        [Test]
        public void ShouldCreateUser()
        {
            var gateway = Substitute.For<IGateway>();

            IUserRepository userRepository = new UserRepository(gateway);

            var user = new User();

            userRepository.CreateUser(user);

            gateway.Received().ExecuteDbQueryWithParams(Arg.Any<string>(), Arg.Any<Dictionary<string, object>>());

        }

        [Test]
        public void ShouldAcceptAlice()
        {
            var gateway = Substitute.For<IGateway>();

            IUserRepository userRepository = new UserRepository(gateway);

            var user = new User {EmailAddress = "alice@myunidays.com"};


            userRepository.CreateUser(user);

            gateway.Received().ExecuteDbQueryWithParams(Arg.Any<string>(), Arg.Any<Dictionary<string, object>>());
        }

        [Test]
        public void ShouldRejectBob()
        {
            var gateway = Substitute.For<IGateway>();

            IUserRepository userRepository = new UserRepository(gateway);

            var user = new User {EmailAddress = "bob@myunidays.com"};


            userRepository.CreateUser(user);

            gateway.DidNotReceive().ExecuteDbQueryWithParams(Arg.Any<string>(), Arg.Any<Dictionary<string, object>>());
        }

        [Test]
        public void ShouldListUsers()
        {

            var mockUserList = new List<User>();

            var gateway = Substitute.For<IGateway>();

            gateway.ReturnUsers(Arg.Any<string>()).Returns(mockUserList);

            IUserRepository userRepository = new UserRepository(gateway);

            var returnedUsers = userRepository.ListAllUsers();

            Assert.AreEqual(mockUserList, returnedUsers);
        }
    }
}