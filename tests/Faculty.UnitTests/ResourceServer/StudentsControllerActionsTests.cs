using Moq;
using Xunit;
using AutoMapper;
using System.Linq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Faculty.Common.Dto.Student;
using Faculty.ResourceServer.Tools;
using Faculty.BusinessLayer.Interfaces;
using Faculty.ResourceServer.Controllers;

namespace Faculty.UnitTests.ResourceServer
{
    public class StudentsControllerActionsTests
    {
        private readonly Mock<IStudentService> _mockStudentService;
        private readonly IMapper _mapper;

        public StudentsControllerActionsTests()
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            _mapper = new Mapper(mapperConfiguration);
            _mockStudentService = new Mock<IStudentService>();
        }

        [Fact]
        public void IndexMethod_ReturnsOkObjectResult_WithListOfCuratorsDisplay_WhenListHaveValues()
        {
            // Arrange
            var studentsDto = GetStudentsDto();
            _mockStudentService.Setup(x => x.GetAll()).Returns(studentsDto);
            var studentsController = new StudentsController(_mockStudentService.Object);

            // Act
            var result = studentsController.GetStudents();

            // Assert
            var viewResult = Assert.IsType<OkObjectResult>(result.Result);
            var models = Assert.IsAssignableFrom<IEnumerable<StudentDto>>(viewResult.Value);
            Assert.Equal(3, models.Count());
            studentsDto.Should().BeEquivalentTo(models);
        }

        [Fact]
        public void IndexMethod_ReturnsNotFoundResult_WithListOfCuratorsDisplay_WhenListHaveNoValues()
        {
            // Arrange
            _mockStudentService.Setup(x => x.GetAll()).Returns(It.IsAny<IEnumerable<StudentDto>>());
            var studentsController = new StudentsController(_mockStudentService.Object);

            // Act
            var result = studentsController.GetStudents();

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
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
        public void CreateMethod_ReturnsCreatedAtActionResult_WhenCorrectModel()
        {
            // Arrange
            const int idNewStudent = 1;
            var studentDto = new StudentDto
            {
                Id = idNewStudent,
                Surname = "test1",
                Name = "test1",
                Doublename = "test1"
            };
            _mockStudentService.Setup(x => x.Create(It.IsAny<StudentDto>())).Returns(studentDto);
            var studentsController = new StudentsController(_mockStudentService.Object);

            // Act
            var result = studentsController.Create(studentDto);

            // Assert
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public void DeleteMethod_ReturnsNoContextResult_WhenModelWasFind()
        {
            // Arrange
            const int idExistStudent = 1;
            var studentDto = new StudentDto
            {
                Id = 1,
                Surname = "test1",
                Name = "test1",
                Doublename = "test1"
            };
            _mockStudentService.Setup(x => x.GetById(idExistStudent)).Returns(studentDto);
            _mockStudentService.Setup(x => x.Delete(idExistStudent));
            var studentsController = new StudentsController(_mockStudentService.Object);

            // Act
            var result = studentsController.Delete(idExistStudent);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteMethod_ReturnsNotFoundResult_WhenModelWasNotFound()
        {
            // Arrange
            const int idExistStudent = 1;
            _mockStudentService.Setup(x => x.GetById(idExistStudent)).Returns(It.IsAny<StudentDto>());
            var studentsController = new StudentsController(_mockStudentService.Object);

            // Act
            var result = studentsController.Delete(idExistStudent);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void EditMethod_ReturnsNoContextResult_WhenModelIsCorrect()
        {
            // Arrange
            const int idExistStudent = 1;
            var studentDto = new StudentDto
            {
                Id = 1,
                Surname = "test1",
                Name = "test1",
                Doublename = "test1"
            };
            _mockStudentService.Setup(x => x.GetById(idExistStudent)).Returns(studentDto);
            _mockStudentService.Setup(x => x.Edit(It.IsAny<StudentDto>()));
            var studentsController = new StudentsController(_mockStudentService.Object);

            // Act
            var result = studentsController.Edit(studentDto);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void EditMethod_ReturnsNotFoundResult_WhenModelWasNotFound()
        {
            // Arrange
            const int idExistStudent = 1;
            var studentDto = new StudentDto
            {
                Id = 1,
                Surname = "test2",
                Name = "test2",
                Doublename = "test2"
            };
            _mockStudentService.Setup(x => x.GetById(idExistStudent)).Returns(It.IsAny<StudentDto>());
            var studentsController = new StudentsController(_mockStudentService.Object);

            // Act
            var result = studentsController.Edit(studentDto);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
