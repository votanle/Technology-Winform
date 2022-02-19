using NUnit.Framework;
using SE214L22.Data.Entity.AppCustomer;
using SE214L22.Data.Entity.AppProduct;
using SE214L22.Data.Repository;
using SE214L22.Shared.Helpers;
using SE214L22.Shared.Pagination;
using System;
using System.Collections.Generic;

namespace SE214L22.DataTests.Tests
{
    [TestFixture]
    public class ManufacturerRepositoryTest
    {
        private Manufacturer GenerateInput(bool generateId = false, int? id = null)
        {
            var input = new Manufacturer
            {
                Name = nameof(Manufacturer) + Helper.RandomString(6),
                Description = nameof(Manufacturer) + Helper.RandomString(6)
            };
            if (generateId) input.Id = 111_111_111;
            if (id != null) input.Id = (int)id;
            return input;
        }

        private bool CompareProperties(Manufacturer expected, Manufacturer actual)
        {
            return
            (
                expected.Name == actual.Name &&
                expected.Description == actual.Description
            );
        }

        // Basic
        [Test]
        public void Get_Success_ReturnEntity()
        {
            // Arrange
            var repository = new ManufacturerRepository();
            var input = repository.Create(GenerateInput());

            // Act
            var result = repository.Get(input.Id);

            // Assert
            Assert.IsInstanceOf<Manufacturer>(result);
        }

        [Test]
        public void Get_Fail_ReturnNull()
        {
            // Arrange
            var repository = new ManufacturerRepository();

            // Act
            var result = repository.Get(111_111_111);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void Create_Success_ReturnEntity()
        {
            // Arrange
            var repository = new ManufacturerRepository();
            var input = GenerateInput();

            // Act
            var result = repository.Create(input);

            // Assert
            Assert.That(CompareProperties(input, result));
        }

        [Test]
        public void Update_Success_ReturnTrue()
        {
            // Arrange
            var repository = new ManufacturerRepository();
            var input = repository.Create(GenerateInput());

            var inputForUpdate = GenerateInput(id: input.Id);

            // Act
            var result = repository.Update(inputForUpdate);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Update_Success_ReturnFalse()
        {
            // Arrange
            var repository = new ManufacturerRepository();
            var input = GenerateInput(generateId: true);

            // Act
            var result = repository.Update(input);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Delete_Success_ReturnNull()
        {
            // Arrange
            var repository = new ManufacturerRepository();
            var input = repository.Create(GenerateInput());

            // Act
            repository.Delete(input.Id);
            var result = repository.Get(input.Id);

            // Assert
            Assert.IsNull(result);
        }

        // Additional
        [Test]
        public void GetManufacturers_Success_ReturnEntities()
        {
            // Arrange
            var repository = new ManufacturerRepository();

            // Act
            var result = repository.GetManufacturers();

            // Assert
            Assert.IsInstanceOf<IEnumerable<Manufacturer>>(result);
        }
    }
}
