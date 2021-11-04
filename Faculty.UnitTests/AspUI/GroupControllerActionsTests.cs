using Moq;
using Xunit;
using System.Net;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Faculty.AspUI.Controllers;
using System.Collections.Generic;
using Faculty.AspUI.ViewModels.Group;
using Faculty.AspUI.Services.Interfaces;

namespace Faculty.UnitTests.AspUI
{
    public class GroupControllerActionsTests
    {
        private readonly Mock<IGroupService> _mockGroupService;
        private readonly Mock<ISpecializationService> _mockSpecializationService;

        public GroupControllerActionsTests()
        {
            _mockGroupService = new Mock<IGroupService>();
            _mockSpecializationService = new Mock<ISpecializationService>();
        }

        #region Index

        [Fact]
        public void IndexMethod_ReturnsAViewResult_WithAListOfGroupDisplay()
        {
            // Arrange
            _mockGroupService.Setup(service => service.GetGroups()).ReturnsAsync(GetGroupsDisplay());
            var groupController = new GroupController(_mockGroupService.Object, _mockSpecializationService.Object);

            // Act
            var result = groupController.Index().Result;

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var models = Assert.IsAssignableFrom<IEnumerable<GroupDisplay>>(viewResult.ViewData.Model);
            Assert.Equal(3, models.Count());
        }

        private static IEnumerable<GroupDisplay> GetGroupsDisplay()
        {
            var groupsDisplay = new List<GroupDisplay>()
            {
                new ()
                {
                    Id = 1,
                    Name = "test1",
                    SpecializationName = "test1"
                },
                new ()
                {
                    Id = 2,
                    Name = "test2",
                    SpecializationName = "test2"
                },
                new ()
                {
                    Id = 3,
                    Name = "test3",
                    SpecializationName = "test3"
                }
            };

            return groupsDisplay;
        }

        [Fact]
        public void IndexMethod_ReturnsRedirectToErrorActionHomeController_WhenAnyHttpRequestException()
        {
            // Arrange
            _mockGroupService.Setup(service => service.GetGroups()).Throws(new HttpRequestException());
            var groupController = new GroupController(_mockGroupService.Object, _mockSpecializationService.Object);

            // Act
            var result = groupController.Index().Result;

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
            var groupAdd = new GroupAdd
            {
                Name = "test1",
                SpecializationId = 1
            };
            _mockGroupService.Setup(service => service.CreateGroup(groupAdd))
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.Created });
            var groupController = new GroupController(_mockGroupService.Object, _mockSpecializationService.Object);

