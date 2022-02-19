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
    public class InvoiceRepositoryTest
    {
        private Invoice GenerateInput(bool generateId = false, int? id = null)
        {
            var input = new Invoice
            {
                CreationTime = DateTime.Now,
                CustomerId = 1,
                Discount = 0,
                Price = 1_111_111,
                Total = 1_111_11,
                UserId = 1
            };
            if (generateId) input.Id = 111_111_111;
            if (id != null) input.Id = (int)id;
            return input;
        }

        private bool CompareProperties(Invoice expected, Invoice actual)
        {
            return
            (
                expected.CreationTime == actual.CreationTime &&
                expected.CustomerId == actual.CustomerId &&
                expected.Discount == actual.Discount &&
                expected.Price == actual.Price &&
                expected.Total == actual.Total &&
                expected.UserId == actual.UserId
            );
        }

        // Basic
        [Test]
        public void Get_Success_ReturnEntity()
        {
            // Arrange
            var repository = new InvoiceRepository();
            var input = repository.Create(GenerateInput());

            // Act
            var result = repository.Get(input.Id);

            // Assert
            Assert.IsInstanceOf<Invoice>(result);
        }

        [Test]
        public void Get_Fail_ReturnNull()
        {
            // Arrange
            var repository = new InvoiceRepository();

            // Act
            var result = repository.Get(111_111_111);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void Create_Success_ReturnEntity()
        {
            // Arrange
            var repository = new InvoiceRepository();
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
            var repository = new InvoiceRepository();
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
            var repository = new InvoiceRepository();
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
            var repository = new InvoiceRepository();
            var input = repository.Create(GenerateInput());

            // Act
            repository.Delete(input.Id);
            var result = repository.Get(input.Id);

            // Assert
            Assert.IsNull(result);
        }

        // Additional
        [Test]
        public void GetSalesByDay_Success_ReturnInt()
        {
            // Arrange
            var repository = new InvoiceRepository();

            // Act
            var result = repository.GetSalesByDay(DateTime.Now);

            // Assert
            Assert.That(result >= 0);
        }

        [Test]
        public void GetSalesByMonth_Success_ReturnInt()
        {
            // Arrange
            var repository = new InvoiceRepository();

            // Act
            var result = repository.GetSalesByMonth(DateTime.Now);

            // Assert
            Assert.That(result >= 0);
        }
    }
}
