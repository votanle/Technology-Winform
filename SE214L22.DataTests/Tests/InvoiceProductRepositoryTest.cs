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
    public class InvoiceProductRepositoryTest
    {
        private InvoiceProduct GenerateInput(bool generateId = false, int? id = null)
        {
            var input = new InvoiceProduct
            {
                Number = 1,
                ProductId = 1,
                InvoiceId = 1,
            };
            if (generateId) input.Id = 111_111_111;
            if (id != null) input.Id = (int)id;
            return input;
        }

        private bool CompareProperties(InvoiceProduct expected, InvoiceProduct actual)
        {
            return
            (
                expected.Number == actual.Number &&
                expected.ProductId == actual.ProductId &&
                expected.InvoiceId == actual.InvoiceId
            );
        }

        // Basic
        [Test]
        public void Get_Success_ReturnEntity()
        {
            // Arrange
            var repository = new InvoiceProductRepository();
            var input = repository.Create(GenerateInput());

            // Act
            var result = repository.Get(input.Id);

            // Assert
            Assert.IsInstanceOf<InvoiceProduct>(result);
        }

        [Test]
        public void Get_Fail_ReturnNull()
        {
            // Arrange
            var repository = new InvoiceProductRepository();

            // Act
            var result = repository.Get(111_111_111);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void Create_Success_ReturnEntity()
        {
            // Arrange
            var repository = new InvoiceProductRepository();
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
            var repository = new InvoiceProductRepository();
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
            var repository = new InvoiceProductRepository();
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
            var repository = new InvoiceProductRepository();
            var input = repository.Create(GenerateInput());

            // Act
            repository.Delete(input.Id);
            var result = repository.Get(input.Id);

            // Assert
            Assert.IsNull(result);
        }

        // Additional
        [Test]
        public void GetInvoiceProductsByCustomerPhoneNumber_Success_ReturnEntities()
        {
            // Arrange
            var repository = new InvoiceProductRepository();
            var phoneNumber = Helper.RandomNumber(10);

            // Act
            var result = repository.GetInvoiceProductsByCustomerPhoneNumber(phoneNumber);

            // Assert
            Assert.IsInstanceOf<IEnumerable<InvoiceProduct>>(result);
        }

        [Test]
        public void GetNumberOfProductByInvoiceId_Success_ReturnInt()
        {
            // Arrange
            var repository = new InvoiceProductRepository();

            // Act
            var result = repository.GetNumberOfProductByInvoiceId(1, 1);

            // Assert
            Assert.That(result >= 0);
        }
    }
}
