using Moq;
using Xunit;
using System.Net;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Faculty.AspUI.Controllers;
using System.Collections.Generic;
using Faculty.AspUI.Services.Interfaces;
using Faculty.AspUI.ViewModels.Specialization;

namespace Faculty.UnitTests.AspUI
{
    public class SpecializationControllerActionsTests
    {
        private readonly Mock<ISpecializationService> _mockSpecializationService;

        public SpecializationControllerActionsTests()
        {
            _mockSpecializationService = new Mock<ISpecializationService>();
        }

        #region Index

        [Fact]
        public void IndexMethod_ReturnsAViewResult_WithAListOfSpecializationDisplay()
        {
            // Arrange
            _mockSpecializationService.Setup(service => service.GetSpecializations()).ReturnsAsync(GetSpecializationsDisplay());
            var specializationController = new SpecializationController(_mockSpecializationService.Object);

            // Act
            var result = specializationController.Index().Result;

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var models = Assert.IsAssignableFrom<IEnumerable<SpecializationDisplayModify>>(viewResult.ViewData.Model);
            Assert.Equal(3, models.Count());
        }

        private static IEnumerable<SpecializationDisplayModify> GetSpecializationsDisplay()
        {
            var specializationsDisplay = new List<SpecializationDisplayModify>()
            {
                new ()
                {
                    Id = 1,
                    Name = "test1"
                },
                new ()
                {
                    Id = 2,
                    Name = "test2"
                },
                new ()
                {
                    Id = 3,
                    Name = "test3"
                }
            };

            return specializationsDisplay;
        }

        [Fact]
        public void IndexMethod_ReturnsRedirectToErrorActionHomeController_WhenAnyHttpRequestException()
        {
            // Arrange
            _mockSpecializationService.Setup(service => service.GetSpecializations()).Throws(new HttpRequestException());
            var specializationController = new SpecializationController(_mockSpecializationService.Object);

            // Act
            var result = specializationController.Index().Result;

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
            var specializationAdd = new SpecializationAdd
            {
                Name = "test1",
            };
            _mockSpecializationService.Setup(service => service.CreateSpecialization(specializationAdd))
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.Created });
            var specializationController = new SpecializationController(_mockSpecializationService.Object);

