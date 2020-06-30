using Festispec.WebApplication.ViewModels;
using Festispec.WebApplication2.Controllers;
using NUnit.Framework;
using System;

namespace Festispec.Test.WebControllers
{
    [TestFixture]
    public class LocationControllerTests
    {
        [Test]
        public void Index_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var locationController = new LocationController();

            // Act
            var result = locationController.Index();

            // Assert
            Assert.Fail();
        }

        [Test]
        public void Index_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            var locationController = new LocationController();
            LocationViewModel model = null;

            // Act
            var result = locationController.Index(
                model);

            // Assert
            Assert.Fail();
        }
    }
}
