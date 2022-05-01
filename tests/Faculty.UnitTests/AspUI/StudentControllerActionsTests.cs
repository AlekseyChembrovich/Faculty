using Moq;
using Xunit;
using System.Net;
using AutoMapper;
using System.Linq;
using System.Net.Http;
using Faculty.AspUI.Tools;
using Microsoft.AspNetCore.Mvc;
using Faculty.AspUI.Controllers;
using System.Collections.Generic;
using Faculty.Common.Dto.Student;
using Faculty.AspUI.ViewModels.Student;
using Faculty.AspUI.Services.Interfaces;

namespace Faculty.UnitTests.AspUI
{
    public class StudentControllerActionsTests
    {
        private readonly Mock<IStudentService> _mockStudentService;
        private readonly IMapper _mapper;

        public StudentControllerActionsTests()
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            _mapper = new Mapper(mapperConfiguration);
            _mockStudentService = new Mock<IStudentService>();
        }

        #region Index

        [Fact]
        public void IndexMethod_ReturnsAViewResult_WithAListOfStudentDisplay()
        {
            // Arrange
            _mockStudentService.Setup(service => service.GetStudents()).ReturnsAsync(GetStudentsDto);
            var studentController = new StudentController(_mockStudentService.Object, _mapper);

            // Act
            var result = studentController.Index().Result;

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var models = Assert.IsAssignableFrom<IEnumerable<StudentDisplayModify>>(viewResult.ViewData.Model);
            Assert.Equal(3, models.Count());
        }

        private static IEnumerable<StudentDto> GetStudentsDto()
        {
            var studentsDto = new List<StudentDto>()
            {
                new ()
                {
                    Id = 1,
                    Surname = "test1",
                    Name = "test1",
                    Doublename = "test1"
                },
                new ()
                {
                    Id = 2,
                    Surname = "test2",
                    Name = "test2",
                    Doublename = "test2"
                },
                new ()
                {
                    Id = 3,
                    Surname = "test3",
                    Name = "test3",
                    Doublename = "test3"
                }
            };

            return studentsDto;
        }

