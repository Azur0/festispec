using Festispec.WebApplication2.Controllers;
using NUnit.Framework;
using System.Web.Mvc;

namespace Festispec.Test.WebControllers
{
    [TestFixture]
    public class HomeControllerTests
    {
        private HomeController _homeController;

        [SetUp]
        public void Setup()
        {
            // Arrange
            _homeController = new HomeController();    
        }
        [Test]
        public void Index_StateUnderTest_ExpectedBehavior()
        {
            // Act
            var result = _homeController.Index();

            // Assert
            Assert.IsInstanceOf(typeof(ViewResult), result);
        }
    }
}
