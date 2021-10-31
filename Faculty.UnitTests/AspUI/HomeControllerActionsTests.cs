using Moq;
using Xunit;
using System;
using System.IO;
using System.Net;
using NSubstitute;
using System.Net.Http;
using Xunit.Abstractions;
using Faculty.AspUI.Tools;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Faculty.AspUI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using Faculty.AspUI.Services.Interfaces;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication;
using Faculty.AspUI.ViewModels.LoginRegister;

namespace Faculty.UnitTests.AspUI
{
    public class UserInfo
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }

        public UserInfo(IConfiguration configuration)
        {
            Login = configuration["AuthInfo:Login"];
            Password = configuration["AuthInfo:Password"];
            Token = configuration["AuthInfo:Token"];
        }
    }

    public class HomeControllerActionsTests
    {
        private readonly Mock<IAuthService> _mockAuthService;
        private readonly Mock<IStringLocalizer<HomeController>> _mockStringLocalizer;
        private readonly AuthOptions _authOptions;
        private readonly UserInfo _userInfo;
        private readonly HttpContext _httpContext;
        private readonly ITestOutputHelper _output;

        public HomeControllerActionsTests(ITestOutputHelper output)
        {
            _mockAuthService = new Mock<IAuthService>();
            _mockStringLocalizer = new Mock<IStringLocalizer<HomeController>>();
            const string keyErrorMessage = "CommonError";
            _mockStringLocalizer.Setup(localizer => localizer[keyErrorMessage]).Returns(new LocalizedString(string.Empty, string.Empty));
            var configuration = new ConfigurationBuilder().AddJsonFile(Path.Combine(Environment.CurrentDirectory, "appsettings.json")).Build();
            _authOptions = new AuthOptions(configuration);
            _userInfo = new UserInfo(configuration);
            _output = output;

            var httpContext = Substitute.For<HttpContext>();
            var session = Substitute.For<ISession>();
            httpContext.Session.Returns(session);
            var serviceProvider = Substitute.For<IServiceProvider>();
            serviceProvider
                .GetService(Arg.Is(typeof(IUrlHelperFactory)))
                .Returns(Substitute.For<IUrlHelperFactory>());
            serviceProvider
                .GetService(Arg.Is(typeof(IAuthenticationService)))
                .Returns(Substitute.For<IAuthenticationService>());
            httpContext.RequestServices.Returns(serviceProvider);
            _httpContext = httpContext;
        }

        [Fact]
        public void LoginMethod_ReturnsRedirectToIndexActionFacultyController_WhenCorrectModel()
        {
            // Arrange
            var loginUser = new LoginUser { Login = _userInfo.Login, Password = _userInfo.Password };
            HttpContent content = new StringContent(_userInfo.Token);
            _mockAuthService.Setup(service => service.Login(loginUser)).ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.NoContent, Content = content });
            var homeController = new HomeController(_mockAuthService.Object, _mockStringLocalizer.Object, _authOptions);
            homeController.ControllerContext = new ControllerContext { HttpContext = _httpContext };

            // Act
            var result = homeController.Login(loginUser).Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
            Assert.Equal("Faculty", redirect.ControllerName);
        }

        [Fact]
        public void LoginMethod_ReturnsAViewResult_WithAModelUserAdd_HttpStatusCodeUnauthorized()
        {
            // Arrange
            var loginUser = new LoginUser { Login = _userInfo.Login, Password = _userInfo.Password };
            _mockAuthService.Setup(service => service.Login(loginUser)).Throws(new HttpRequestException(string.Empty, null, HttpStatusCode.BadRequest));
            var homeController = new HomeController(_mockAuthService.Object, _mockStringLocalizer.Object, _authOptions);

            // Act
            var result = homeController.Login(loginUser).Result;

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<LoginUser>(viewResult.Model);
            Assert.Equal(loginUser, model);
        }

        [Fact]
        public void LoginMethod_ReturnsRedirectToErrorActionHomeController_WhenAnyHttpRequestException()
        {
            // Arrange
            var loginUser = new LoginUser { Login = _userInfo.Login, Password = _userInfo.Password };
            _mockAuthService.Setup(service => service.Login(loginUser)).Throws(new HttpRequestException());
            var homeController = new HomeController(_mockAuthService.Object, _mockStringLocalizer.Object, _authOptions);

            // Act
            var result = homeController.Login(loginUser).Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Error", redirect.ActionName);
            Assert.Equal("Home", redirect.ControllerName);
        }

        [Fact]
        public void RegisterMethod_RedirectToLoginAction_WhenCorrectModel()
        {
            // Arrange
            var registerUser = new RegisterUser { Login = "User12345678", Password = "User12345678", PasswordConfirm = "User12345678" };
            _mockAuthService.Setup(service => service.Register(registerUser)).Returns(Task.FromResult(new HttpResponseMessage(HttpStatusCode.NoContent)));
            var homeController = new HomeController(_mockAuthService.Object, _mockStringLocalizer.Object, _authOptions);

            // Act
            var result = homeController.Register(registerUser).Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Login", redirect.ActionName);
        }

        [Fact]
        public void RegisterMethod_ReturnsAViewResult_WithAModelUserAdd_HttpStatusCodeUnauthorized()
        {
            // Arrange
            var registerUser = new RegisterUser { Login = null, Password = "User12345678", PasswordConfirm = "User12345678" };
            _mockAuthService.Setup(service => service.Register(registerUser)).Throws(new HttpRequestException(string.Empty, null, HttpStatusCode.BadRequest));
            var homeController = new HomeController(_mockAuthService.Object, _mockStringLocalizer.Object, _authOptions);

            // Act
            var result = homeController.Register(registerUser).Result;

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<RegisterUser>(viewResult.Model);
            Assert.Equal(registerUser, model);
        }

        [Fact]
        public void RegisterMethod_ReturnsRedirectToErrorActionHomeController_WhenAnyHttpRequestException()
        {
            // Arrange
            var registerUser = new RegisterUser { Login = "User12345678", Password = "User12345678", PasswordConfirm = "User12345678" };
            _mockAuthService.Setup(service => service.Register(registerUser)).Throws(new HttpRequestException());
            var homeController = new HomeController(_mockAuthService.Object, _mockStringLocalizer.Object, _authOptions);

            // Act
            var result = homeController.Register(registerUser).Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Error", redirect.ActionName);
            Assert.Equal("Home", redirect.ControllerName);
        }
    }
}
