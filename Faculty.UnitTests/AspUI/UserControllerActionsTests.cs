using Moq;
using Xunit;
using System;
using System.Net;
using AutoMapper;
using System.Linq;
using System.Net.Http;
using Xunit.Abstractions;
using Faculty.AspUI.Tools;
using Faculty.Common.Dto.User;
using Microsoft.AspNetCore.Mvc;
using Faculty.AspUI.Controllers;
using System.Collections.Generic;
using Faculty.AspUI.ViewModels.User;
using Microsoft.Extensions.Localization;
using Faculty.AspUI.Services.Interfaces;

namespace Faculty.UnitTests.AspUI
{
    public class UserControllerActionsTests
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<IStringLocalizer<UserController>> _mockStringLocalizer;
        private readonly ITestOutputHelper _output;
        private readonly IMapper _mapper;

        public UserControllerActionsTests(ITestOutputHelper output)
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            _mapper = new Mapper(mapperConfiguration);
            _mockUserService = new Mock<IUserService>();
            _mockStringLocalizer = new Mock<IStringLocalizer<UserController>>();
            const string keyErrorMessage = "CommonError";
            _mockStringLocalizer.Setup(localizer => localizer[keyErrorMessage]).Returns(new LocalizedString(string.Empty, string.Empty));
            _output = output;
        }

        #region Index

        [Fact]
        public void IndexMethod_ReturnsAViewResult_WithAListOfUserDisplay()
        {
            // Arrange
            _mockUserService.Setup(service => service.GetUsers()).ReturnsAsync(GetUsersDto());
            var userController = new UserController(_mockUserService.Object, _mockStringLocalizer.Object, _mapper);

            // Act
            var result = userController.Index().Result;

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var models = Assert.IsAssignableFrom<IEnumerable<UserDisplay>>(viewResult.ViewData.Model);
            Assert.Equal(3, models.Count());
        }

        private static IEnumerable<UserDto> GetUsersDto()
        {
            var usersDto = new List<UserDto>()
            {
                new ()
                {
                    Id = Guid.NewGuid().ToString(),
                    Login = "Admin12345",
                    Roles = new List<string> { "administrator" },
                    Birthday = DateTime.Now
                },
                new ()
                {
                    Id = Guid.NewGuid().ToString(),
                    Login = "Admin54321",
                    Roles = new List<string> { "administrator", "employee" },
                    Birthday = DateTime.Now
                },
                new ()
                {
                    Id = Guid.NewGuid().ToString(),
                    Login = "Employee12345",
                    Roles = new List<string> { "employee" },
                    Birthday = DateTime.Now
                }
            };

            return usersDto;
        }

        [Fact]
        public void IndexMethod_ReturnsRedirectToLoginActionHomeController_WhenHttpStatusCodeUnauthorized()
        {
            // Arrange
            _mockUserService.Setup(service => service.GetUsers()).Throws(new HttpRequestException(string.Empty, null, HttpStatusCode.Unauthorized));
            var userController = new UserController(_mockUserService.Object, _mockStringLocalizer.Object, _mapper);

            // Act
            var result = userController.Index().Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Login", redirect.ActionName);
            Assert.Equal("Home", redirect.ControllerName);
        }

        [Fact]
        public void IndexMethod_ReturnsRedirectToErrorActionHomeController_WhenAnyHttpRequestException()
        {
            // Arrange
            _mockUserService.Setup(service => service.GetUsers()).Throws(new HttpRequestException());
            var userController = new UserController(_mockUserService.Object, _mockStringLocalizer.Object, _mapper);

            // Act
            var result = userController.Index().Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Error", redirect.ActionName);
            Assert.Equal("Home", redirect.ControllerName);
        }

        #endregion

        #region Create

        [Fact]
        public void CreateMethod_ReturnsRedirectToIndexAction_WhenCorrectModel()
        {
            // Arrange
            var userAdd = new UserAdd
            {
                Login = "Test123456",
                Password = "Test123456",
                Roles = new List<string> { "administrator" },
                Birthday = DateTime.Now
            };
            var userAddDto = _mapper.Map<UserAdd, UserAddDto>(userAdd);
            _mockUserService.Setup(service => service.CreateUser(userAddDto)).ReturnsAsync(new HttpResponseMessage
                {StatusCode = HttpStatusCode.Created});
            var userController = new UserController(_mockUserService.Object, _mockStringLocalizer.Object, _mapper);

            // Act
            var result = userController.Create(userAdd).Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }

        [Fact]
        public void CreateMethod_ReturnsAViewResult_WithAModelUserAdd_WhenInvalidModel()
        {
            // Arrange
            var userAdd = new UserAdd
            {
                Login = "Test123456",
                Password = null,
                Roles = new List<string> { "administrator" },
                Birthday = DateTime.Now
            };
            var userAddDto = _mapper.Map<UserAdd, UserAddDto>(userAdd);
            _mockUserService.Setup(service => service.CreateUser(userAddDto)).ReturnsAsync(new HttpResponseMessage
                {StatusCode = HttpStatusCode.Created});
            var userController = new UserController(_mockUserService.Object, _mockStringLocalizer.Object, _mapper);
            userController.ModelState.AddModelError(string.Empty, "Invalid password.");

            // Act
            var result = userController.Create(userAdd).Result;

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<UserAdd>(viewResult.Model);
            Assert.Equal(userAdd, model);
        }

        [Fact]
        public void CreateMethod_ReturnsAViewResult_WithAModelUserAdd_WhenHttpStatusCodeBadRequest()
        {
            // Arrange
            var userAdd = new UserAdd
            {
                Login = null,
                Password = null,
                Roles = new List<string> { "administrator" },
                Birthday = DateTime.Now
            };
            _mockUserService.Setup(service => service.CreateUser(It.IsAny<UserAddDto>()))
                .Throws(new HttpRequestException(string.Empty, null, HttpStatusCode.BadRequest));
            var userController = new UserController(_mockUserService.Object, _mockStringLocalizer.Object, _mapper);

            // Act
            var result = userController.Create(userAdd).Result;

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<UserAdd>(viewResult.Model);
            Assert.Equal(userAdd, model);
        }

        [Fact]
        public void CreateMethod_ReturnsRedirectToLoginActionHomeController_WhenHttpStatusCodeUnauthorized()
        {
            // Arrange
            var userAdd = new UserAdd
            {
                Login = "Test123456",
                Password = "Test123456",
                Roles = new List<string> { "administrator" },
                Birthday = DateTime.Now
            };
            _mockUserService.Setup(service => service.CreateUser(It.IsAny<UserAddDto>()))
                .Throws(new HttpRequestException(string.Empty, null, HttpStatusCode.Unauthorized));
            var userController = new UserController(_mockUserService.Object, _mockStringLocalizer.Object, _mapper);

            // Act
            var result = userController.Create(userAdd).Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Login", redirect.ActionName);
            Assert.Equal("Home", redirect.ControllerName);
        }

        [Fact]
        public void CreateMethod_ReturnsRedirectToErrorActionHomeController_WhenAnyHttpRequestException()
        {
            // Arrange
            var userAdd = new UserAdd
            {
                Login = "Test123456",
                Password = "Test123456",
                Roles = new List<string> { "administrator" },
                Birthday = DateTime.Now
            };
            _mockUserService.Setup(service => service.CreateUser(It.IsAny<UserAddDto>())).Throws(new HttpRequestException());
            var userController = new UserController(_mockUserService.Object, _mockStringLocalizer.Object, _mapper);

            // Act
            var result = userController.Create(userAdd).Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Error", redirect.ActionName);
            Assert.Equal("Home", redirect.ControllerName);
        }

        #endregion

        #region Delete

        [Fact]
        public void DeleteMethod_ReturnsRedirectToIndexAction_WhenCorrectArgument()
        {
            // Arrange
            const string idExistingUser = "1";
            _mockUserService.Setup(service => service.DeleteUser(idExistingUser)).ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NoContent
            });
            var userController = new UserController(_mockUserService.Object, _mockStringLocalizer.Object, _mapper);

            // Act
            var result = userController.Delete(idExistingUser).Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }

        [Fact]
        public void DeleteMethod_ReturnsRedirectToLoginActionHomeController_WhenHttpStatusCodeUnauthorized()
        {
            // Arrange
            const string idExistingUser = "1";
            _mockUserService.Setup(service => service.DeleteUser(idExistingUser))
                .Throws(new HttpRequestException(string.Empty, null, HttpStatusCode.Unauthorized));
            var userController = new UserController(_mockUserService.Object, _mockStringLocalizer.Object, _mapper);

            // Act
            var result = userController.Delete(idExistingUser).Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Login", redirect.ActionName);
            Assert.Equal("Home", redirect.ControllerName);
        }

        [Fact]
        public void DeleteMethod_ReturnsRedirectToErrorActionHomeController_WhenAnyHttpRequestException()
        {
            // Arrange
            const string idExistingUser = "1";
            _mockUserService.Setup(service => service.DeleteUser(idExistingUser)).Throws(new HttpRequestException());
            var userController = new UserController(_mockUserService.Object, _mockStringLocalizer.Object, _mapper);

            // Act
            var result = userController.Delete(idExistingUser).Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Error", redirect.ActionName);
            Assert.Equal("Home", redirect.ControllerName);
        }

        #endregion

        #region Edit

        [Fact]
        public void EditMethod_ReturnsRedirectToIndexAction_WhenCorrectModel()
        {
            // Arrange
            var userModify = new UserModify
            {
                Id = Guid.NewGuid().ToString(),
                Login = "Test123456",
                Roles = new List<string> { "administrator" },
                Birthday = DateTime.Now
            };
            var userDto = _mapper.Map<UserModify, UserDto>(userModify);
            _mockUserService.Setup(service => service.EditUser(userDto)).ReturnsAsync(new HttpResponseMessage
                {StatusCode = HttpStatusCode.NoContent});
            var userController = new UserController(_mockUserService.Object, _mockStringLocalizer.Object, _mapper);

            // Act
            var result = userController.Edit(userModify).Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }

        [Fact]
        public void EditMethod_ReturnsAViewResult_WithAModelUserAdd_WhenInvalidModel()
        {
            // Arrange
            var userModify = new UserModify
            {
                Id = Guid.NewGuid().ToString(),
                Login = null,
                Roles = new List<string> { "administrator" },
                Birthday = DateTime.Now
            };
            var userDto = _mapper.Map<UserModify, UserDto>(userModify);
            _mockUserService.Setup(service => service.EditUser(userDto)).ReturnsAsync(new HttpResponseMessage
                {StatusCode = HttpStatusCode.Created});
            var userController = new UserController(_mockUserService.Object, _mockStringLocalizer.Object, _mapper);
            userController.ModelState.AddModelError(string.Empty, "Invalid login.");

            // Act
            var result = userController.Edit(userModify).Result;

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<UserModify>(viewResult.Model);
            Assert.Equal(userModify, model);
        }

        [Fact]
        public void EditMethod_ReturnsAViewResult_WithAModelUserAdd_WhenHttpStatusCodeBadRequest()
        {
            // Arrange
            var userModify = new UserModify
            {
                Id = Guid.NewGuid().ToString(),
                Login = null,
                Roles = new List<string> { "administrator" },
                Birthday = DateTime.Now
            };
            _mockUserService.Setup(service => service.EditUser(It.IsAny<UserDto>()))
                .Throws(new HttpRequestException(string.Empty, null, HttpStatusCode.BadRequest));
            var userController = new UserController(_mockUserService.Object, _mockStringLocalizer.Object, _mapper);

            // Act
            var result = userController.Edit(userModify).Result;

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<UserModify>(viewResult.Model);
            Assert.Equal(userModify, model);
        }

        [Fact]
        public void EditMethod_ReturnsRedirectToLoginActionHomeController_WhenHttpStatusCodeUnauthorized()
        {
            // Arrange
            var userModify = new UserModify
            {
                Id = Guid.NewGuid().ToString(),
                Login = "Test123456",
                Roles = new List<string> { "administrator" },
                Birthday = DateTime.Now
            };
            _mockUserService.Setup(service => service.EditUser(It.IsAny<UserDto>()))
                .Throws(new HttpRequestException(string.Empty, null, HttpStatusCode.Unauthorized));
            var userController = new UserController(_mockUserService.Object, _mockStringLocalizer.Object, _mapper);

            // Act
            var result = userController.Edit(userModify).Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Login", redirect.ActionName);
            Assert.Equal("Home", redirect.ControllerName);
        }

        [Fact]
        public void EditMethod_ReturnsRedirectToErrorActionHomeController_WhenAnyHttpRequestException()
        {
            // Arrange
            var userModify = new UserModify
            {
                Id = Guid.NewGuid().ToString(),
                Login = "Test123456",
                Roles = new List<string> { "administrator" },
                Birthday = DateTime.Now
            };
            _mockUserService.Setup(service => service.EditUser(It.IsAny<UserDto>())).Throws(new HttpRequestException());
            var userController = new UserController(_mockUserService.Object, _mockStringLocalizer.Object, _mapper);

            // Act
            var result = userController.Edit(userModify).Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Error", redirect.ActionName);
            Assert.Equal("Home", redirect.ControllerName);
        }

        #endregion

        #region EditPassword

        [Fact]
        public void EditPasswordMethod_ReturnsViewResultWithAction_ForCorrectModel()
        {
            // Arrange
            var userModifyPassword = new UserModifyPassword
            {
                Id = Guid.NewGuid().ToString(),
                NewPassword = "Test123456"
            };
            var userModifyPasswordDto = _mapper.Map<UserModifyPassword, UserModifyPasswordDto>(userModifyPassword);
            _mockUserService.Setup(service => service.EditPasswordUser(userModifyPasswordDto))
                .ReturnsAsync(new HttpResponseMessage {StatusCode = HttpStatusCode.Created});
            var userController = new UserController(_mockUserService.Object, _mockStringLocalizer.Object, _mapper);

            // Act
            var result = userController.EditPassword(userModifyPassword).Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }

        [Fact]
        public void EditPasswordMethod_ReturnsAViewResult_WithAModelUserAdd_WhenInvalidModel()
        {
            // Arrange
            var userModifyPassword = new UserModifyPassword
            {
                Id = Guid.NewGuid().ToString(),
                NewPassword = null
            };
            var userModifyPasswordDto = _mapper.Map<UserModifyPassword, UserModifyPasswordDto>(userModifyPassword);
            _mockUserService.Setup(service => service.EditPasswordUser(userModifyPasswordDto))
                .ReturnsAsync(new HttpResponseMessage {StatusCode = HttpStatusCode.Created});
            var userController = new UserController(_mockUserService.Object, _mockStringLocalizer.Object, _mapper);
            userController.ModelState.AddModelError(string.Empty, "Invalid login.");

            // Act
            var result = userController.EditPassword(userModifyPassword).Result;

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<UserModifyPassword>(viewResult.Model);
            Assert.Equal(userModifyPassword, model);
        }

        [Fact]
        public void EditPasswordMethod_ReturnsAViewResult_WithAModelUserAdd_WhenHttpStatusCodeBadRequest()
        {
            // Arrange
            var userModifyPassword = new UserModifyPassword
            {
                Id = Guid.NewGuid().ToString(),
                NewPassword = null
            };
            _mockUserService.Setup(service => service.EditPasswordUser(It.IsAny<UserModifyPasswordDto>()))
                .Throws(new HttpRequestException(string.Empty, null, HttpStatusCode.BadRequest));
            var userController = new UserController(_mockUserService.Object, _mockStringLocalizer.Object, _mapper);

            // Act
            var result = userController.EditPassword(userModifyPassword).Result;

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<UserModifyPassword>(viewResult.Model);
            Assert.Equal(userModifyPassword, model);
        }

        [Fact]
        public void EditPasswordMethod_ReturnsRedirectToLoginActionHomeController_WhenHttpStatusCodeUnauthorized()
        {
            // Arrange
            var userEditPassword = new UserModifyPassword
            {
                Id = Guid.NewGuid().ToString(),
                NewPassword = "Test123456"
            };
            _mockUserService.Setup(service => service.EditPasswordUser(It.IsAny<UserModifyPasswordDto>()))
                .Throws(new HttpRequestException(string.Empty, null, HttpStatusCode.Unauthorized));
            var userController = new UserController(_mockUserService.Object, _mockStringLocalizer.Object, _mapper);

            // Act
            var result = userController.EditPassword(userEditPassword).Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Login", redirect.ActionName);
            Assert.Equal("Home", redirect.ControllerName);
        }

        [Fact]
        public void EditPasswordMethod_ReturnsRedirectToErrorActionHomeController_WhenAnyHttpRequestException()
        {
            // Arrange
            var userEditPassword = new UserModifyPassword
            {
                Id = Guid.NewGuid().ToString(),
                NewPassword = "Test123456"
            };
            _mockUserService.Setup(service => service.EditPasswordUser(It.IsAny<UserModifyPasswordDto>())).Throws(new HttpRequestException());
            var userController = new UserController(_mockUserService.Object, _mockStringLocalizer.Object, _mapper);

            // Act
            var result = userController.EditPassword(userEditPassword).Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Error", redirect.ActionName);
            Assert.Equal("Home", redirect.ControllerName);
        }

        #endregion
    }
}
