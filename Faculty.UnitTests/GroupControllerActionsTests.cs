using Moq;
using Xunit;
using AutoMapper;
using System.Linq;
using FluentAssertions;
using Faculty.AspUI.Models;
using Faculty.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Faculty.AspUI.Controllers;
using System.Collections.Generic;
using Faculty.DataAccessLayer.Models;
using Faculty.BusinessLayer.Services;
using Faculty.BusinessLayer.ModelsDto.GroupDto;
using Faculty.DataAccessLayer.RepositoryEntityFramework;

namespace Faculty.UnitTests
{
    public class GroupControllerActionsTests
    {
        private readonly Mock<IRepositoryGroup> _mockRepositoryGroup;
        private readonly Mock<IRepository<Specialization>> _mockRepositorySpecialization;

        public GroupControllerActionsTests()
        {
            _mockRepositoryGroup = new Mock<IRepositoryGroup>();
            _mockRepositorySpecialization = new Mock<IRepository<Specialization>>();
        }

        [Fact]
        public void IndexMethod_ReturnsViewResult_WithListOfDisplayModelsDisplay()
        {
            // Arrange
            _mockRepositoryGroup.Setup(repository => repository.GetAllIncludeForeignKey()).Returns(GetTestModels()).Verifiable();
            var modelService = new GroupService(_mockRepositoryGroup.Object, _mockRepositorySpecialization.Object);
            var modelController = new GroupController(modelService);

            // Act
            var result = modelController.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var models = Assert.IsAssignableFrom<IEnumerable<GroupDisplay>>(viewResult.ViewData.Model);
            Assert.Equal(3, models.Count());
            _mockRepositoryGroup.Verify(r => r.GetAllIncludeForeignKey());
        }

        private static IEnumerable<Group> GetTestModels()
        {
            var models = new List<Group>()
            {
                new ()
                {
                    Id = 1,
                    Name = "test1",
                    SpecializationId = 1
                },
                new ()
                {
                    Id = 2,
                    Name = "test2",
                    SpecializationId = 2
                },
                new ()
                {
                    Id = 3,
                    Name = "test3",
                    SpecializationId = 3
                }
            };

            return models;
        }

        [Fact]
        public void CreateMethod_CallInsertMethodRepository_RedirectToIndexMethodWith_ForCorrectModel()
        {
            // Arrange
            var modelModify = new GroupModify { Name = "test1", SpecializationId = 1 };
            Mapper.Initialize(cfg => cfg.CreateMap<GroupModify, CreateGroupDto>());
            var modelDto = Mapper.Map<GroupModify, CreateGroupDto>(modelModify);
            Mapper.Initialize(cfg => cfg.CreateMap<CreateGroupDto, Group>());
            var model = Mapper.Map<CreateGroupDto, Group>(modelDto);
            _mockRepositoryGroup.Setup(repository => repository.Insert(model)).Verifiable();
            var modelService = new GroupService(_mockRepositoryGroup.Object, _mockRepositorySpecialization.Object);
            var modelController = new GroupController(modelService);

            // Act
            var result = modelController.Create(modelModify);

            // Assert
            var redirectToAction = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToAction.ActionName);
            _mockRepositoryGroup.Verify(r => r.Insert(It.IsAny<Group>()), Times.Once);
        }

        [Fact]
        public void CreateMethod_ReturnsViewResultWithModel_ForInvalidModel()
        {
            // Arrange
            var modelModify = new GroupModify { Name = null, SpecializationId = 1 };
            Mapper.Initialize(cfg => cfg.CreateMap<GroupModify, CreateGroupDto>());
            var modelDto = Mapper.Map<GroupModify, CreateGroupDto>(modelModify);
            Mapper.Initialize(cfg => cfg.CreateMap<CreateGroupDto, Group>());
            var model = Mapper.Map<CreateGroupDto, Group>(modelDto);
            _mockRepositoryGroup.Setup(repository => repository.Insert(model)).Verifiable();
            var modelService = new GroupService(_mockRepositoryGroup.Object, _mockRepositorySpecialization.Object);
            var modelController = new GroupController(modelService);
            modelController.ModelState.AddModelError("NameRequired", "Name is required.");

            // Act
            var result = modelController.Create(modelModify);

            // Assert
            Assert.IsType<ViewResult>(result);
            _mockRepositoryGroup.Verify(r => r.Insert(It.IsAny<Group>()), Times.Never);
        }

        [Fact]
        public void DeleteMethod_CallDeleteMethodRepository_RedirectToIndexMethod_ForCorrectArgument()
        {
            // Arrange
            const int deleteModelId = 1;
            var model = new Group { Id = deleteModelId, Name = "test1", SpecializationId = 1 };
            _mockRepositoryGroup.Setup(repository => repository.Delete(model)).Verifiable();
            var modelService = new GroupService(_mockRepositoryGroup.Object, _mockRepositorySpecialization.Object);
            var modelController = new GroupController(modelService);

            // Act
            var result = modelController.Delete(deleteModelId);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            _mockRepositoryGroup.Verify(r => r.Delete(It.IsAny<Group>()), Times.Once);
        }

