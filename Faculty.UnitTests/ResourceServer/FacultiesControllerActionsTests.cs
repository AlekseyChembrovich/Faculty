using Moq;
using Xunit;
using System;
using System.Linq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Faculty.Common.Dto.Faculty;
using Faculty.BusinessLayer.Interfaces;
using Faculty.ResourceServer.Controllers;

namespace Faculty.UnitTests.ResourceServer
{
    public class FacultiesControllerActionsTests
    {
        private readonly Mock<IFacultyService> _mockFacultyService;

        public FacultiesControllerActionsTests()
        {
            _mockFacultyService = new Mock<IFacultyService>();
        }

        [Fact]
        public void IndexMethod_ReturnsOkObjectResult_WithListOfCuratorsDisplay_WhenListHaveValues()
        {
            // Arrange
            var facultiesDto = GetFacultiesDto();
            _mockFacultyService.Setup(x => x.GetAll()).Returns(facultiesDto);
            var facultiesController = new FacultiesController(_mockFacultyService.Object);

            // Act
            var result = facultiesController.GetFaculties();

            // Assert
            var viewResult = Assert.IsType<OkObjectResult>(result.Result);
            var models = Assert.IsAssignableFrom<IEnumerable<FacultyDisplayDto>>(viewResult.Value);
            Assert.Equal(3, models.Count());
            facultiesDto.Should().BeEquivalentTo(models);
        }

        [Fact]
        public void IndexMethod_ReturnsNotFoundResult_WithListOfCuratorsDisplay_WhenListHaveNoValues()
        {
            // Arrange
            _mockFacultyService.Setup(x => x.GetAll()).Returns(It.IsAny<IEnumerable<FacultyDisplayDto>>());
            var facultiesController = new FacultiesController(_mockFacultyService.Object);

            // Act
            var result = facultiesController.GetFaculties();

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        private static IEnumerable<FacultyDisplayDto> GetFacultiesDto()
        {
            var facultiesDto = new List<FacultyDisplayDto>()
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
                    StudentSurname = "test2",
                    CuratorSurname = "test2",
                    GroupName = "test2"
                },
                new ()
                {
                    Id = 3,
                    StartDateEducation = DateTime.Now,
                    CountYearEducation = 5,
                    StudentSurname = "test3",
                    CuratorSurname = "test3",
                    GroupName = "test3"
                }
            };

            return facultiesDto;
        }

        [Fact]
        public void CreateMethod_ReturnsCreatedAtActionResult_WhenCorrectModel()
        {
            // Arrange
            const int idNewFaculty = 1;
            var facultyDto = new FacultyDto
            {
                Id = idNewFaculty,
                StartDateEducation = DateTime.Now,
                CountYearEducation = 5,
                CuratorId = 1,
                GroupId = 1,
                StudentId = 1
            };
            _mockFacultyService.Setup(x => x.Create(It.IsAny<FacultyDto>())).Returns(facultyDto);
            var facultiesController = new FacultiesController(_mockFacultyService.Object);

            // Act
            var result = facultiesController.Create(facultyDto);

            // Assert
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public void DeleteMethod_ReturnsNoContextResult_WhenModelWasFind()
        {
            // Arrange
            const int idNewFaculty = 1;
            var curatorDto = new FacultyDto
            {
                Id = 1,
                StartDateEducation = DateTime.Now,
                CountYearEducation = 5,
                CuratorId = 1,
                GroupId = 1,
                StudentId = 1
            };
            _mockFacultyService.Setup(x => x.GetById(idNewFaculty)).Returns(curatorDto);
            _mockFacultyService.Setup(x => x.Delete(idNewFaculty));
            var facultiesController = new FacultiesController(_mockFacultyService.Object);

            // Act
            var result = facultiesController.Delete(idNewFaculty);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteMethod_ReturnsNotFoundResult_WhenModelWasNotFound()
        {
            // Arrange
            const int idExistCurator = 1;
            _mockFacultyService.Setup(x => x.GetById(idExistCurator)).Returns(It.IsAny<FacultyDto>());
            var facultiesController = new FacultiesController(_mockFacultyService.Object);

            // Act
            var result = facultiesController.Delete(idExistCurator);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void EditMethod_ReturnsNoContextResult_WhenModelIsCorrect()
        {
            // Arrange
            const int idExistFaculty = 1;
            var curatorDto = new FacultyDto
            {
                Id = 1,
                StartDateEducation = DateTime.Now,
                CountYearEducation = 5,
                CuratorId = 1,
                GroupId = 1,
                StudentId = 1
            };
            _mockFacultyService.Setup(x => x.GetById(idExistFaculty)).Returns(curatorDto);
            _mockFacultyService.Setup(x => x.Edit(It.IsAny<FacultyDto>()));
            var facultiesController = new FacultiesController(_mockFacultyService.Object);

            // Act
            var result = facultiesController.Edit(curatorDto);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void EditMethod_ReturnsNotFoundResult_WhenModelWasNotFound()
        {
            // Arrange
            const int idExistFaculty = 1;
            var facultyModify = new FacultyDto
            {
                Id = 1,
                StartDateEducation = DateTime.Now,
                CountYearEducation = 4,
                CuratorId = 2,
                GroupId = 2,
                StudentId = 2
            };
            _mockFacultyService.Setup(x => x.GetById(idExistFaculty)).Returns(It.IsAny<FacultyDto>());
            var facultiesController = new FacultiesController(_mockFacultyService.Object);

            // Act
            var result = facultiesController.Edit(facultyModify);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
