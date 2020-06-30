using Festispec.WebApplication2.Controllers;
using NUnit.Framework;
using System;
using System.Web.Mvc;

namespace Festispec.Test.WebControllers
{
    [TestFixture]
    public class AvailabilityControllerTests
    {
        private AvailabilityController _availabilityController;

        [SetUp]
        public void Setup()
        {
            // Arrange
            _availabilityController = new AvailabilityController();
        }


        [Test]
        public void Index_StateUnderTest_ExpectedBehavior()
        {
            // Act
            var result = _availabilityController.Index();

            // Assert
            Assert.IsInstanceOf(typeof(ViewResult), result);
        }

        [Test]
        public void Index_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            DateTime dateTime = default;

            // Act
            var result = _availabilityController.Index(dateTime);

            // Assert
            Assert.IsInstanceOf(typeof(ViewResult), result);
        }
    }
}
