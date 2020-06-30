using Festispec.WebApplication.ViewModels;
using Festispec.WebApplication2.Controllers;
using NUnit.Framework;
using System;

namespace Festispec.Test.WebControllers
{
    [TestFixture]
    public class ProfileControllerTests
    {
        [Test]
        public void Index_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var profileController = new ProfileController();

            // Act
            var result = profileController.Index();

            // Assert
            Assert.Fail();
        }

        [Test]
        public void Index_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            var profileController = new ProfileController();
            UserViewModel model = null;

            // Act
            var result = profileController.Index(
                model);

            // Assert
            Assert.Fail();
        }
    }
}
