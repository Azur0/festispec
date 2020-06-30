using FestiSpec.SharedCode.Repositories;
using NUnit.Framework;
using SharedCode.Models;

namespace Festispec.Test.Repositories
{
    [TestFixture]
    public class UnitOfWorkTests
    {
        /// NOT POSSIBLE:
        ///   No ability to assert if context is being used as festispeccontext has no overrides of savecontext.
        ///
        //[Test]
        //public void Save_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    using (UnitOfWork unitOfWork = new UnitOfWork())
        //    {

        //        // Act
        //        unitOfWork.Save();
        //    }

        //    // Assert
        //    Assert.Fail();
        //}


        ///
        /// Tests of Unit Of Work:
        /// 
        /// purpose is to make clear that there is a distinct difference between
        /// disposing and setting an object to null;
        ///
        /// Clear explanation:
        /// https://stackoverflow.com/questions/574019/setting-an-object-to-null-vs-dispose
        ///

        [Test]
        public void Dispose_ManualDispose_ExpectedBehavior()
        {
            // Arrange
            UnitOfWork unitOfWork = new UnitOfWork();

            // Act
            unitOfWork.Dispose();
            unitOfWork = null;

            // Assert
            Assert.IsNotNull(unitOfWork);
        }
        [Test]
        public void Dispose_UsingDispose_ExpectedBehavior()
        {
            UnitOfWork unitOfWork;
            // Arrange
            using (unitOfWork = new UnitOfWork())
            {
                //Possible code...
            }
            // Assert
            Assert.IsNotNull(unitOfWork);
        }

    }
}
