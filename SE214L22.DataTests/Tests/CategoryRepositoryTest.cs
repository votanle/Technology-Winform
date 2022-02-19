using NUnit.Framework;
using SE214L22.Data.Entity.AppProduct;
using SE214L22.Data.Repository;
using SE214L22.Shared.Helpers;
using System.Collections.Generic;

namespace SE214L22.DataTests.Tests
{
    [TestFixture]
    public class CategoryRepositoryTest
    {

        // Basic
        [Test]
        public void Get_Success_ReturnCategory()
        {
            // Arrange
            var repository = new CategoryRepository();
            var input = repository.Create(new Category
            {
                Name = "category-" + Helper.RandomString(6),
                Description = "category-" + Helper.RandomString(6),
            });

            // Act
            var result = repository.Get(input.Id);

            // Assert
            Assert.IsInstanceOf<Category>(result);
        }

        [Test]
        public void Get_Fail_ReturnNull()
        {
            // Arrange
            var repository = new CategoryRepository();

            // Act
            var result = repository.Get(111_111_111);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void Create_Success_ReturnCategory()
        {
            // Arrange
            var repository = new CategoryRepository();
            var input = new Category
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
            var repository = new CategoryRepository(); 
            var input = repository.Create(new Category
            {
                Name = "category-" + Helper.RandomString(6),
                Description = "category-" + Helper.RandomString(6),
            });

            var inputForUpdate = new Category
            {
                Id = input.Id,
                Name = "category-update-" + Helper.RandomString(6),
                Description = "category-update-" + Helper.RandomString(6),
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
            var repository = new CategoryRepository();
            var input = new Category
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
            var repository = new CategoryRepository();
            var input = repository.Create(new Category
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
        public void GetCategories_Success_ReturnCategoriesList()
        {
            // Arrange
            var repository = new CategoryRepository();

            // Act
            var result = repository.GetCategories();

            // Assert
            Assert.IsInstanceOf<IEnumerable<Category>>(result);
        }
    }
}
