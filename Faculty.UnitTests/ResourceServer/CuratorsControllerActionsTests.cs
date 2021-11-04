using Moq;
using Xunit;
using AutoMapper;
using System.Linq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Faculty.ResourceServer.Tools;
using Faculty.BusinessLayer.Interfaces;
using Faculty.BusinessLayer.Dto.Curator;
using Faculty.ResourceServer.Controllers;
using Faculty.ResourceServer.Models.Curator;

namespace Faculty.UnitTests.ResourceServer
{
    public class CuratorsControllerActionsTests
    {
        private readonly Mock<ICuratorService> _mockCuratorService;
        private readonly IMapper _mapper;

        public CuratorsControllerActionsTests()
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            _mapper = new Mapper(mapperConfiguration);
            _mockCuratorService = new Mock<ICuratorService>();
        }

        [Fact]
        public void IndexMethod_ReturnsOkObjectResult_WithListOfCuratorsDisplay_WhenListHaveValues()
        {
            // Arrange
            var curatorsDto = GetCuratorsDto();
            var curatorsDisplay = _mapper.Map<IEnumerable<CuratorDisplayModify>>(curatorsDto);
            _mockCuratorService.Setup(x => x.GetAll()).Returns(curatorsDto);
            var curatorsController = new CuratorsController(_mockCuratorService.Object, _mapper);

            // Act
            var result = curatorsController.GetCurators();

            // Assert
            var viewResult = Assert.IsType<OkObjectResult>(result.Result);
            var models = Assert.IsAssignableFrom<IEnumerable<CuratorDisplayModify>>(viewResult.Value);
            Assert.Equal(3, models.Count());
            curatorsDisplay.Should().BeEquivalentTo(models);
        }

        [Fact]
        public void IndexMethod_ReturnsNotFoundResult_WithListOfCuratorsDisplay_WhenListHaveNoValues()
        {
            // Arrange
            _mockCuratorService.Setup(x => x.GetAll()).Returns(It.IsAny<IEnumerable<CuratorDto>>());
            var curatorsController = new CuratorsController(_mockCuratorService.Object, _mapper);

            // Act
            var result = curatorsController.GetCurators();

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        private static IEnumerable<CuratorDto> GetCuratorsDto()
        {
            var curatorsDto = new List<CuratorDto>()
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

            return curatorsDto;
        }

        [Fact]
        public void CreateMethod_ReturnsCreatedAtActionResult_WhenCorrectModel()
        {
            // Arrange
            const int idNewCurator = 1;
            var curatorAdd = new CuratorAdd
            {
                Surname = "test1",
                Name = "test1",
                Doublename = "test1",
                Phone = "+375-29-557-06-11"
            };
            var curatorDto = _mapper.Map<CuratorAdd, CuratorDto>(curatorAdd);
            curatorDto.Id = idNewCurator;
            _mockCuratorService.Setup(x => x.Create(It.IsAny<CuratorDto>())).Returns(curatorDto);
            var curatorsController = new CuratorsController(_mockCuratorService.Object, _mapper);

            // Act
            var result = curatorsController.Create(curatorAdd);

            // Assert
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public void DeleteMethod_ReturnsNoContextResult_WhenModelWasFind()
        {
            // Arrange
            const int idExistCurator = 1;
            var curatorDto = new CuratorDto
            {
                Id = 1,
                Surname = "test1",
                Name = "test1",
                Doublename = "test1",
                Phone = "+375-29-557-06-11"
            };
            _mockCuratorService.Setup(x => x.GetById(idExistCurator)).Returns(curatorDto);
            _mockCuratorService.Setup(x => x.Delete(idExistCurator));
            var curatorsController = new CuratorsController(_mockCuratorService.Object, _mapper);

            // Act
            var result = curatorsController.Delete(idExistCurator);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteMethod_ReturnsNotFoundResult_WhenModelWasNotFound()
        {
            // Arrange
            const int idExistCurator = 1;
            _mockCuratorService.Setup(x => x.GetById(idExistCurator)).Returns(It.IsAny<CuratorDto>());
            var curatorsController = new CuratorsController(_mockCuratorService.Object, _mapper);

            // Act
            var result = curatorsController.Delete(idExistCurator);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void EditMethod_ReturnsNoContextResult_WhenModelIsCorrect()
        {
            // Arrange
            const int idExistCurator = 1;
            var curatorDto = new CuratorDto
            {
                Id = 1,
                Surname = "test1",
                Name = "test1",
                Doublename = "test1",
                Phone = "+375-29-557-06-11"
            };
            var curatorModify = new CuratorDisplayModify
            {
                Id = 1,
                Surname = "test2",
                Name = "test2",
                Doublename = "test2",
                Phone = "+375-29-557-06-22"
            };
            _mockCuratorService.Setup(x => x.GetById(idExistCurator)).Returns(curatorDto);
            _mockCuratorService.Setup(x => x.Edit(It.IsAny<CuratorDto>()));
            var curatorsController = new CuratorsController(_mockCuratorService.Object, _mapper);

            // Act
            var result = curatorsController.Edit(curatorModify);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void EditMethod_ReturnsNotFoundResult_WhenModelWasNotFound()
        {
            // Arrange
            const int idExistCurator = 1;
            var curatorModify = new CuratorDisplayModify
            {
                Id = 1,
                Surname = "test2",
                Name = "test2",
                Doublename = "test2",
                Phone = "+375-29-557-06-22"
            };
            _mockCuratorService.Setup(x => x.GetById(idExistCurator)).Returns(It.IsAny<CuratorDto>());
            var curatorsController = new CuratorsController(_mockCuratorService.Object, _mapper);

            // Act
            var result = curatorsController.Edit(curatorModify);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
