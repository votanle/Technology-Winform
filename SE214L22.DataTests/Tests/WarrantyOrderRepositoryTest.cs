using NUnit.Framework;
using SE214L22.Data.Entity.AppCustomer;
using SE214L22.Data.Entity.AppProduct;
using SE214L22.Data.Entity.AppUser;
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
    public class WarrantyOrderRepositoryTest
    {
        private WarrantyOrder GenerateInput(bool generateId = false, int? id = null)
        {
            var input = new WarrantyOrder
            {
                InvoiceId = 1,
                CustomerId = 1,
                ProductId = 1,
                CreationTime = DateTime.Now,
                Status = (int)WarrantyOrderStatus.WaitForSent,
            };
            if (generateId) input.Id = 111_111_111;
            if (id != null) input.Id = (int)id;
            return input;
        }

        private bool CompareProperties(WarrantyOrder expected, WarrantyOrder actual)
        {
            return
            (
                expected.InvoiceId == actual.InvoiceId &&
                expected.CustomerId == actual.CustomerId &&
                expected.ProductId == actual.ProductId &&
                expected.CreationTime == actual.CreationTime &&
                expected.Status == actual.Status
            );
        }

        // Basic
        [Test]
        public void Get_Success_ReturnEntity()
        {
            // Arrange
            var repository = new WarrantyOrderRepository();
            var input = repository.Create(GenerateInput());

            // Act
            var result = repository.Get(input.Id);

            // Assert
            Assert.IsInstanceOf<WarrantyOrder>(result);
        }

        [Test]
        public void Get_Fail_ReturnNull()
        {
            // Arrange
            var repository = new WarrantyOrderRepository();

            // Act
            var result = repository.Get(111_111_111);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void Create_Success_ReturnEntity()
        {
            // Arrange
            var repository = new WarrantyOrderRepository();
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
            var repository = new WarrantyOrderRepository();
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
            var repository = new WarrantyOrderRepository();
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
            var repository = new WarrantyOrderRepository();
            var input = repository.Create(GenerateInput());

            // Act
            repository.Delete(input.Id);
            var result = repository.Get(input.Id);

            // Assert
            Assert.IsNull(result);
        }

        // Additional
        [Test]
        public void GetAllWithStatusFilter_Success_ReturnEntities()
        {
            // Arrange
            var repository = new WarrantyOrderRepository();
            var filter = new List<WarrantyOrderStatus>()
            {
                WarrantyOrderStatus.WaitForSent,
                WarrantyOrderStatus.Sent,
                WarrantyOrderStatus.WaitForCustomer,
                WarrantyOrderStatus.Done,
            };
            // Act
            var result = repository.GetAllWithStatusFilter(filter);

            // Assert
            Assert.IsInstanceOf<IEnumerable<WarrantyOrder>>(result);
        }

        [Test]
        public void GetNumberOfWarrantyOrderByInvoiceIdAndProductId_Success_ReturnInt()
        {
            // Arrange
            var repository = new WarrantyOrderRepository();
            
            // Act
            var result = repository.GetNumberOfWarrantyOrderByInvoiceIdAndProductId(1, 1);

            // Assert
            Assert.That(result >= 0);
        }
    }
}
