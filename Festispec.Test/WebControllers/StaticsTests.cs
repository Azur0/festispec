using Festispec.WebApplication2.Controllers;
using NUnit.Framework;
using System;

namespace Festispec.Test.WebControllers
{
    [TestFixture]
    public class StaticsTests
    {
        [Test]
        public void SHA256_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            string text = null;

            // Act
            var result = Statics.SHA256(
                text);

            // Assert
            Assert.Fail();
        }
    }
}
