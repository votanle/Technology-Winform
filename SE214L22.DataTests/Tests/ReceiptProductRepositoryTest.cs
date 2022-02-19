using NUnit.Framework;
using SE214L22.Data.Entity.AppCustomer;
using SE214L22.Data.Entity.AppProduct;
using SE214L22.Data.Entity.Others;
using SE214L22.Data.Repository;
using SE214L22.Data.Repository.AggregateDto;
using SE214L22.Shared.AppConsts;
using SE214L22.Shared.Dtos;
using SE214L22.Shared.Helpers;
using SE214L22.Shared.Pagination;
using System;
using System.Collections.Generic;

namespace SE214L22.DataTests.Tests
{
    [TestFixture]
    public class ReceiptProductRepositoryTest
    {
        private ReceiptProduct GenerateInput(bool generateId = false, int? id = null)
        {
            var input = new ReceiptProduct
            {
                Number = 10,
                PriceIn = 1_111_111,
                ProductId = 1,
                ReceiptId = 1
            };
            if (generateId) input.Id = 111_111_111;
            if (id != null) input.Id = (int)id;
            return input;
        }

        private bool CompareProperties(ReceiptProduct expected, ReceiptProduct actual)
        {
            return
            (
                expected.Number == actual.Number &&
                expected.PriceIn == actual.PriceIn &&
                expected.ProductId == actual.ProductId &&
                expected.ReceiptId == actual.ReceiptId
            );
        }

        // Basic
        [Test]
        public void Get_Success_ReturnEntity()
        {
            // Arrange
            var repository = new ReceiptProductRepository();
            var input = repository.Create(GenerateInput());

            // Act
            var result = repository.Get(input.Id);

            // Assert
            Assert.IsInstanceOf<ReceiptProduct>(result);
        }

        [Test]
        public void Get_Fail_ReturnNull()
        {
            // Arrange
            var repository = new ReceiptProductRepository();

            // Act
            var result = repository.Get(111_111_111);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void Create_Success_ReturnEntity()
        {
            // Arrange
            var repository = new ReceiptProductRepository();
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
            var repository = new ReceiptProductRepository();
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
            var repository = new ReceiptProductRepository();
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
            var repository = new ReceiptProductRepository();
            var input = repository.Create(GenerateInput());

            // Act
            repository.Delete(input.Id);
            var result = repository.Get(input.Id);

            // Assert
            Assert.IsNull(result);
        }

        // Additional
        [Test]
        public void GetAllByReceiptId_Success_ReturnEntities()
        {
            // Arrange
            var repository = new ReceiptProductRepository();
            var input = repository.Create(GenerateInput());

            // Act
            var result = repository.GetAllByReceiptId(1);

            // Assert
            Assert.IsInstanceOf<IEnumerable<ReceiptProduct>>(result);
        }
    }
}
