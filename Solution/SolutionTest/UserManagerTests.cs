using BusinessLogic.Entities;
using BusinessLogic.Enums;
using BusinessLogic.Managers;
using SolutionTest.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionTest
{
    [TestClass]
    public class UserManagerTests
    {
        private UserManager _userManager;
        private FakeUserMediator _fakeUserMediator;

        [TestInitialize]
        public void Setup()
        {
            _fakeUserMediator = new FakeUserMediator();
            _userManager = new UserManager(_fakeUserMediator);
        }

        [TestMethod]
        public void AddUser_ShouldAddUserSuccessfully()
        {
            // Arrange
            var user = new User
            {
                User_Id = 1,
                UserName = "testuser",
                FirstName = "Test",
                LastName = "User",
                Email = "testuser@example.com",
                Password = "password123",
                DateOfBirth = new DateTime(1990, 1, 1),
                Role = UserRole.Customer,
                PhoneNumber = "1234567890",
                Address = "123 Test St"
            };

            // Act
            _userManager.AddUser(user);

            // Assert
            var addedUser = _fakeUserMediator.GetUserById(1);
            Assert.IsNotNull(addedUser);
            Assert.AreEqual(user.UserName, addedUser.UserName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddUser_ShouldThrowException_WhenUserAlreadyExists()
        {
            // Arrange
            var user = new User
            {
                User_Id = 1,
                UserName = "testuser",
                FirstName = "Test",
                LastName = "User",
                Email = "testuser@example.com",
                Password = "password123",
                DateOfBirth = new DateTime(1990, 1, 1),
                Role = UserRole.Customer,
                PhoneNumber = "1234567890",
                Address = "123 Test St"
            };

            _fakeUserMediator.AddUser(user);

            // Act
            _userManager.AddUser(user);
        }

        [TestMethod]
        public void UpdateUser_ShouldUpdateUserSuccessfully()
        {
            // Arrange
            var user = new User
            {
                User_Id = 1,
                UserName = "testuser",
                FirstName = "Test",
                LastName = "User",
                Email = "testuser@example.com",
                Password = "password123",
                DateOfBirth = new DateTime(1990, 1, 1),
                Role = UserRole.Customer,
                PhoneNumber = "1234567890",
                Address = "123 Test St"
            };
            _fakeUserMediator.AddUser(user);

            var updatedUser = new User
            {
                User_Id = 1,
                UserName = "updateduser",
                FirstName = "Updated",
                LastName = "User",
                Email = "updateduser@example.com",
                Password = "newpassword",
                DateOfBirth = new DateTime(1990, 1, 1),
                Role = UserRole.StaffMember,
                PhoneNumber = "9876543210",
                Address = "456 Updated St"
            };

            // Act
            _userManager.UpdateUser(updatedUser);

            // Assert
            var retrievedUser = _fakeUserMediator.GetUserById(1);
            Assert.IsNotNull(retrievedUser);
            Assert.AreEqual("updateduser", retrievedUser.UserName);
            Assert.AreEqual(UserRole.StaffMember, retrievedUser.Role);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void UpdateUser_ShouldThrowException_WhenUserDoesNotExist()
        {
            // Arrange
            var updatedUser = new User
            {
                User_Id = 1,
                UserName = "nonexistentuser",
                FirstName = "Nonexistent",
                LastName = "User",
                Email = "nonexistent@example.com",
                Password = "password",
                DateOfBirth = new DateTime(1990, 1, 1),
                Role = UserRole.Customer,
                PhoneNumber = "0000000000",
                Address = "Nonexistent Address"
            };

            // Act
            _userManager.UpdateUser(updatedUser);
        }

        [TestMethod]
        public void DeleteUser_ShouldRemoveUserSuccessfully()
        {
            // Arrange
            var user = new User
            {
                User_Id = 1,
                UserName = "testuser",
                FirstName = "Test",
                LastName = "User",
                Email = "testuser@example.com",
                Password = "password123",
                DateOfBirth = new DateTime(1990, 1, 1),
                Role = UserRole.Customer,
                PhoneNumber = "1234567890",
                Address = "123 Test St"
            };
            _fakeUserMediator.AddUser(user);

            // Act
            _userManager.DeleteUser(1);

            // Assert
            Assert.ThrowsException<KeyNotFoundException>(() => _fakeUserMediator.GetUserById(1));
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void DeleteUser_ShouldThrowException_WhenUserDoesNotExist()
        {
            // Act
            _userManager.DeleteUser(999);
        }

        [TestMethod]
        public void GetUserById_ShouldReturnCorrectUser()
        {
            // Arrange
            var user = new User
            {
                User_Id = 1,
                UserName = "testuser",
                FirstName = "Test",
                LastName = "User",
                Email = "testuser@example.com",
                Password = "password123",
                DateOfBirth = new DateTime(1990, 1, 1),
                Role = UserRole.Customer,
                PhoneNumber = "1234567890",
                Address = "123 Test St"
            };
            _fakeUserMediator.AddUser(user);

            // Act
            var result = _userManager.GetUserById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("testuser", result.UserName);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void GetUserById_ShouldThrowException_WhenUserDoesNotExist()
        {
            // Act
            _userManager.GetUserById(999);
        }

        [TestMethod]
        public void GetUsersByRole_ShouldReturnCorrectUsers()
        {
            // Arrange
            var user1 = new User { User_Id = 1, UserName = "user1", Role = UserRole.Customer };
            var user2 = new User { User_Id = 2, UserName = "user2", Role = UserRole.Customer };
            var user3 = new User { User_Id = 3, UserName = "user3", Role = UserRole.StaffMember };

            _fakeUserMediator.AddUser(user1);
            _fakeUserMediator.AddUser(user2);
            _fakeUserMediator.AddUser(user3);

            // Act
            var result = _userManager.GetUsersByRole(UserRole.Customer.ToString());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Any(u => u.UserName == "user1"));
            Assert.IsTrue(result.Any(u => u.UserName == "user2"));
        }

        [TestMethod]
        public void GetUsersByRole_ShouldReturnEmpty_WhenNoUsersMatchRole()
        {
            // Arrange
            var user = new User { User_Id = 1, UserName = "user1", Role = UserRole.StaffMember };
            _fakeUserMediator.AddUser(user);

            // Act
            var result = _userManager.GetUsersByRole(UserRole.Customer.ToString());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }
    }
}
