using Moq;
using Xunit;
using System;
using AutoMapper;
using System.Linq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Faculty.ResourceServer.Tools;
using Faculty.BusinessLayer.Interfaces;
using Faculty.BusinessLayer.Dto.Faculty;
using Faculty.ResourceServer.Controllers;
using Faculty.ResourceServer.Models.Curator;
using Faculty.ResourceServer.Models.Faculty;

namespace Faculty.UnitTests.ResourceServer
{
    public class FacultiesControllerActionsTests
    {
        private readonly Mock<IFacultyService> _mockFacultyService;
        private readonly IMapper _mapper;

        public FacultiesControllerActionsTests()
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            _mapper = new Mapper(mapperConfiguration);
            _mockFacultyService = new Mock<IFacultyService>();
        }

        [Fact]
        public void IndexMethod_ReturnsOkObjectResult_WithListOfCuratorsDisplay_WhenListHaveValues()
        {
            // Arrange
            var facultiesDto = GetFacultiesDto();
            var facultiesDisplay = _mapper.Map<IEnumerable<FacultyDisplay>>(facultiesDto);
            _mockFacultyService.Setup(x => x.GetAll()).Returns(facultiesDto);
            var facultiesController = new FacultiesController(_mockFacultyService.Object, _mapper);

            // Act
            var result = facultiesController.GetFaculties();

            // Assert
            var viewResult = Assert.IsType<OkObjectResult>(result.Result);
            var models = Assert.IsAssignableFrom<IEnumerable<FacultyDisplay>>(viewResult.Value);
            Assert.Equal(3, models.Count());
            facultiesDisplay.Should().BeEquivalentTo(models);
        }

        [Fact]
        public void IndexMethod_ReturnsNotFoundResult_WithListOfCuratorsDisplay_WhenListHaveNoValues()
        {
            // Arrange
            _mockFacultyService.Setup(x => x.GetAll()).Returns(It.IsAny<IEnumerable<FacultyDisplayDto>>());
            var facultiesController = new FacultiesController(_mockFacultyService.Object, _mapper);

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
            var facultyAdd = new FacultyAdd
            {
                StartDateEducation = DateTime.Now,
                CountYearEducation = 5,
                CuratorId = 1,
                GroupId = 1,
                StudentId = 1
            };
            var facultyDto = _mapper.Map<FacultyAdd, FacultyDto>(facultyAdd);
            facultyDto.Id = idNewFaculty;
            _mockFacultyService.Setup(x => x.Create(It.IsAny<FacultyDto>())).Returns(facultyDto);
            var facultiesController = new FacultiesController(_mockFacultyService.Object, _mapper);

            // Act
            var result = facultiesController.Create(facultyAdd);

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
            var facultiesController = new FacultiesController(_mockFacultyService.Object, _mapper);

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
            var facultiesController = new FacultiesController(_mockFacultyService.Object, _mapper);

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
            var facultyModify = new FacultyModify
            {
                Id = 1,
                StartDateEducation = DateTime.Now,
                CountYearEducation = 4,
                CuratorId = 2,
                GroupId = 2,
                StudentId = 2
            };
            _mockFacultyService.Setup(x => x.GetById(idExistFaculty)).Returns(curatorDto);
            _mockFacultyService.Setup(x => x.Edit(It.IsAny<FacultyDto>()));
            var facultiesController = new FacultiesController(_mockFacultyService.Object, _mapper);

            // Act
            var result = facultiesController.Edit(facultyModify);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void EditMethod_ReturnsNotFoundResult_WhenModelWasNotFound()
        {
            // Arrange
            const int idExistFaculty = 1;
            var facultyModify = new FacultyModify
            {
                Id = 1,
                StartDateEducation = DateTime.Now,
                CountYearEducation = 4,
                CuratorId = 2,
                GroupId = 2,
                StudentId = 2
            };
            _mockFacultyService.Setup(x => x.GetById(idExistFaculty)).Returns(It.IsAny<FacultyDto>());
            var facultiesController = new FacultiesController(_mockFacultyService.Object, _mapper);

            // Act
            var result = facultiesController.Edit(facultyModify);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
