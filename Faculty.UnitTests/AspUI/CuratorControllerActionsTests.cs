using Moq;
using Xunit;
using System.Net;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Faculty.AspUI.Controllers;
using System.Collections.Generic;
using Faculty.AspUI.ViewModels.Curator;
using Faculty.AspUI.Services.Interfaces;

namespace Faculty.UnitTests.AspUI
{
    public class CuratorControllerActionsTests
    {
        private readonly Mock<ICuratorService> _mockCuratorService;

        public CuratorControllerActionsTests()
        {
            _mockCuratorService = new Mock<ICuratorService>();
        }

        #region Index

        [Fact]
        public void IndexMethod_ReturnsAViewResult_WithAListOfCuratorDisplay()
        {
            // Arrange
            _mockCuratorService.Setup(service => service.GetCurators()).ReturnsAsync(GetCuratorsDisplay());
            var curatorController = new CuratorController(_mockCuratorService.Object);

            // Act
            var result = curatorController.Index().Result;

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var models = Assert.IsAssignableFrom<IEnumerable<CuratorDisplayModify>>(viewResult.ViewData.Model);
            Assert.Equal(3, models.Count());
        }

        private static IEnumerable<CuratorDisplayModify> GetCuratorsDisplay()
        {
            var curatorsDisplay = new List<CuratorDisplayModify>()
            {
                new ()
                {
                    Id = 1,
                    Surname = "test1",
                    Name = "test1",
                    Doublename = "test1",
                    Phone = "+375-29-557-06-11"
                },
                new ()
                {
                    Id = 2,
                    Surname = "test2",
                    Name = "test2",
                    Doublename = "test2",
                    Phone = "+375-29-557-06-22"
                },
                new ()
                {
                    Id = 3,
                    Surname = "test3",
                    Name = "test3",
                    Doublename = "test3",
                    Phone = "+375-29-557-06-33"
                }
            };

            return curatorsDisplay;
        }

        [Fact]
        public void IndexMethod_ReturnsRedirectToErrorActionHomeController_WhenAnyHttpRequestException()
        {
            // Arrange
            _mockCuratorService.Setup(service => service.GetCurators()).Throws(new HttpRequestException());
            var curatorController = new CuratorController(_mockCuratorService.Object);

            // Act
            var result = curatorController.Index().Result;

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
            var curatorAdd = new CuratorAdd
            {
                Surname = "test1",
                Name = "test1",
                Doublename = "test1",
                Phone = "+375-29-557-06-11"
            };
            _mockCuratorService.Setup(service => service.CreateCurator(curatorAdd)).ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.Created });
            var curatorController = new CuratorController(_mockCuratorService.Object);

