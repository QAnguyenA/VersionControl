using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using UnitTestExample.Controllers;

namespace UnitTestExample.Test
{
    public class AccountControllerTestFixture
    {
        [
            
            TestCase("ABCD1234",false),
            TestCase("Ab1234",false),
            TestCase("Abcd1234",true),
            TestCase("abcd1234",false),
            TestCase("abcdABCD",false)

        ]

        public void TestValidatePassword(string password, bool expectedResult)
        {
            // Arrange
            var accountController = new AccountController();

            // Act
            var actualResult = accountController.ValidateEmail(password);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);

        }
    }
}
