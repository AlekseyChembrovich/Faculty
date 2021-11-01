using Moq;
using Xunit;
using System;
using System.IO;
using Xunit.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Faculty.AuthenticationServer.Tools;
using Faculty.AuthenticationServer.Models;
using Faculty.AuthenticationServer.Controllers;
using Faculty.AuthenticationServer.Models.LoginRegister;

namespace Faculty.UnitTests.AuthenticationServer
{
    public class AuthControllerActionsTests
    {
        private readonly ITestOutputHelper _output;
        private readonly Mock<UserManager<CustomUser>> _mockUserManager;
        private readonly Mock<SignInManager<CustomUser>> _mockSignInManager;
        private readonly AuthOptions _authOptions;

        public AuthControllerActionsTests(ITestOutputHelper output)
        {
            var mockUserStore = Mock.Of<IUserStore<CustomUser>>();
            _mockUserManager = new Mock<UserManager<CustomUser>>(mockUserStore, null, null, null, null, null, null, null, null);
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var mockUserPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<CustomUser>>();
            _mockSignInManager = new Mock<SignInManager<CustomUser>>(
                _mockUserManager.Object, 
                mockHttpContextAccessor.Object,
                mockUserPrincipalFactory.Object, null, null, null, null);
            var configuration = new ConfigurationBuilder().AddJsonFile(Path.Combine(Environment.CurrentDirectory, "appsettings.json")).Build();
            _authOptions = new AuthOptions(configuration);
            _output = output;
        }

        #region Login

        [Fact]
        public void LoginMethod_ReturnsOkObjectResult_WithJwtToken_WhenModelIsCorrect()
        {
            // Arrange
            var loginUser = new LoginUser { Login = "User123456", Password = "User123456" };
            var customUser = new CustomUser
            {
                Id = "1",
                UserName = "User123456",
                Birthday = DateTime.Now
            };
            _mockUserManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(customUser);
            _mockSignInManager.Setup(x =>
                    x.CheckPasswordSignInAsync(It.IsAny<CustomUser>(), It.IsAny<string>(), It.IsAny<bool>()))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);
            _mockUserManager.Setup(x => x.GetRolesAsync(customUser)).ReturnsAsync(new List<string> { "administrator" });
            var authController = new AuthController(_mockUserManager.Object, _mockSignInManager.Object, _authOptions);

            // Act
            var result = authController.Login(loginUser).Result;

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(result);
            var jwt = Assert.IsType<string>(objectResult.Value);
            Assert.NotNull(jwt);
        }

        [Fact]
        public void LoginMethod_ReturnsBadRequestResult_WhenModelWasNotFound()
        {
            // Arrange
            var loginUser = new LoginUser { Login = "User123456", Password = "User123456" };
            _mockUserManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(It.IsAny<CustomUser>());
            var authController = new AuthController(_mockUserManager.Object, _mockSignInManager.Object, _authOptions);

            // Act
            var result = authController.Login(loginUser).Result;

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void LoginMethod_ReturnsBadRequestResult_WhenInvalidPassword()
        {
            // Arrange
            var loginUser = new LoginUser { Login = "User123456", Password = "User123456" };
            var customUser = new CustomUser
            {
                Id = "1",
                UserName = "User123456",
                Birthday = DateTime.Now
            };
            _mockUserManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(customUser);
            _mockSignInManager.Setup(x =>
                    x.CheckPasswordSignInAsync(It.IsAny<CustomUser>(), It.IsAny<string>(), It.IsAny<bool>()))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Failed);
            var authController = new AuthController(_mockUserManager.Object, _mockSignInManager.Object, _authOptions);

            // Act
            var result = authController.Login(loginUser).Result;

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        #endregion

        #region Register

        [Fact]
        public void RegisterMethod_ReturnsNoContextResult_WhenModelIsCorrect()
        {
            // Arrange
            var registerUser = new RegisterUser { Login = "User123456", Password = "User123456" };
            var customUser = new CustomUser
            {
                Id = "1",
                UserName = registerUser.Login,
                Birthday = DateTime.Now
            };
            _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<CustomUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            var authController = new AuthController(_mockUserManager.Object, _mockSignInManager.Object, _authOptions);

            // Act
            var result = authController.Register(registerUser).Result;

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void RegisterMethod_ReturnsBadRequestResult_WhenFailedIdentityResult()
        {
            // Arrange
            var registerUser = new RegisterUser { Login = "User123456", Password = "User123456" };
            var customUser = new CustomUser
            {
                Id = "1",
                UserName = registerUser.Login,
                Birthday = DateTime.Now
            };
            _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<CustomUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Failed());
            var authController = new AuthController(_mockUserManager.Object, _mockSignInManager.Object, _authOptions);

            // Act
            var result = authController.Register(registerUser).Result;

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        #endregion
    }
}
