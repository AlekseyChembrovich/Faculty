using Moq;
using Xunit;
using System;
using System.Net;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Faculty.AspUI.Controllers;
using System.Collections.Generic;
using Faculty.AspUI.ViewModels.Faculty;
using Faculty.AspUI.Services.Interfaces;

namespace Faculty.UnitTests.AspUI
{
    public class FacultyControllerActionsTests
    {
        private readonly Mock<IFacultyService> _mockFacultyService;
        private readonly Mock<IGroupService> _mockGroupService;
        private readonly Mock<IStudentService> _mockStudentService;
        private readonly Mock<ICuratorService> _mockCuratorService;

        public FacultyControllerActionsTests()
        {
            _mockFacultyService = new Mock<IFacultyService>();
            _mockGroupService = new Mock<IGroupService>();
            _mockStudentService = new Mock<IStudentService>();
            _mockCuratorService = new Mock<ICuratorService>();
        }

        #region Index

        [Fact]
        public void IndexMethod_ReturnsAViewResult_WithAListOfFacultyDisplay()
        {
            // Arrange
            _mockFacultyService.Setup(service => service.GetFaculties()).ReturnsAsync(GetFacultiesDisplay());
            var facultyController = new FacultyController(_mockFacultyService.Object, _mockGroupService.Object,
                _mockStudentService.Object, _mockCuratorService.Object);

            // Act
            var result = facultyController.Index().Result;

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var models = Assert.IsAssignableFrom<IEnumerable<FacultyDisplay>>(viewResult.ViewData.Model);
            Assert.Equal(3, models.Count());
        }

        private static IEnumerable<FacultyDisplay> GetFacultiesDisplay()
        {
            var facultiesDisplay = new List<FacultyDisplay>()
            {
                new ()
                {
                    Id = 1,
                    StartDateEducation = DateTime.Now,
                    CountYearEducation = 5,
                    StudentSurname = "test1",
                    CuratorSurname = "test1",
                    GroupName = "test1"
                },
                new ()
                {
                    Id = 2,
                    StartDateEducation = DateTime.Now,
                    CountYearEducation = 4,
                    StudentSurname = "test4",
                    CuratorSurname = "test4",
                    GroupName = "test4"
                },
                new ()
                {
                    Id = 3,
                    StartDateEducation = DateTime.Now,
                    CountYearEducation = 5,
                    StudentSurname = "test5",
                    CuratorSurname = "test5",
                    GroupName = "test5"
                }
            };

            return facultiesDisplay;
        }

        [Fact]
        public void IndexMethod_ReturnsRedirectToErrorActionHomeController_WhenAnyHttpRequestException()
        {
            // Arrange
            _mockFacultyService.Setup(service => service.GetFaculties()).Throws(new HttpRequestException());
            var facultyController = new FacultyController(_mockFacultyService.Object, _mockGroupService.Object,
                _mockStudentService.Object, _mockCuratorService.Object);

            // Act
            var result = facultyController.Index().Result;

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
            var facultyAdd = new FacultyAdd
            {
                StartDateEducation = DateTime.Now,
                CountYearEducation = 5,
                StudentId = 1,
                CuratorId = 1,
                GroupId = 1
            };
            _mockFacultyService.Setup(service => service.CreateFaculty(facultyAdd))
                .ReturnsAsync(new HttpResponseMessage {StatusCode = HttpStatusCode.Created});
            var facultyController = new FacultyController(_mockFacultyService.Object, _mockGroupService.Object,
                _mockStudentService.Object, _mockCuratorService.Object);

            // Act
            var result = facultyController.Create(facultyAdd).Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }

