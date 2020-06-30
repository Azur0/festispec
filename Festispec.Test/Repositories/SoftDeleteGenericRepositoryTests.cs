using FestiSpec.SharedCode.Repositories;
using NUnit.Framework;
using SharedCode.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Festispec.Test.Repositories
{
    [TestFixture]
    public class SoftDeleteGenericRepositoryTests
    {
        [Test]
        public void Delete_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            UnitOfWork uow = new UnitOfWork();
            var result = uow.CustomerRepository.Get().First();
            uow.Dispose();

            // Act
            result.Delete();

            // Assert
            Assert.IsNotNull(result.DeletedAt);
        }

        [Test]
        [TestCase(1)]
        [TestCase(15)]
        [TestCase(999999999)]
        [TestCase(-1)]
        public void Get_ExpectedBehavior(int id)
        {
            // Arrange
            UnitOfWork uow = new UnitOfWork();

            // Act & Assert
            Assert.DoesNotThrow(() => { var result = uow.CustomerRepository.Get(x => x.Id == id); });
            uow.Dispose();
        }

        [Test]
        public void GetPaginable_BypassUnitOfWork_Expectedehavior()
        {
            // Arrange
            FestispecContext context = new FestispecContext();
            var softDeleteGenericRepository = new SoftDeleteGenericRepository<Customer>(context);
            int pageAmount = 10;
            int currentPage = 0;

            // Act
            var result = softDeleteGenericRepository.GetPaginable(
                pageAmount,
                currentPage);

            // Assert
            Assert.IsTrue(result.ToList().Count() <= pageAmount);
        }

        [Test]
        public void GetIncludes_StateUnderTest_ExpectedBehavior()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                // Arrange
                string children = "Location";

                // Act
                var result = uow.UserRepository
                    .GetIncludes(children)
                    .First();
                
                // Assert
                Assert.IsNotNull(result.Location);
            }
        }
    }
}