            // Act
            var result = groupController.Create(groupAdd).Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }

        [Fact]
        public void CreateMethod_ReturnsAViewResult_WithAModelGroupAdd_WhenInvalidModel()
        {
            // Arrange
            var groupAdd = new GroupAdd
            {
                Name = null,
                SpecializationId = 1
            };
            _mockGroupService.Setup(service => service.CreateGroup(groupAdd)).ReturnsAsync(new HttpResponseMessage());
            var groupController = new GroupController(_mockGroupService.Object, _mockSpecializationService.Object);
            groupController.ModelState.AddModelError(string.Empty, "Invalid name.");

            // Act
            var result = groupController.Create(groupAdd).Result;

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<GroupAdd>(viewResult.Model);
            Assert.Equal(groupAdd, model);
        }

        [Fact]
        public void CreateMethod_ReturnsRedirectToLoginActionHomeController_WhenHttpStatusCodeUnauthorized()
        {
            // Arrange
            var groupAdd = new GroupAdd
            {
                Name = "test1",
                SpecializationId = 1
            };
            _mockGroupService.Setup(service => service.CreateGroup(groupAdd))
                .Throws(new HttpRequestException(string.Empty, null, HttpStatusCode.Unauthorized));
            var groupController = new GroupController(_mockGroupService.Object, _mockSpecializationService.Object);

            // Act
            var result = groupController.Create(groupAdd).Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Login", redirect.ActionName);
            Assert.Equal("Home", redirect.ControllerName);
        }

        [Fact]
        public void CreateMethod_ReturnsRedirectToErrorActionHomeController_WhenAnyHttpRequestException()
        {
            // Arrange
            var groupAdd = new GroupAdd
            {
                Name = "test1",
                SpecializationId = 1
            };
            _mockGroupService.Setup(service => service.CreateGroup(groupAdd)).Throws(new HttpRequestException());
            var groupController = new GroupController(_mockGroupService.Object, _mockSpecializationService.Object);

            // Act
            var result = groupController.Create(groupAdd).Result;

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
            const int idExistGroup = 1;
            var groupModify = new GroupModify
            {
                Id = 1,
                Name = "test1",
                SpecializationId = 1
            };
            _mockGroupService.Setup(service => service.DeleteGroup(idExistGroup))
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.NoContent });
            var groupController = new GroupController(_mockGroupService.Object, _mockSpecializationService.Object);

            // Act
            var result = groupController.Delete(groupModify).Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }

        [Fact]
        public void DeleteMethod_ReturnsRedirectToLoginActionHomeController_WhenHttpStatusCodeUnauthorized()
        {
            // Arrange
            const int idExistGroup = 1;
            var groupModify = new GroupModify
            {
                Id = 1,
                Name = "test1",
                SpecializationId = 1
            };
            _mockGroupService.Setup(service => service.DeleteGroup(idExistGroup))
                .Throws(new HttpRequestException(string.Empty, null, HttpStatusCode.Unauthorized));
            var groupController = new GroupController(_mockGroupService.Object, _mockSpecializationService.Object);

            // Act
            var result = groupController.Delete(groupModify).Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Login", redirect.ActionName);
            Assert.Equal("Home", redirect.ControllerName);
        }

        [Fact]
        public void DeleteMethod_ReturnsRedirectToErrorActionHomeController_WhenAnyHttpRequestException()
        {
            // Arrange
            var groupModify = new GroupModify
            {
                Id = 1,
                Name = "test1",
                SpecializationId = 1
            };
            _mockGroupService.Setup(service => service.DeleteGroup(It.IsAny<int>())).Throws(new HttpRequestException());
            var groupController = new GroupController(_mockGroupService.Object, _mockSpecializationService.Object);

            // Act
            var result = groupController.Delete(groupModify).Result;

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
            var groupModify = new GroupModify
            {
                Id = 1,
                Name = "test1",
                SpecializationId = 1
            };
            _mockGroupService.Setup(service => service.EditGroup(groupModify))
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.NoContent });
            var groupController = new GroupController(_mockGroupService.Object, _mockSpecializationService.Object);

            // Act
            var result = groupController.Edit(groupModify).Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }

        [Fact]
        public void EditMethod_ReturnsAViewResult_WithAModelGroupModify_WhenInvalidModel()
        {
            // Arrange
            var groupModify = new GroupModify
            {
                Id = 1,
                Name = null,
                SpecializationId = 1
            };
            _mockGroupService.Setup(service => service.EditGroup(groupModify)).ReturnsAsync(new HttpResponseMessage());
            var groupController = new GroupController(_mockGroupService.Object, _mockSpecializationService.Object);
            groupController.ModelState.AddModelError(string.Empty, "Invalid name.");

            // Act
            var result = groupController.Edit(groupModify).Result;

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<GroupModify>(viewResult.Model);
            Assert.Equal(groupModify, model);
        }

        [Fact]
        public void EditMethod_ReturnsRedirectToLoginActionHomeController_WhenHttpStatusCodeUnauthorized()
        {
            // Arrange
            var groupModify = new GroupModify
            {
                Id = 1,
                Name = "test1",
                SpecializationId = 1
            };
            _mockGroupService.Setup(service => service.EditGroup(groupModify))
                .Throws(new HttpRequestException(string.Empty, null, HttpStatusCode.Unauthorized));
            var groupController = new GroupController(_mockGroupService.Object, _mockSpecializationService.Object);

            // Act
            var result = groupController.Edit(groupModify).Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Login", redirect.ActionName);
            Assert.Equal("Home", redirect.ControllerName);
        }

        [Fact]
        public void EditMethod_ReturnsRedirectToErrorActionHomeController_WhenAnyHttpRequestException()
        {
            // Arrange
            var groupModify = new GroupModify
            {
                Id = 1,
                Name = "test1",
                SpecializationId = 1
            };
            _mockGroupService.Setup(service => service.EditGroup(groupModify)).Throws(new HttpRequestException());
            var groupController = new GroupController(_mockGroupService.Object, _mockSpecializationService.Object);

            // Act
            var result = groupController.Edit(groupModify).Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Error", redirect.ActionName);
            Assert.Equal("Home", redirect.ControllerName);
        }

        #endregion
    }
}
