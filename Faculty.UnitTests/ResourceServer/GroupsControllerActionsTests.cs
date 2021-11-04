using Moq;
using Xunit;
using AutoMapper;
using System.Linq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Faculty.ResourceServer.Tools;
using Faculty.BusinessLayer.Dto.Group;
using Faculty.BusinessLayer.Interfaces;
using Faculty.ResourceServer.Controllers;
using Faculty.ResourceServer.Models.Group;

namespace Faculty.UnitTests.ResourceServer
{
    public class GroupsControllerActionsTests
    {
        private readonly Mock<IGroupService> _mockGroupService;
        private readonly IMapper _mapper;

        public GroupsControllerActionsTests()
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            _mapper = new Mapper(mapperConfiguration);
            _mockGroupService = new Mock<IGroupService>();
        }

        [Fact]
        public void IndexMethod_ReturnsOkObjectResult_WithListOfCuratorsDisplay_WhenListHaveValues()
        {
            // Arrange
            var groupsDto = GetGroupsDto();
            var groupsDisplay = _mapper.Map<IEnumerable<GroupDisplay>>(groupsDto);
            _mockGroupService.Setup(x => x.GetAll()).Returns(groupsDto);
            var groupsController = new GroupsController(_mockGroupService.Object, _mapper);

            // Act
            var result = groupsController.GetGroups();

            // Assert
            var viewResult = Assert.IsType<OkObjectResult>(result.Result);
            var models = Assert.IsAssignableFrom<IEnumerable<GroupDisplay>>(viewResult.Value);
            Assert.Equal(3, models.Count());
            groupsDisplay.Should().BeEquivalentTo(models);
        }

        [Fact]
        public void IndexMethod_ReturnsNotFoundResult_WithListOfCuratorsDisplay_WhenListHaveNoValues()
        {
            // Arrange
            _mockGroupService.Setup(x => x.GetAll()).Returns(It.IsAny<IEnumerable<GroupDisplayDto>>());
            var groupsController = new GroupsController(_mockGroupService.Object, _mapper);

            // Act
            var result = groupsController.GetGroups();

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        private static IEnumerable<GroupDisplayDto> GetGroupsDto()
        {
            var groupsDto = new List<GroupDisplayDto>()
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

            return groupsDto;
        }

        [Fact]
        public void CreateMethod_ReturnsCreatedAtActionResult_WhenCorrectModel()
        {
            // Arrange
            const int idNewGroup = 1;
            var groupAdd = new GroupAdd
            {
                Name = "test1",
                SpecializationId = 1
            };
            var groupDto = _mapper.Map<GroupAdd, GroupDto>(groupAdd);
            groupDto.Id = idNewGroup;
            _mockGroupService.Setup(x => x.Create(It.IsAny<GroupDto>())).Returns(groupDto);
            var groupsController = new GroupsController(_mockGroupService.Object, _mapper);

            // Act
            var result = groupsController.Create(groupAdd);

            // Assert
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public void DeleteMethod_ReturnsNoContextResult_WhenModelWasFind()
        {
            // Arrange
            const int idNewGroup = 1;
            var groupDto = new GroupDto
            {
                Id = 1,
                Name = "test1",
                SpecializationId = 1
            };
            _mockGroupService.Setup(x => x.GetById(idNewGroup)).Returns(groupDto);
            _mockGroupService.Setup(x => x.Delete(idNewGroup));
            var groupsController = new GroupsController(_mockGroupService.Object, _mapper);

            // Act
            var result = groupsController.Delete(idNewGroup);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteMethod_ReturnsNotFoundResult_WhenModelWasNotFound()
        {
            // Arrange
            const int idNewGroup = 1;
            _mockGroupService.Setup(x => x.GetById(idNewGroup)).Returns(It.IsAny<GroupDto>());
            var groupsController = new GroupsController(_mockGroupService.Object, _mapper);

            // Act
            var result = groupsController.Delete(idNewGroup);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void EditMethod_ReturnsNoContextResult_WhenModelIsCorrect()
        {
            // Arrange
            const int idExistGroup = 1;
            var curatorDto = new GroupDto
            {
                Id = 1,
                Name = "test1",
                SpecializationId = 1
            };
            var groupModify = new GroupModify
            {
                Id = 1,
                Name = "test2",
                SpecializationId = 2
            };
            _mockGroupService.Setup(x => x.GetById(idExistGroup)).Returns(curatorDto);
            _mockGroupService.Setup(x => x.Edit(It.IsAny<GroupDto>()));
            var groupsController = new GroupsController(_mockGroupService.Object, _mapper);

            // Act
            var result = groupsController.Edit(groupModify);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void EditMethod_ReturnsNotFoundResult_WhenModelWasNotFound()
        {
            // Arrange
            const int idExistGroup = 1;
            var groupModify = new GroupModify
            {
                Id = 1,
                Name = "test2",
                SpecializationId = 2
            };
            _mockGroupService.Setup(x => x.GetById(idExistGroup)).Returns(It.IsAny<GroupDto>());
            var groupsController = new GroupsController(_mockGroupService.Object, _mapper);

            // Act
            var result = groupsController.Edit(groupModify);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