            // Act
            var result = curatorController.Create(curatorAdd).Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }

        [Fact]
        public void CreateMethod_ReturnsAViewResult_WithAModelCuratorAdd_WhenInvalidModel()
        {
            // Arrange
            var curatorAdd = new CuratorAdd
            {
                Surname = "test1",
                Name = null,
                Doublename = "test1",
                Phone = "+375-29-557-06-11"
            };
            _mockCuratorService.Setup(service => service.CreateCurator(curatorAdd)).ReturnsAsync(new HttpResponseMessage());
            var curatorController = new CuratorController(_mockCuratorService.Object);
            curatorController.ModelState.AddModelError(string.Empty, "Invalid name.");

            // Act
            var result = curatorController.Create(curatorAdd).Result;

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<CuratorAdd>(viewResult.Model);
            Assert.Equal(curatorAdd, model);
        }

        [Fact]
        public void CreateMethod_ReturnsRedirectToLoginActionHomeController_WhenHttpStatusCodeUnauthorized()
        {
            // Arrange
            var curatorAdd = new CuratorAdd
            {
                Surname = "test1",
                Name = "test1",
                Doublename = "test1",
                Phone = "+375-29-557-06-11"
            };
            _mockCuratorService.Setup(service => service.CreateCurator(curatorAdd)).Throws(new HttpRequestException(string.Empty, null, HttpStatusCode.Unauthorized));
            var curatorController = new CuratorController(_mockCuratorService.Object);

            // Act
            var result = curatorController.Create(curatorAdd).Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Login", redirect.ActionName);
            Assert.Equal("Home", redirect.ControllerName);
        }

        [Fact]
        public void CreateMethod_ReturnsRedirectToErrorActionHomeController_WhenAnyHttpRequestException()
        {
            // Arrange
            var curatorAdd = new CuratorAdd
            {
                Surname = "test1",
                Name = "test1",
                Doublename = "test1",
                Phone = "+375-29-557-06-11"
            };
            _mockCuratorService.Setup(service => service.CreateCurator(curatorAdd)).Throws(new HttpRequestException());
            var curatorController = new CuratorController(_mockCuratorService.Object);

            // Act
            var result = curatorController.Create(curatorAdd).Result;

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
            const int idExistCurator = 1;
            var curatorModify = new CuratorDisplayModify()
            {
                Id = 1,
                Surname = "test1",
                Name = "test1",
                Doublename = "test1",
                Phone = "+375-29-557-06-11"
            };
            _mockCuratorService.Setup(service => service.DeleteCurator(idExistCurator))
                .ReturnsAsync(new HttpResponseMessage {StatusCode = HttpStatusCode.NoContent});
            var curatorController = new CuratorController(_mockCuratorService.Object);

            // Act
            var result = curatorController.Delete(curatorModify).Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }

        [Fact]
        public void DeleteMethod_ReturnsRedirectToLoginActionHomeController_WhenHttpStatusCodeUnauthorized()
        {
            // Arrange
            const int idExistCurator = 1;
            var curatorModify = new CuratorDisplayModify()
            {
                Id = 1,
                Surname = "test1",
                Name = "test1",
                Doublename = "test1",
                Phone = "+375-29-557-06-11"
            };
            _mockCuratorService.Setup(service => service.DeleteCurator(idExistCurator))
                .Throws(new HttpRequestException(string.Empty, null, HttpStatusCode.Unauthorized));
            var curatorController = new CuratorController(_mockCuratorService.Object);

            // Act
            var result = curatorController.Delete(curatorModify).Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Login", redirect.ActionName);
            Assert.Equal("Home", redirect.ControllerName);
        }

        [Fact]
        public void DeleteMethod_ReturnsRedirectToErrorActionHomeController_WhenAnyHttpRequestException()
        {
            // Arrange
            const int idExistCurator = 1;
            var curatorModify = new CuratorDisplayModify()
            {
                Id = 1,
                Surname = "test1",
                Name = "test1",
                Doublename = "test1",
                Phone = "+375-29-557-06-11"
            };
            _mockCuratorService.Setup(service => service.DeleteCurator(idExistCurator)).Throws(new HttpRequestException());
            var curatorController = new CuratorController(_mockCuratorService.Object);

            // Act
            var result = curatorController.Delete(curatorModify).Result;

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
            var curatorModify = new CuratorDisplayModify
            {
                Id = 1,
                Surname = "test1",
                Name = "test1",
                Doublename = "test1",
                Phone = "+375-29-557-06-11"
            };
            _mockCuratorService.Setup(service => service.EditCurator(curatorModify))
                .ReturnsAsync(new HttpResponseMessage {StatusCode = HttpStatusCode.NoContent});
            var curatorController = new CuratorController(_mockCuratorService.Object);

            // Act
            var result = curatorController.Create(curatorModify).Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }

        [Fact]
        public void EditMethod_ReturnsAViewResult_WithAModelCuratorModify_WhenInvalidModel()
        {
            // Arrange
            var curatorModify = new CuratorDisplayModify
            {
                Id = 1,
                Surname = "test1",
                Name = null,
                Doublename = "test1",
                Phone = "+375-29-557-06-11"
            };
            _mockCuratorService.Setup(service => service.EditCurator(curatorModify))
                .ReturnsAsync(new HttpResponseMessage());
            var curatorController = new CuratorController(_mockCuratorService.Object);
            curatorController.ModelState.AddModelError(string.Empty, "Invalid name.");

            // Act
            var result = curatorController.Edit(curatorModify).Result;

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<CuratorDisplayModify>(viewResult.Model);
            Assert.Equal(curatorModify, model);
        }

        [Fact]
        public void EditMethod_ReturnsRedirectToLoginActionHomeController_WhenHttpStatusCodeUnauthorized()
        {
            // Arrange
            var curatorModify = new CuratorDisplayModify
            {
                Id = 1,
                Surname = "test1",
                Name = "test1",
                Doublename = "test1",
                Phone = "+375-29-557-06-11"
            };
            _mockCuratorService.Setup(service => service.EditCurator(curatorModify))
                .Throws(new HttpRequestException(string.Empty, null, HttpStatusCode.Unauthorized));
            var curatorController = new CuratorController(_mockCuratorService.Object);

            // Act
            var result = curatorController.Edit(curatorModify).Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Login", redirect.ActionName);
            Assert.Equal("Home", redirect.ControllerName);
        }

        [Fact]
        public void EditMethod_ReturnsRedirectToErrorActionHomeController_WhenAnyHttpRequestException()
        {
            // Arrange
            var curatorModify = new CuratorDisplayModify
            {
                Id = 1,
                Surname = "test1",
                Name = "test1",
                Doublename = "test1",
                Phone = "+375-29-557-06-11"
            };
            _mockCuratorService.Setup(service => service.EditCurator(curatorModify)).Throws(new HttpRequestException());
            var curatorController = new CuratorController(_mockCuratorService.Object);

            // Act
            var result = curatorController.Edit(curatorModify).Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Error", redirect.ActionName);
            Assert.Equal("Home", redirect.ControllerName);
        }

        #endregion
    }
}
