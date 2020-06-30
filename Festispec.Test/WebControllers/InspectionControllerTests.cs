using Festispec.WebApplication2.Controllers;
using NUnit.Framework;
using System;
using Festispec.WebApplication.ViewModels;
using System.Web.Mvc;

namespace Festispec.Test.WebControllers
{
    [TestFixture]
    public class InspectionControllerTests
    {
        [Test]
        public void Index_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var inspectionController = new InspectionController();
            InspectionFormViewModel model = null;

            // Act
            var result = inspectionController.Index(
                model);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void Overview_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var inspectionController = new InspectionController();
            InspectionFormViewModel model = null;

            // Act
            var result = inspectionController.Overview(
                model);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void Overview_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            var inspectionController = new InspectionController();
            InspectionFormViewModel model = null;
            FormCollection formValues = null;

            // Act
            var result = inspectionController.Overview(
                model,
                formValues);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void Index_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            var inspectionController = new InspectionController();
            FormCollection form = null;
            InspectionFormViewModel model = null;

            // Act
            var result = inspectionController.Index(
                form,
                model);

            // Assert
            Assert.Fail();
        }
    }
}
