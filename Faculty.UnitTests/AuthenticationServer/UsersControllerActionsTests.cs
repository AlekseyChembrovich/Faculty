using Moq;
using Xunit;
using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using Xunit.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Faculty.AuthenticationServer.Tools;
using Faculty.AuthenticationServer.Models;
using Faculty.AuthenticationServer.Controllers;
using Faculty.AuthenticationServer.Models.User;

namespace Faculty.UnitTests.AuthenticationServer
{
    public class UsersControllerActionsTests
    {
        private readonly ITestOutputHelper _output;
        private readonly Mock<UserManager<CustomUser>> _mockUserManager;
        private readonly Mock<RoleManager<IdentityRole>> _mockRoleManager;
        private readonly Mock<IPasswordValidator<CustomUser>> _mockPasswordValidator;
        private readonly Mock<IPasswordHasher<CustomUser>> _mockPasswordHasher;
        private readonly AuthOptions _authOptions;

        public UsersControllerActionsTests(ITestOutputHelper output)
        {
            var mockUserStore = Mock.Of<IUserStore<CustomUser>>();
            _mockUserManager = new Mock<UserManager<CustomUser>>(mockUserStore, null, null, null, null, null, null, null, null);
            var mockRoleStore = Mock.Of<IRoleStore<IdentityRole>>();
            _mockRoleManager = new Mock<RoleManager<IdentityRole>>(mockRoleStore, null, null, null, null);
            _mockPasswordValidator = new Mock<IPasswordValidator<CustomUser>>();
            _mockPasswordHasher = new Mock<IPasswordHasher<CustomUser>>();
            var configuration = new ConfigurationBuilder().AddJsonFile(Path.Combine(Environment.CurrentDirectory, "appsettings.json")).Build();
            _authOptions = new AuthOptions(configuration);
            _output = output;
        }

        private static IQueryable<CustomUser> GetCustomUsers()
        {
            var models = new List<CustomUser>
            {
                new () { Id = "1", UserName = "User12345_1", Birthday = DateTime.Now.Date },
                new () { Id = "2", UserName = "User12345_2", Birthday = DateTime.Now.Date },
                new () { Id = "3", UserName = "User12345_3", Birthday = DateTime.Now.Date }
            };

            return models.AsQueryable();
        }

        private static IQueryable<IdentityRole> GetIdentityRoles()
        {
            var models = new List<IdentityRole>
            {
                new () { Id = "1", Name = "administrator" },
                new () { Id = "2", Name = "employee" }
            };

            return models.AsQueryable();
        }

        #region GetUsers

        [Fact]
        public void GetUsersMethod_ReturnsOkObjectResult_WithListOfUsersDisplay_WhenListHaveValues()
        {
            // Arrange
            var usersDisplay = new List<UserDisplay>
            {
                new () { Id = "1", Login = "User12345_1", Birthday = DateTime.Now.Date },
                new () { Id = "2", Login = "User12345_2", Birthday = DateTime.Now.Date },
                new () { Id = "3", Login = "User12345_3", Birthday = DateTime.Now.Date }
            };
            _mockUserManager.Setup(x => x.Users).Returns(GetCustomUsers());
            var usersController = new UsersController(_mockUserManager.Object, _mockRoleManager.Object, _authOptions);

            // Act
            var result = usersController.GetUsers().Result;

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(result);
            var listUserDisplay = Assert.IsType<List<UserDisplay>>(objectResult.Value);
            usersDisplay.Should().BeEquivalentTo(listUserDisplay,
                option => option
                    .Including(x => x.Id)
                    .Including(x => x.Login)
                    .Including(x => x.Birthday));
        }