        [Fact]
        public void DeleteMethod_RedirectToIndexMethod_ForInvalidArgument()
        {
            // Arrange
            int? deleteModelId = null;
            var model = new Group { Name = "test1", SpecializationId = 1 };
            _mockRepositoryGroup.Setup(repository => repository.Delete(model)).Verifiable();
            var modelService = new GroupService(_mockRepositoryGroup.Object, _mockRepositorySpecialization.Object);
            var modelController = new GroupController(modelService);

            // Act
            var result = modelController.Delete(deleteModelId);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            _mockRepositoryGroup.Verify(r => r.Delete(It.IsAny<Group>()), Times.Never);
        }

        [Fact]
        public void EditPostMethod_CallUpdateMethodRepository_RedirectToIndexMethod_ForCorrectModel()
        {
            // Arrange
            var modelModify = new GroupModify { Id = 1, Name = "test1", SpecializationId = 1 };
            Mapper.Initialize(cfg => cfg.CreateMap<GroupModify, EditGroupDto>());
            var modelDto = Mapper.Map<GroupModify, EditGroupDto>(modelModify);
            Mapper.Initialize(cfg => cfg.CreateMap<EditGroupDto, Group>());
            var model = Mapper.Map<EditGroupDto, Group>(modelDto);
            _mockRepositoryGroup.Setup(repository => repository.Update(model)).Verifiable();
            var modelService = new GroupService(_mockRepositoryGroup.Object, _mockRepositorySpecialization.Object);
            var modelController = new GroupController(modelService);

            // Act
            var result = modelController.Edit(modelModify);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            _mockRepositoryGroup.Verify(r => r.Update(It.IsAny<Group>()), Times.Once);
        }

        [Fact]
        public void EditPostMethod_ReturnsViewResultWithModel_ForInvalidModel()
        {
            // Arrange
            var modelModify = new GroupModify { Id = 1, Name = null, SpecializationId = 1 };
            Mapper.Initialize(cfg => cfg.CreateMap<GroupModify, EditGroupDto>());
            var modelDto = Mapper.Map<GroupModify, EditGroupDto>(modelModify);
            Mapper.Initialize(cfg => cfg.CreateMap<EditGroupDto, Group>());
            var model = Mapper.Map<EditGroupDto, Group>(modelDto);
            _mockRepositoryGroup.Setup(repository => repository.Update(model)).Verifiable();
            var modelService = new GroupService(_mockRepositoryGroup.Object, _mockRepositorySpecialization.Object);
            var modelController = new GroupController(modelService);
            modelController.ModelState.AddModelError("NameRequired", "Name is required.");

            // Act
            var result = modelController.Edit(modelModify);

            // Assert
            Assert.IsType<ViewResult>(result);
            _mockRepositoryGroup.Verify(r => r.Update(It.IsAny<Group>()), Times.Never);
        }

        [Fact]
        public void EditGetMethod_CallGetByIdMethodRepository_ReturnsViewResultWithModel_ForCorrectArgument()
        {
            // Arrange
            const int editModelId = 1;
            var modelModify = new GroupModify { Id = editModelId, Name = "test1", SpecializationId = 1 };
            Mapper.Initialize(cfg => cfg.CreateMap<GroupModify, EditGroupDto>());
            var modelDto = Mapper.Map<GroupModify, EditGroupDto>(modelModify);
            Mapper.Initialize(cfg => cfg.CreateMap<EditGroupDto, Group>());
            var model = Mapper.Map<EditGroupDto, Group>(modelDto);
            _mockRepositoryGroup.Setup(repository => repository.GetById(editModelId)).Returns(model).Verifiable();
            var modelService = new GroupService(_mockRepositoryGroup.Object, _mockRepositorySpecialization.Object);
            var modelController = new GroupController(modelService);

            // Act
            var result = modelController.Edit(editModelId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            modelModify.Should().BeEquivalentTo(viewResult.Model);
            _mockRepositoryGroup.Verify(r => r.GetById(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void EditGetMethod_RedirectToIndexMethod_ForInvalidArgument()
        {
            // Arrange
            int? editModelId = null;
            var modelService = new GroupService(_mockRepositoryGroup.Object, _mockRepositorySpecialization.Object);
            var modelController = new GroupController(modelService);

            // Act
            var result = modelController.Edit(editModelId);

            // Assert
            var redirectToAction = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToAction.ActionName);
        }

        [Fact]
        public void EditGetMethod_RedirectToIndexMethod_ForNotFoundedModel()
        {
            // Arrange
            const int editModelId = 1;
            Group model = default;
            _mockRepositoryGroup.Setup(repository => repository.GetById(editModelId)).Returns(model).Verifiable();
            var modelService = new GroupService(_mockRepositoryGroup.Object, _mockRepositorySpecialization.Object);
            var modelController = new GroupController(modelService);

            // Act
            var result = modelController.Edit(editModelId);

            // Assert
            var redirectToAction = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToAction.ActionName);
        }
    }
}
