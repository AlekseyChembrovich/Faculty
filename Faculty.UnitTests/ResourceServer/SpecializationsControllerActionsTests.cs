using Moq;
using Xunit;
using AutoMapper;
using System.Linq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Faculty.ResourceServer.Tools;
using Faculty.BusinessLayer.Interfaces;
using Faculty.Common.Dto.Specialization;
using Faculty.ResourceServer.Controllers;

namespace Faculty.UnitTests.ResourceServer
{
    public class SpecializationsControllerActionsTests
    {
        private readonly Mock<ISpecializationService> _mockSpecializationService;
        private readonly IMapper _mapper;

        public SpecializationsControllerActionsTests()
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            _mapper = new Mapper(mapperConfiguration);
            _mockSpecializationService = new Mock<ISpecializationService>();
        }

        [Fact]
        public void IndexMethod_ReturnsOkObjectResult_WithListOfCuratorsDisplay_WhenListHaveValues()
        {
            // Arrange
            var specializationsDto = GetSpecializationsDto();
            _mockSpecializationService.Setup(x => x.GetAll()).Returns(specializationsDto);
            var specializationsController = new SpecializationsController(_mockSpecializationService.Object);

            // Act
            var result = specializationsController.GetSpecializations();

            // Assert
            var viewResult = Assert.IsType<OkObjectResult>(result.Result);
            var models = Assert.IsAssignableFrom<IEnumerable<SpecializationDto>>(viewResult.Value);
            Assert.Equal(3, models.Count());
            specializationsDto.Should().BeEquivalentTo(models);
        }

        [Fact]
        public void IndexMethod_ReturnsNotFoundResult_WithListOfCuratorsDisplay_WhenListHaveNoValues()
        {
            // Arrange
            _mockSpecializationService.Setup(x => x.GetAll()).Returns(It.IsAny<IEnumerable<SpecializationDto>>());
            var specializationsController = new SpecializationsController(_mockSpecializationService.Object);

            // Act
            var result = specializationsController.GetSpecializations();

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        private static IEnumerable<SpecializationDto> GetSpecializationsDto()
        {
            var specializationsDto = new List<SpecializationDto>()
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

            return specializationsDto;
        }

        [Fact]
        public void CreateMethod_ReturnsCreatedAtActionResult_WhenCorrectModel()
        {
            // Arrange
            const int idNewSpecialization = 1;
            var specializationDto = new SpecializationDto
            {
                Id = idNewSpecialization,
                Name = "test1"
            };
            _mockSpecializationService.Setup(x => x.Create(It.IsAny<SpecializationDto>())).Returns(specializationDto);
            var specializationsController = new SpecializationsController(_mockSpecializationService.Object);

            // Act
            var result = specializationsController.Create(specializationDto);

            // Assert
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public void DeleteMethod_ReturnsNoContextResult_WhenModelWasFind()
        {
            // Arrange
            const int idExistSpecialization = 1;
            var specializationDto = new SpecializationDto
            {
                Id = 1,
                Name = "test1"
            };
            _mockSpecializationService.Setup(x => x.GetById(idExistSpecialization)).Returns(specializationDto);
            _mockSpecializationService.Setup(x => x.Delete(idExistSpecialization));
            var specializationsController = new SpecializationsController(_mockSpecializationService.Object);

            // Act
            var result = specializationsController.Delete(idExistSpecialization);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteMethod_ReturnsNotFoundResult_WhenModelWasNotFound()
        {
            // Arrange
            const int idExistSpecialization = 1;
            _mockSpecializationService.Setup(x => x.GetById(idExistSpecialization)).Returns(It.IsAny<SpecializationDto>());
            var specializationsController = new SpecializationsController(_mockSpecializationService.Object);

            // Act
            var result = specializationsController.Delete(idExistSpecialization);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void EditMethod_ReturnsNoContextResult_WhenModelIsCorrect()
        {
            // Arrange
            const int idExistSpecialization = 1;
            var specializationDto = new SpecializationDto
            {
                Id = 1,
                Name = "test1"
            };
            _mockSpecializationService.Setup(x => x.GetById(idExistSpecialization)).Returns(specializationDto);
            _mockSpecializationService.Setup(x => x.Edit(It.IsAny<SpecializationDto>()));
            var specializationsController = new SpecializationsController(_mockSpecializationService.Object);

            // Act
            var result = specializationsController.Edit(specializationDto);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void EditMethod_ReturnsNotFoundResult_WhenModelWasNotFound()
        {
            // Arrange
            const int idExistSpecialization = 1;
            var specializationDto = new SpecializationDto
            {
                Id = 1,
                Name = "test2"
            };
            _mockSpecializationService.Setup(x => x.GetById(idExistSpecialization)).Returns(It.IsAny<SpecializationDto>());
            var specializationsController = new SpecializationsController(_mockSpecializationService.Object);

            // Act
            var result = specializationsController.Edit(specializationDto);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