        [Fact]
        public void IndexMethod_ReturnsRedirectToErrorActionHomeController_WhenAnyHttpRequestException()
        {
            // Arrange
            _mockStudentService.Setup(service => service.GetStudents()).Throws(new HttpRequestException());
            var studentController = new StudentController(_mockStudentService.Object, _mapper);

            // Act
            var result = studentController.Index().Result;

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
            var studentAdd = new StudentAdd
            {
                Surname = "test1",
                Name = "test1",
                Doublename = "test1"
            };
            var studentDto = _mapper.Map<StudentAdd, StudentDto>(studentAdd);
            _mockStudentService.Setup(service => service.CreateStudent(studentDto))
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.Created });
            var studentController = new StudentController(_mockStudentService.Object, _mapper);

            // Act
            var result = studentController.Create(studentAdd).Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }

        [Fact]
        public void CreateMethod_ReturnsAViewResult_WithAModelStudentAdd_WhenInvalidModel()
        {
            // Arrange
            var studentAdd = new StudentAdd
            {
                Surname = "test1",
                Name = null,
                Doublename = "test1"
            };
            var studentDto = _mapper.Map<StudentAdd, StudentDto>(studentAdd);
            _mockStudentService.Setup(service => service.CreateStudent(studentDto)).ReturnsAsync(new HttpResponseMessage());
            var studentController = new StudentController(_mockStudentService.Object, _mapper);
            studentController.ModelState.AddModelError(string.Empty, "Invalid name.");

            // Act
            var result = studentController.Create(studentAdd).Result;

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<StudentAdd>(viewResult.Model);
            Assert.Equal(studentAdd, model);
        }

        [Fact]
        public void CreateMethod_ReturnsRedirectToLoginActionHomeController_WhenHttpStatusCodeUnauthorized()
        {
            // Arrange
            var studentAdd = new StudentAdd
            {
                Surname = "test1",
                Name = "test1",
                Doublename = "test1"
            };
            var studentDto = _mapper.Map<StudentAdd, StudentDto>(studentAdd);
            _mockStudentService.Setup(service => service.CreateStudent(It.IsAny<StudentDto>()))
                .Throws(new HttpRequestException(string.Empty, null, HttpStatusCode.Unauthorized));
            var studentController = new StudentController(_mockStudentService.Object, _mapper);

            // Act
            var result = studentController.Create(studentAdd).Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Login", redirect.ActionName);
            Assert.Equal("Home", redirect.ControllerName);
        }

        [Fact]
        public void CreateMethod_ReturnsRedirectToErrorActionHomeController_WhenAnyHttpRequestException()
        {
            // Arrange
            var studentAdd = new StudentAdd
            {
                Surname = "test1",
                Name = "test1",
                Doublename = "test1"
            };
            var studentDto = _mapper.Map<StudentAdd, StudentDto>(studentAdd);
            _mockStudentService.Setup(service => service.CreateStudent(It.IsAny<StudentDto>())).Throws(new HttpRequestException());
            var studentController = new StudentController(_mockStudentService.Object, _mapper);

            // Act
            var result = studentController.Create(studentAdd).Result;

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
            const int idExistStudent = 1;
            var studentModify = new StudentDisplayModify
            {
                Id = 1,
                Surname = "test1",
                Name = "test1",
                Doublename = "test1"
            };
            _mockStudentService.Setup(service => service.DeleteStudent(idExistStudent))
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.NoContent });
            var studentController = new StudentController(_mockStudentService.Object, _mapper);

            // Act
            var result = studentController.Delete(studentModify).Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }

        [Fact]
        public void DeleteMethod_ReturnsRedirectToLoginActionHomeController_WhenHttpStatusCodeUnauthorized()
        {
            // Arrange
            const int idExistStudent = 1;
            var studentModify = new StudentDisplayModify
            {
                Id = 1,
                Surname = "test1",
                Name = "test1",
                Doublename = "test1"
            };
            _mockStudentService.Setup(service => service.DeleteStudent(idExistStudent))
                .Throws(new HttpRequestException(string.Empty, null, HttpStatusCode.Unauthorized));
            var studentController = new StudentController(_mockStudentService.Object, _mapper);

            // Act
            var result = studentController.Delete(studentModify).Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Login", redirect.ActionName);
            Assert.Equal("Home", redirect.ControllerName);
        }

        [Fact]
        public void DeleteMethod_ReturnsRedirectToErrorActionHomeController_WhenAnyHttpRequestException()
        {
            // Arrange
            var studentModify = new StudentDisplayModify
            {
                Id = 1,
                Surname = "test1",
                Name = "test1",
                Doublename = "test1"
            };
            _mockStudentService.Setup(service => service.DeleteStudent(It.IsAny<int>())).Throws(new HttpRequestException());
            var studentController = new StudentController(_mockStudentService.Object, _mapper);

            // Act
            var result = studentController.Delete(studentModify).Result;

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
            var studentModify = new StudentDisplayModify
            {
                Id = 1,
                Surname = "test1",
                Name = "test1",
                Doublename = "test1"
            };
            var studentDto = _mapper.Map<StudentDisplayModify, StudentDto>(studentModify);
            _mockStudentService.Setup(service => service.EditStudent(studentDto))
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.NoContent });
            var studentController = new StudentController(_mockStudentService.Object, _mapper);

            // Act
            var result = studentController.Edit(studentModify).Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }

        [Fact]
        public void EditMethod_ReturnsAViewResult_WithAModelStudentModify_WhenInvalidModel()
        {
            // Arrange
            var studentModify = new StudentDisplayModify
            {
                Id = 1,
                Surname = "test1",
                Name = null,
                Doublename = "test1"
            };
            var studentDto = _mapper.Map<StudentDisplayModify, StudentDto>(studentModify);
            _mockStudentService.Setup(service => service.EditStudent(studentDto)).ReturnsAsync(new HttpResponseMessage());
            var studentController = new StudentController(_mockStudentService.Object, _mapper);
            studentController.ModelState.AddModelError(string.Empty, "Invalid name.");

            // Act
            var result = studentController.Edit(studentModify).Result;

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<StudentDisplayModify>(viewResult.Model);
            Assert.Equal(studentModify, model);
        }

        [Fact]
        public void EditMethod_ReturnsRedirectToLoginActionHomeController_WhenHttpStatusCodeUnauthorized()
        {
            // Arrange
            var studentModify = new StudentDisplayModify
            {
                Id = 1,
                Surname = "test1",
                Name = "test1",
                Doublename = "test1"
            };
            var studentDto = _mapper.Map<StudentDisplayModify, StudentDto>(studentModify);
            _mockStudentService.Setup(service => service.EditStudent(It.IsAny<StudentDto>()))
                .Throws(new HttpRequestException(string.Empty, null, HttpStatusCode.Unauthorized));
            var studentController = new StudentController(_mockStudentService.Object, _mapper);

            // Act
            var result = studentController.Edit(studentModify).Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Login", redirect.ActionName);
            Assert.Equal("Home", redirect.ControllerName);
        }

        [Fact]
        public void EditMethod_ReturnsRedirectToErrorActionHomeController_WhenAnyHttpRequestException()
        {
            // Arrange
            var studentModify = new StudentDisplayModify
            {
                Id = 1,
                Surname = "test1",
                Name = "test1",
                Doublename = "test1"
            };
            var studentDto = _mapper.Map<StudentDisplayModify, StudentDto>(studentModify);
            _mockStudentService.Setup(service => service.EditStudent(It.IsAny<StudentDto>())).Throws(new HttpRequestException());
            var studentController = new StudentController(_mockStudentService.Object, _mapper);

            // Act
            var result = studentController.Edit(studentModify).Result;

            // Assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Error", redirect.ActionName);
            Assert.Equal("Home", redirect.ControllerName);
        }

        #endregion
    }
}