        [Fact]
        public void GetUsersMethod_ReturnsNotFoundResult_WhenListHaveNoValues()
        {
            // Arrange
            var usersDisplay = new List<UserDisplay>
            {
                new () { Id = "1", Login = "User12345_1", Birthday = DateTime.Now.Date },
                new () { Id = "2", Login = "User12345_2", Birthday = DateTime.Now.Date },
                new () { Id = "3", Login = "User12345_3", Birthday = DateTime.Now.Date }
            };
            _mockUserManager.Setup(x => x.Users).Returns(new List<CustomUser>().AsQueryable());
            var usersController = new UsersController(_mockUserManager.Object, _mockRoleManager.Object, _authOptions);

            // Act
            var result = usersController.GetUsers().Result;

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        #endregion

        #region GetUser

        [Fact]
        public void GetUsersMethod_ReturnsOkObjectResult_WithModelOfUserDisplay_WhenModelWasFind()
        {
            // Arrange
            const string idExistedUser = "1";
            var customUser = new CustomUser
            {
                Id = "1",
                UserName = "User12345_1",
                Birthday = DateTime.Now.Date
            };
            var userDisplay = new UserDisplay
            {
                Id = customUser.Id,
                Login = customUser.UserName,
                Birthday = customUser.Birthday
            };
            _mockUserManager.Setup(x => x.FindByIdAsync(idExistedUser)).ReturnsAsync(customUser);
            var usersController = new UsersController(_mockUserManager.Object, _mockRoleManager.Object, _authOptions);

            // Act
            var result = usersController.GetUsers(idExistedUser).Result;

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(result.Result as OkObjectResult);
            var receiptsUserDisplay = Assert.IsType<UserDisplay>(objectResult.Value);
            userDisplay.Should().BeEquivalentTo(receiptsUserDisplay,
                option => option
                    .Including(x => x.Id)
                    .Including(x => x.Login)
                    .Including(x => x.Birthday));
        }

        [Fact]
        public void GetUsersMethod_ReturnsNotFoundResult_WhenModelWasNotFound()
        {
            // Arrange
            const string idNoExistedUser = "1";
            _mockUserManager.Setup(x => x.FindByIdAsync(idNoExistedUser)).ReturnsAsync(It.IsAny<CustomUser>());
            var usersController = new UsersController(_mockUserManager.Object, _mockRoleManager.Object, _authOptions);

            // Act
            var result = usersController.GetUsers().Result;

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        #endregion

        #region Create

        [Fact]
        public void CreateMethod_ReturnsCreatedAtActionResult_WhenSucceededIdentityResult()
        {
            // Arrange
            var userAdd = new UserAdd
            {
                Login = "User12345_4",
                Password = "User12345_4",
                Roles = new List<string> { "administrator" },
                Birthday = DateTime.Now
            };
            _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<CustomUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            var usersController = new UsersController(_mockUserManager.Object, _mockRoleManager.Object, _authOptions);

            // Act
            var result = usersController.Create(userAdd).Result;

            // Assert
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public void CreateMethod_ReturnsBadRequestResult_WhenFailedIdentityResult()
        {
            // Arrange
            var userAdd = new UserAdd
            {
                Login = "User12345_4",
                Password = "User12345_4",
                Roles = new List<string> { "administrator" },
                Birthday = DateTime.Now
            };
            _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<CustomUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Failed());
            var usersController = new UsersController(_mockUserManager.Object, _mockRoleManager.Object, _authOptions);

            // Act
            var result = usersController.Create(userAdd).Result;

            // Assert
            Assert.IsType<BadRequestResult>(result.Result);
        }

        #endregion

        #region Delete

        [Fact]
        public void DeleteMethod_ReturnsNoContextResult_WhenModelWasFind()
        {
            // Arrange
            const string idExistingUser = "1";
            var customUser = new CustomUser
            {
                Id = "1",
                UserName = "User12345_1",
                Birthday = DateTime.Now.Date
            };
            _mockUserManager.Setup(x => x.FindByIdAsync(idExistingUser)).ReturnsAsync(customUser);
            _mockUserManager.Setup(x => x.DeleteAsync(It.IsAny<CustomUser>())).ReturnsAsync(IdentityResult.Success);
            var usersController = new UsersController(_mockUserManager.Object, _mockRoleManager.Object, _authOptions);

            // Act
            var result = usersController.Delete(idExistingUser).Result;

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteMethod_ReturnsNotFoundResult_WhenModelWasNotFound()
        {
            // Arrange
            const string idNoExistingUser = "1";
            _mockUserManager.Setup(x => x.FindByIdAsync(idNoExistingUser)).ReturnsAsync(It.IsAny<CustomUser>());
            var usersController = new UsersController(_mockUserManager.Object, _mockRoleManager.Object, _authOptions);

            // Act
            var result = usersController.Delete(idNoExistingUser).Result;

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        #endregion

        #region Edit

        [Fact]
        public void EditMethod_ReturnsNoContextResult_WhenModelIsCorrect()
        {
            // Arrange
            const string idExistingUser = "1";
            var customUser = new CustomUser
            {
                Id = "1",
                UserName = "User12345_1",
                Birthday = DateTime.Now.Date
            };
            var userModify = new UserModify
            {
                Id = "1",
                Login = "User12345_1",
                Roles = new List<string> { "administrator" },
                Birthday = DateTime.Now.Date
            };
            _mockUserManager.Setup(x => x.FindByIdAsync(idExistingUser)).ReturnsAsync(customUser);
            _mockUserManager.Setup(x => x.UpdateAsync(It.IsAny<CustomUser>())).ReturnsAsync(IdentityResult.Success);
            var usersController = new UsersController(_mockUserManager.Object, _mockRoleManager.Object, _authOptions);

            // Act
            var result = usersController.Edit(userModify).Result;

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void EditMethod_ReturnsNotFoundResult_WhenModelWasNotFound()
        {
            // Arrange
            const string idNotExistingUser = "1";
            var userModify = new UserModify
            {
                Id = "1",
                Login = "User12345_1",
                Roles = new List<string> { "administrator" },
                Birthday = DateTime.Now.Date
            };
            _mockUserManager.Setup(x => x.FindByIdAsync(idNotExistingUser)).ReturnsAsync(It.IsAny<CustomUser>());
            var usersController = new UsersController(_mockUserManager.Object, _mockRoleManager.Object, _authOptions);

            // Act
            var result = usersController.Edit(userModify).Result;

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void EditMethod_ReturnsBadRequestResult_WhenFailedIdentityResult()
        {
            // Arrange
            const string idExistingUser = "1";
            var customUser = new CustomUser
            {
                Id = "1",
                UserName = "User12345_1",
                Birthday = DateTime.Now.Date
            };
            var userModify = new UserModify
            {
                Id = "1",
                Login = "User12345_1",
                Roles = new List<string> { "administrator" },
                Birthday = DateTime.Now.Date
            };
            _mockUserManager.Setup(x => x.FindByIdAsync(idExistingUser)).ReturnsAsync(customUser);
            _mockUserManager.Setup(x => x.UpdateAsync(It.IsAny<CustomUser>())).ReturnsAsync(IdentityResult.Failed());
            var usersController = new UsersController(_mockUserManager.Object, _mockRoleManager.Object, _authOptions);

            // Act
            var result = usersController.Edit(userModify).Result;

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        #endregion

        #region EditPassword

        [Fact]
        public void EditPasswordMethod_ReturnsNoContextResult_WhenModelIsCorrect()
        {
            // Arrange
            const string idExistingUser = "1";
            var customUser = new CustomUser
            {
                Id = "1",
                UserName = "User12345_1",
                Birthday = DateTime.Now.Date
            };
            var userModifyPassword = new UserModifyPassword
            {
                Id = "1",
                NewPassword = "password"
            };
            _mockUserManager.Setup(x => x.FindByIdAsync(idExistingUser)).ReturnsAsync(customUser);
            _mockPasswordValidator.Setup(x =>
                x.ValidateAsync(_mockUserManager.Object, customUser, It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            _mockUserManager.Object.PasswordValidators.Add(_mockPasswordValidator.Object);
            _mockPasswordHasher.Setup(x => x.HashPassword(customUser, It.IsAny<string>())).Returns(It.IsAny<string>());
            _mockUserManager.Object.PasswordHasher = _mockPasswordHasher.Object;
            _mockUserManager.Setup(x => x.UpdateAsync(It.IsAny<CustomUser>())).ReturnsAsync(IdentityResult.Success);
            var usersController = new UsersController(_mockUserManager.Object, _mockRoleManager.Object, _authOptions);

            // Act
            var result = usersController.EditPassword(userModifyPassword).Result;

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void EditPasswordMethod_ReturnsNotFoundResult_WhenModelWasNotFound()
        {
            // Arrange
            const string idNotExistingUser = "1";
            var userModifyPassword = new UserModifyPassword
            {
                Id = "1",
                NewPassword = "password"
            };
            _mockUserManager.Setup(x => x.FindByIdAsync(idNotExistingUser)).ReturnsAsync(It.IsAny<CustomUser>());
            var usersController = new UsersController(_mockUserManager.Object, _mockRoleManager.Object, _authOptions);

            // Act
            var result = usersController.EditPassword(userModifyPassword).Result;

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void EditPasswordMethod_ReturnsBadRequestResult_WhenFailedResultValidationPassword()
        {
            // Arrange
            const string idExistingUser = "1";
            var customUser = new CustomUser
            {
                Id = "1",
                UserName = "User12345_1",
                Birthday = DateTime.Now.Date
            };
            var userModifyPassword = new UserModifyPassword
            {
                Id = "1",
                NewPassword = "password"
            };
            _mockUserManager.Setup(x => x.FindByIdAsync(idExistingUser)).ReturnsAsync(customUser);
            _mockPasswordValidator.Setup(x =>
                x.ValidateAsync(_mockUserManager.Object, customUser, It.IsAny<string>())).ReturnsAsync(IdentityResult.Failed());
            _mockUserManager.Object.PasswordValidators.Add(_mockPasswordValidator.Object);
            var usersController = new UsersController(_mockUserManager.Object, _mockRoleManager.Object, _authOptions);

            // Act
            var result = usersController.EditPassword(userModifyPassword).Result;

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void EditPasswordMethod_ReturnsBadRequestResult_WhenFailedResultUpdateUser()
        {
            // Arrange
            const string idExistingUser = "1";
            var customUser = new CustomUser
            {
                Id = "1",
                UserName = "User12345_1",
                Birthday = DateTime.Now.Date
            };
            var userModifyPassword = new UserModifyPassword
            {
                Id = "1",
                NewPassword = "password"
            };
            _mockUserManager.Setup(x => x.FindByIdAsync(idExistingUser)).ReturnsAsync(customUser);
            _mockPasswordValidator.Setup(x =>
                x.ValidateAsync(_mockUserManager.Object, customUser, It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            _mockUserManager.Object.PasswordValidators.Add(_mockPasswordValidator.Object);
            _mockPasswordHasher.Setup(x => x.HashPassword(customUser, It.IsAny<string>())).Returns(It.IsAny<string>());
            _mockUserManager.Object.PasswordHasher = _mockPasswordHasher.Object;
            _mockUserManager.Setup(x => x.UpdateAsync(It.IsAny<CustomUser>())).ReturnsAsync(IdentityResult.Failed());
            var usersController = new UsersController(_mockUserManager.Object, _mockRoleManager.Object, _authOptions);

            // Act
            var result = usersController.EditPassword(userModifyPassword).Result;

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        #endregion

        #region GetRoles

        [Fact]
        public void GetRolesMethod_ReturnsOkObjectResult_WithListOfRoleName_WhenListHaveValues()
        {
            // Arrange
            var listNamesRole = new List<string> { "administrator", "employee" };
            _mockRoleManager.Setup(x => x.Roles).Returns(GetIdentityRoles());
            var usersController = new UsersController(_mockUserManager.Object, _mockRoleManager.Object, _authOptions);

            // Act
            var result = usersController.GetRoles().Result;

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(result);
            var receiptsListNamesRole = Assert.IsType<List<string>>(objectResult.Value);
            listNamesRole.Should().BeEquivalentTo(receiptsListNamesRole);
        }

        [Fact]
        public void GetRolesMethod_ReturnsNotFoundResult_WhenListHaveNoValues()
        {
            // Arrange
            _mockRoleManager.Setup(x => x.Roles).Returns(new List<IdentityRole>().AsQueryable());
            var usersController = new UsersController(_mockUserManager.Object, _mockRoleManager.Object, _authOptions);

            // Act
            var result = usersController.GetRoles().Result;

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        #endregion
    }
}