            // Act
            var result = specializationController.Create(specializationAdd).Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }

        [Fact]
        public void CreateMethod_ReturnsAViewResult_WithAModelSpecializationAdd_WhenInvalidModel()
        {
            // Arrange
            var specializationAdd = new SpecializationAdd
            {
                Name = null,
            };
            _mockSpecializationService.Setup(service => service.CreateSpecialization(specializationAdd)).ReturnsAsync(new HttpResponseMessage());
            var specializationController = new SpecializationController(_mockSpecializationService.Object);
            specializationController.ModelState.AddModelError(string.Empty, "Invalid name.");

            // Act
            var result = specializationController.Create(specializationAdd).Result;

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<SpecializationAdd>(viewResult.Model);
            Assert.Equal(specializationAdd, model);
        }

        [Fact]
        public void CreateMethod_ReturnsRedirectToLoginActionHomeController_WhenHttpStatusCodeUnauthorized()
        {
            // Arrange
            var specializationAdd = new SpecializationAdd
            {
                Name = "test1",
            };
            _mockSpecializationService.Setup(service => service.CreateSpecialization(specializationAdd))
                .Throws(new HttpRequestException(string.Empty, null, HttpStatusCode.Unauthorized));
            var specializationController = new SpecializationController(_mockSpecializationService.Object);

            // Act
            var result = specializationController.Create(specializationAdd).Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Login", redirect.ActionName);
            Assert.Equal("Home", redirect.ControllerName);
        }

        [Fact]
        public void CreateMethod_ReturnsRedirectToErrorActionHomeController_WhenAnyHttpRequestException()
        {
            // Arrange
            var specializationAdd = new SpecializationAdd
            {
                Name = "test1"
            };
            _mockSpecializationService.Setup(service => service.CreateSpecialization(specializationAdd)).Throws(new HttpRequestException());
            var specializationController = new SpecializationController(_mockSpecializationService.Object);

            // Act
            var result = specializationController.Create(specializationAdd).Result;

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
            const int idExistSpecialization = 1;
            var specializationModify = new SpecializationDisplayModify
            {
                Id = 1,
                Name = "test1"
            };
            _mockSpecializationService.Setup(service => service.DeleteSpecialization(idExistSpecialization))
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.NoContent });
            var specializationController = new SpecializationController(_mockSpecializationService.Object);

            // Act
            var result = specializationController.Delete(specializationModify).Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }

        [Fact]
        public void DeleteMethod_ReturnsRedirectToLoginActionHomeController_WhenHttpStatusCodeUnauthorized()
        {
            // Arrange
            const int idExistSpecialization = 1;
            var specializationModify = new SpecializationDisplayModify
            {
                Id = 1,
                Name = "test1"
            };
            _mockSpecializationService.Setup(service => service.DeleteSpecialization(idExistSpecialization))
                .Throws(new HttpRequestException(string.Empty, null, HttpStatusCode.Unauthorized));
            var specializationController = new SpecializationController(_mockSpecializationService.Object);

            // Act
            var result = specializationController.Delete(specializationModify).Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Login", redirect.ActionName);
            Assert.Equal("Home", redirect.ControllerName);
        }

        [Fact]
        public void DeleteMethod_ReturnsRedirectToErrorActionHomeController_WhenAnyHttpRequestException()
        {
            // Arrange
            var specializationModify = new SpecializationDisplayModify
            {
                Id = 1,
                Name = "test1"
            };
            _mockSpecializationService.Setup(service => service.DeleteSpecialization(It.IsAny<int>())).Throws(new HttpRequestException());
            var specializationController = new SpecializationController(_mockSpecializationService.Object);

            // Act
            var result = specializationController.Delete(specializationModify).Result;

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
            var specializationModify = new SpecializationDisplayModify
            {
                Id = 1,
                Name = "test1"
            };
            _mockSpecializationService.Setup(service => service.EditSpecialization(specializationModify))
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.NoContent });
            var specializationController = new SpecializationController(_mockSpecializationService.Object);

            // Act
            var result = specializationController.Edit(specializationModify).Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }

        [Fact]
        public void EditMethod_ReturnsAViewResult_WithAModelSpecializationModify_WhenInvalidModel()
        {
            // Arrange
            var specializationModify = new SpecializationDisplayModify
            {
                Id = 1,
                Name = null
            };
            _mockSpecializationService.Setup(service => service.EditSpecialization(specializationModify)).ReturnsAsync(new HttpResponseMessage());
            var specializationController = new SpecializationController(_mockSpecializationService.Object);
            specializationController.ModelState.AddModelError(string.Empty, "Invalid name.");

            // Act
            var result = specializationController.Edit(specializationModify).Result;

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<SpecializationDisplayModify>(viewResult.Model);
            Assert.Equal(specializationModify, model);
        }

        [Fact]
        public void EditMethod_ReturnsRedirectToLoginActionHomeController_WhenHttpStatusCodeUnauthorized()
        {
            // Arrange
            var specializationModify = new SpecializationDisplayModify
            {
                Id = 1,
                Name = "test1"
            };
            _mockSpecializationService.Setup(service => service.EditSpecialization(specializationModify))
                .Throws(new HttpRequestException(string.Empty, null, HttpStatusCode.Unauthorized));
            var specializationController = new SpecializationController(_mockSpecializationService.Object);

            // Act
            var result = specializationController.Edit(specializationModify).Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Login", redirect.ActionName);
            Assert.Equal("Home", redirect.ControllerName);
        }

        [Fact]
        public void EditMethod_ReturnsRedirectToErrorActionHomeController_WhenAnyHttpRequestException()
        {
            // Arrange
            var specializationModify = new SpecializationDisplayModify
            {
                Id = 1,
                Name = "test1"
            };
            _mockSpecializationService.Setup(service => service.EditSpecialization(specializationModify)).Throws(new HttpRequestException());
            var specializationController = new SpecializationController(_mockSpecializationService.Object);

            // Act
            var result = specializationController.Edit(specializationModify).Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Error", redirect.ActionName);
            Assert.Equal("Home", redirect.ControllerName);
        }

        #endregion
    }
}