        [Fact]
        public void CreateMethod_ReturnsAViewResult_WithAModelFacultyAdd_WhenInvalidModel()
        {
            // Arrange
            var facultyAdd = new FacultyAdd
            {
                StartDateEducation = DateTime.Now,
                CountYearEducation = 0,
                StudentId = 1,
                CuratorId = 1,
                GroupId = 1
            };
            _mockFacultyService.Setup(service => service.CreateFaculty(facultyAdd)).ReturnsAsync(new HttpResponseMessage());
            var facultyController = new FacultyController(_mockFacultyService.Object, _mockGroupService.Object,
                _mockStudentService.Object, _mockCuratorService.Object);
            facultyController.ModelState.AddModelError(string.Empty, "Invalid count year education.");

            // Act
            var result = facultyController.Create(facultyAdd).Result;

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<FacultyAdd>(viewResult.Model);
            Assert.Equal(facultyAdd, model);
        }

        [Fact]
        public void CreateMethod_ReturnsRedirectToLoginActionHomeController_WhenHttpStatusCodeUnauthorized()
        {
            // Arrange
            var facultyAdd = new FacultyAdd
            {
                StartDateEducation = DateTime.Now,
                CountYearEducation = 5,
                StudentId = 1,
                CuratorId = 1,
                GroupId = 1
            };
            _mockFacultyService.Setup(service => service.CreateFaculty(facultyAdd))
                .Throws(new HttpRequestException(string.Empty, null, HttpStatusCode.Unauthorized));
            var facultyController = new FacultyController(_mockFacultyService.Object, _mockGroupService.Object,
                _mockStudentService.Object, _mockCuratorService.Object);

            // Act
            var result = facultyController.Create(facultyAdd).Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Login", redirect.ActionName);
            Assert.Equal("Home", redirect.ControllerName);
        }

        [Fact]
        public void CreateMethod_ReturnsRedirectToErrorActionHomeController_WhenAnyHttpRequestException()
        {
            // Arrange
            var facultyAdd = new FacultyAdd
            {
                StartDateEducation = DateTime.Now,
                CountYearEducation = 5,
                StudentId = 1,
                CuratorId = 1,
                GroupId = 1
            };
            _mockFacultyService.Setup(service => service.CreateFaculty(facultyAdd)).Throws(new HttpRequestException());
            var facultyController = new FacultyController(_mockFacultyService.Object, _mockGroupService.Object,
                _mockStudentService.Object, _mockCuratorService.Object);

            // Act
            var result = facultyController.Create(facultyAdd).Result;

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
            const int idExistFaculty = 1;
            var facultyModify = new FacultyModify
            {
                Id = 1,
                StartDateEducation = DateTime.Now,
                CountYearEducation = 5,
                StudentId = 1,
                CuratorId = 1,
                GroupId = 1
            };
            _mockFacultyService.Setup(service => service.DeleteFaculty(idExistFaculty))
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.NoContent });
            var facultyController = new FacultyController(_mockFacultyService.Object, _mockGroupService.Object,
                _mockStudentService.Object, _mockCuratorService.Object);

