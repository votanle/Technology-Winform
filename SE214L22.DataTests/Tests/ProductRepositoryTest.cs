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
    public class ProductRepositoryTest
    {
        private Product GenerateInput(bool generateId = false, int? id = null)
        {
            var input = new Product
            {
                CategoryId = 1,
                isDelete = 0,
                Number = 0,
                ManufacturerId = 1,
                Name = nameof(Product) + Helper.RandomString(6),
                Photo = DefaultPhotoNames.Product,
                Status = (int)ProductStatus.Available,
                WarrantyPeriod = 12
            };
            if (generateId) input.Id = 111_111_111;
            if (id != null) input.Id = (int)id;
            return input;
        }

        private bool CompareProperties(Product expected, Product actual)
        {
            return
            (
                expected.CategoryId == actual.CategoryId &&
                expected.isDelete == actual.isDelete &&
                expected.Number == actual.Number &&
                expected.ManufacturerId == actual.ManufacturerId &&
                expected.Name == actual.Name &&
                expected.Status == actual.Status &&
                expected.Photo == actual.Photo &&
                expected.WarrantyPeriod == actual.WarrantyPeriod
            );
        }

        // Basic
        [Test]
        public void Get_Success_ReturnEntity()
        {
            // Arrange
            var repository = new ProductRepository();
            var input = repository.Create(GenerateInput());

            // Act
            var result = repository.Get(input.Id);

            // Assert
            Assert.IsInstanceOf<Product>(result);
        }

        [Test]
        public void Get_Fail_ReturnNull()
        {
            // Arrange
            var repository = new ProductRepository();

            // Act
            var result = repository.Get(111_111_111);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void Create_Success_ReturnEntity()
        {
            // Arrange
            var repository = new ProductRepository();
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
            var repository = new ProductRepository();
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
            var repository = new ProductRepository();
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
            var repository = new ProductRepository();
            var input = repository.Create(GenerateInput());

            // Act
            repository.Delete(input.Id);
            var result = repository.Get(input.Id);

            // Assert
            Assert.IsNull(result);
        }

        // Additional
        [Test]
        public void GetProductsOrderBySales_Success_ReturnEntities()
        {
            // Arrange
            var repository = new ProductRepository();

            // Act
            var result = repository.GetProductsOrderBySales(DateTime.Now);

            // Assert
            Assert.IsInstanceOf<IEnumerable<ProductAggregateDto>>(result);
        }

        [Test]
        public void UpdateSaleProperty_Success_ReturnTrue()
        {
            // Arrange
            var repository = new ProductRepository();
            var input = repository.Create(GenerateInput());

            var number = 11;
            var priceIn = 1_000_000;

            // Act
            repository.UpdateSaleProperty(input.Id, number, priceIn);
            var result = repository.Get(input.Id);

            // Assert
            Assert.That(result != null && result.Number == number && result.PriceIn == priceIn);
        }

        [Test]
        public void GetProducts_Success_ReturnEntities()
        {
            // Arrange
            var repository = new ProductRepository();

            // Act
            var result = repository.GetProducts(1, 10);

            // Assert
            Assert.IsInstanceOf<PaginatedList<Product>>(result);
        }

        [Test]
        public void GetProductsForImport_Success_ReturnEntities()
        {
            // Arrange
            var repository = new ProductRepository();
            
            // Act
            var result = repository.GetProductsForImport("sanpham 1", 1, 10);

            // Assert
            Assert.IsInstanceOf<PaginatedList<Product>>(result);
        }

        [Test]
        public void UpdateNumberById_Success_ReturnTrue()
        {
            // Arrange
            var repository = new ProductRepository();
            var input = repository.Create(GenerateInput());

            var number = 10;

            // Act
            repository.UpdateNumberById(input.Id, number);
            var result = repository.Get(input.Id);

            // Assert
            Assert.That(result != null && result.Number == input.Number - number);
        }

        [Test]
        public void GetProductPhotoById_Success_ReturnString()
        {
            // Arrange
            var repository = new ProductRepository();
            var input = repository.Create(GenerateInput());

            // Act
            var result = repository.GetProductPhotoById(input.Id);

            // Assert
            Assert.IsInstanceOf<string>(result);
        }
    }
}
