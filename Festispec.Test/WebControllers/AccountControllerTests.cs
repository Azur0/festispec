using Festispec.WebApplication.ViewModels;
using Festispec.WebApplication2.Controllers;
using NUnit.Framework;
using System;
using System.Web.Mvc;

namespace Festispec.Test.WebControllers
{
    /// <summary>
    ///     Tests of the account controller that checks if all 
    ///     functions can be called and properly execute (redirect +) view return
    /// </summary>
    [TestFixture]
    public class AccountControllerTests
    {
        private AccountController _accountController;

        [SetUp]
        public void Setup()
        {
            _accountController = new AccountController();
        }
        
        [Test]
        public void Index_ParameterLess_ExpectedBehavior()
        {
            // Arrange
            LoginViewModel model = null;
            
            ActionResult result = _accountController.Index(model);

            // Act & Assert
            Assert.IsInstanceOf(typeof(ViewResult), result);
        }

        [Test]
        public void Login_ParameterLess_ExpectedBehavior()
        {
            // Arrange
            LoginViewModel model = null;

            // Act
            var result = _accountController.Login(model);

            // Assert
            Assert.IsInstanceOf(typeof(ViewResult), result);
        }

        [Test]
        public void Logout_StateUnderTest_ExpectedBehavior()
        {
            // Act
            var result = _accountController.Logout();

            // Assert
            Assert.IsInstanceOf(typeof(ViewResult), result);
        }
    }
}