            // Act
            var result = facultyController.Delete(facultyModify).Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }

        [Fact]
        public void DeleteMethod_ReturnsRedirectToLoginActionHomeController_WhenHttpStatusCodeUnauthorized()
        {
            // Arrange
            const int idExistFaculty = 1;
            var facultyModify = new FacultyModify
            {
                Id = 1,
                StartDateEducation = DateTime.Now,
                CountYearEducation = 5,
                StudentId = 1,
                CuratorId = 1,
                GroupId = 1
            };
            _mockFacultyService.Setup(service => service.DeleteFaculty(idExistFaculty))
                .Throws(new HttpRequestException(string.Empty, null, HttpStatusCode.Unauthorized));
            var facultyController = new FacultyController(_mockFacultyService.Object, _mockGroupService.Object,
                _mockStudentService.Object, _mockCuratorService.Object);

            // Act
            var result = facultyController.Delete(facultyModify).Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Login", redirect.ActionName);
            Assert.Equal("Home", redirect.ControllerName);
        }

        [Fact]
        public void DeleteMethod_ReturnsRedirectToErrorActionHomeController_WhenAnyHttpRequestException()
        {
            // Arrange
            var facultyModify = new FacultyModify
            {
                Id = 1,
                StartDateEducation = DateTime.Now,
                CountYearEducation = 5,
                StudentId = 1,
                CuratorId = 1,
                GroupId = 1
            };
            _mockFacultyService.Setup(service => service.DeleteFaculty(It.IsAny<int>())).Throws(new HttpRequestException());
            var facultyController = new FacultyController(_mockFacultyService.Object, _mockGroupService.Object,
                _mockStudentService.Object, _mockCuratorService.Object);

            // Act
            var result = facultyController.Delete(facultyModify).Result;

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
            var facultyModify = new FacultyModify
            {
                Id = 1,
                StartDateEducation = DateTime.Now,
                CountYearEducation = 5,
                StudentId = 1,
                CuratorId = 1,
                GroupId = 1
            };
            _mockFacultyService.Setup(service => service.EditFaculty(facultyModify))
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.NoContent });
            var facultyController = new FacultyController(_mockFacultyService.Object, _mockGroupService.Object,
                _mockStudentService.Object, _mockCuratorService.Object);

            // Act
            var result = facultyController.Edit(facultyModify).Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }

        [Fact]
        public void EditMethod_ReturnsAViewResult_WithAModelFacultyModify_WhenInvalidModel()
        {
            // Arrange
            var facultyModify = new FacultyModify
            {
                Id = 1,
                StartDateEducation = DateTime.Now,
                CountYearEducation = 0,
                StudentId = 1,
                CuratorId = 1,
                GroupId = 1
            };
            _mockFacultyService.Setup(service => service.EditFaculty(facultyModify))
                .ReturnsAsync(new HttpResponseMessage());
            var facultyController = new FacultyController(_mockFacultyService.Object, _mockGroupService.Object,
                _mockStudentService.Object, _mockCuratorService.Object);
            facultyController.ModelState.AddModelError(string.Empty, "Invalid count year education.");

            // Act
            var result = facultyController.Edit(facultyModify).Result;

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<FacultyModify>(viewResult.Model);
            Assert.Equal(facultyModify, model);
        }

        [Fact]
        public void EditMethod_ReturnsRedirectToLoginActionHomeController_WhenHttpStatusCodeUnauthorized()
        {
            // Arrange
            var facultyModify = new FacultyModify
            {
                Id = 1,
                StartDateEducation = DateTime.Now,
                CountYearEducation = 5,
                StudentId = 1,
                CuratorId = 1,
                GroupId = 1
            };
            _mockFacultyService.Setup(service => service.EditFaculty(facultyModify))
                .Throws(new HttpRequestException(string.Empty, null, HttpStatusCode.Unauthorized));
            var facultyController = new FacultyController(_mockFacultyService.Object, _mockGroupService.Object,
                _mockStudentService.Object, _mockCuratorService.Object);

            // Act
            var result = facultyController.Edit(facultyModify).Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Login", redirect.ActionName);
            Assert.Equal("Home", redirect.ControllerName);
        }

        [Fact]
        public void EditMethod_ReturnsRedirectToErrorActionHomeController_WhenAnyHttpRequestException()
        {
            // Arrange
            var facultyModify = new FacultyModify
            {
                Id = 1,
                StartDateEducation = DateTime.Now,
                CountYearEducation = 5,
                StudentId = 1,
                CuratorId = 1,
                GroupId = 1
            };
            _mockFacultyService.Setup(service => service.EditFaculty(facultyModify)).Throws(new HttpRequestException());
            var facultyController = new FacultyController(_mockFacultyService.Object, _mockGroupService.Object,
                _mockStudentService.Object, _mockCuratorService.Object);

            // Act
            var result = facultyController.Edit(facultyModify).Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Error", redirect.ActionName);
            Assert.Equal("Home", redirect.ControllerName);
        }

        #endregion
    }
}
