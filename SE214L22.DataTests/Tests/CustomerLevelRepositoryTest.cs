using NUnit.Framework;
using SE214L22.Data.Entity.AppCustomer;
using SE214L22.Data.Entity.AppProduct;
using SE214L22.Data.Repository;
using SE214L22.Shared.Helpers;
using System.Collections.Generic;

namespace SE214L22.DataTests.Tests
{
    public class CustomerLevelRepositoryTest
    {
        // Basic
        [Test]
        public void Get_Success_ReturnCategory()
        {
            // Arrange
            var repository = new CustomerLevelRepository();
            var input = repository.Create(new CustomerLevel
            {
                Name = "CustomerLevel-" + Helper.RandomString(6),
                Description = "CustomerLevel-" + Helper.RandomString(6),
            });

            // Act
            var result = repository.Get(input.Id);

            // Assert
            Assert.IsInstanceOf<CustomerLevel>(result);
        }

        [Test]
        public void Get_Fail_ReturnNull()
        {
            // Arrange
            var repository = new CustomerLevelRepository();

            // Act
            var result = repository.Get(111_111_111);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void Create_Success_ReturnCategory()
        {
            // Arrange
            var repository = new CustomerLevelRepository();
            var input = new CustomerLevel
            {
                Name = "category-" + Helper.RandomString(6),
                Description = "category-" + Helper.RandomString(6),
            };

            // Act
            var result = repository.Create(input);

            // Assert
            Assert.That(result.Name == input.Name && result.Description == result.Description);
        }

        [Test]
        public void Update_Success_ReturnTrue()
        {
            // Arrange
            var repository = new CustomerLevelRepository();
            var input = repository.Create(new CustomerLevel
            {
                Name = "CustomerLevel-" + Helper.RandomString(6),
                Description = "CustomerLevel-" + Helper.RandomString(6),
            });

            var inputForUpdate = new CustomerLevel
            {
                Id = input.Id,
                Name = "CustomerLevel-update-" + Helper.RandomString(6),
                Description = "CustomerLevel-update-" + Helper.RandomString(6),
            };

            // Act
            var result = repository.Update(inputForUpdate);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Update_Success_ReturnFalse()
        {
            // Arrange
            var repository = new CustomerLevelRepository();
            var input = new CustomerLevel
            {
                Id = 111_111_111,
                Name = "category-" + Helper.RandomString(6),
                Description = "category-" + Helper.RandomString(6),
            };

            // Act
            var result = repository.Update(input);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Delete_Success_ReturnNull()
        {
            // Arrange
            var repository = new CustomerLevelRepository();
            var input = repository.Create(new CustomerLevel
            {
                Name = "category-" + Helper.RandomString(6),
                Description = "category-" + Helper.RandomString(6),
            });

            // Act
            repository.Delete(input.Id);
            var result = repository.Get(input.Id);
            // Assert
            Assert.IsNull(result);
        }

        // Additional
        [Test]
        public void GetCustomerLevels_Success_ReturnCustomerLevelsList()
        {
            // Arrange
            var repository = new CustomerLevelRepository();

            // Act
            var result = repository.GetCustomerLevels();

            // Assert
            Assert.IsInstanceOf<IEnumerable<CustomerLevel>>(result);
        }

        [Test]
        public void GetCustomerLevelByName_Success_ReturnCustomerLevel()
        {
            // Arrange
            var repository = new CustomerLevelRepository();
            var input = repository.Create(new CustomerLevel
            {
                Name = "category-" + Helper.RandomString(6),
                Description = "category-" + Helper.RandomString(6),
            });

            // Act
            var result = repository.GetCustomerLevelByName(input.Name);

            // Assert
            Assert.IsInstanceOf<CustomerLevel>(result);
        }

        [Test]
        public void GetCustomerLevelByName_Fail_ReturnNull()
        {
            // Arrange
            var repository = new CustomerLevelRepository();
            var name = Helper.RandomString(9);

            // Act
            var result = repository.GetCustomerLevelByName(name);

            // Assert
            Assert.IsNull(result);
        }
    }
}
