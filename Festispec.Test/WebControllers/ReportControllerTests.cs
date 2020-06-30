using Festispec.WebApplication2.Controllers;
using Festispec.WebApplication2.ViewModels;
using NUnit.Framework;
using System;

namespace Festispec.Test.WebControllers
{
    [TestFixture]
    public class ReportControllerTests
    {
        [Test]
        public void Index_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var reportController = new ReportController();
            int id = 0;
            ReportViewModel model = null;

            // Act
            var result = reportController.Index(
                id,
                model);

            // Assert
            Assert.Fail();
        }
    }
}
