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
    public class UserRepositoryTest
    {
        private User GenerateInput(bool generateId = false, int? id = null)
        {
            var input = new User
            {
                Name = nameof(User) + Helper.RandomString(6),
                Email = Helper.RandomString(6) + "@gmail.com",
                CreationTime = DateTime.Now,
                IsDeleted = false,
                Password = Helper.HashPassword(Helper.RandomNumber(6)),
                Address = Helper.RandomString(6),
                PhoneNumber = Helper.RandomNumber(10),
                Photo = DefaultPhotoNames.User,
                Dob = DateTime.Now.AddDays(-(365*20)),
                RoleId = 1
            };
            if (generateId) input.Id = 111_111_111;
            if (id != null) input.Id = (int)id;
            return input;
        }

        private bool CompareProperties(User expected, User actual)
        {
            return
            (
                expected.Name == actual.Name &&
                expected.Email == actual.Email &&
                expected.CreationTime == actual.CreationTime &&
                expected.IsDeleted == actual.IsDeleted &&
                expected.Password == actual.Password &&
                expected.Address == actual.Address &&
                expected.PhoneNumber == actual.PhoneNumber &&
                expected.Photo == actual.Photo &&
                expected.Dob == actual.Dob &&
                expected.RoleId == actual.RoleId
            );
        }

        // Basic
        [Test]
        public void Get_Success_ReturnEntity()
        {
            // Arrange
            var repository = new UserRepository();
            var input = repository.Create(GenerateInput());

            // Act
            var result = repository.Get(input.Id);

            // Assert
            Assert.IsInstanceOf<User>(result);
        }

        [Test]
        public void Get_Fail_ReturnNull()
        {
            // Arrange
            var repository = new UserRepository();

            // Act
            var result = repository.Get(111_111_111);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void Create_Success_ReturnEntity()
        {
            // Arrange
            var repository = new UserRepository();
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
            var repository = new UserRepository();
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
            var repository = new UserRepository();
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
            var repository = new UserRepository();
            var input = repository.Create(GenerateInput());

            // Act
            repository.Delete(input.Id);
            var result = repository.Get(input.Id);

            // Assert
            Assert.IsNull(result);
        }

        // Additional
        [Test]
        public void GetAllUsers_Success_ReturnNull()
        {
            // Arrange
            var repository = new UserRepository();

            // Act
            var result = repository.GetAllUsers();

            // Assert
            Assert.IsInstanceOf<IEnumerable<User>>(result);
        }

        [Test]
        public void GetUserByEmail_Success_ReturnEntity()
        {
            // Arrange
            var repository = new UserRepository();
            var input = repository.Create(GenerateInput());

            // Act
            var result = repository.GetUserByEmail(input.Email);

            // Assert
            Assert.IsInstanceOf<User>(result);
        }

        [Test]
        public void GetUserByEmail_Success_ReturnNull()
        {
            // Arrange
            var repository = new UserRepository();

            // Act
            var result = repository.GetUserByEmail(Helper.RandomString(6) + "@gmail.com");

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void UpdateUserPassword_Success_ReturnTrue()
        {
            // Arrange
            var repository = new UserRepository();
            var input = repository.Create(GenerateInput());

            var passwordForUpdate = Helper.HashPassword(Helper.RandomString(6));

            // Act
            repository.UpdateUserPassword(input.Id, passwordForUpdate);
            var result = repository.Get(input.Id);

            // Assert
            Assert.That(result != null && result.Password == passwordForUpdate);
        }

        [Test]
        public void CountUsers_Success_ReturnInt()
        {
            // Arrange
            var repository = new UserRepository();

            // Act
            var result = repository.CountUsers();

            // Assert
            Assert.That(result >= 0);
        }

        [Test]
        public void GetUserPhotoById_Success_ReturnString()
        {
            // Arrange
            var repository = new UserRepository();
            var input = repository.Create(GenerateInput());

            // Act
            var result = repository.GetUserPhotoById(input.Id);

            // Assert
            Assert.IsInstanceOf<string>(result);
        }

        [Test]
        public void GetUserPhotoById_Success_ReturnNull()
        {
            // Arrange
            var repository = new UserRepository();

            // Act
            var result = repository.GetUserPhotoById(111_111_111);

            // Assert
            Assert.IsNull(result);
        }
    }
}
