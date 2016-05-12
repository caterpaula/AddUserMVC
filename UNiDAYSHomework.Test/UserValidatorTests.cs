using FluentValidation.TestHelper;
using NUnit.Framework;
using UNiDAYSHomework.Models;

namespace UNiDAYSHomework.Test
{
    [TestFixture]
    class UserValidatorTests
    {
        private UserValidator validator;

        [SetUp]
        public void Setup()
        {
            validator = new UserValidator();
        }


        [Test]
        public void TestNullEmailIsInvalid()
        {
            
        }

        public void TestEmailWithoutAtIsInvalid()
        {

        }

    }
}
