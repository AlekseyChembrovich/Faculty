using Moq;
using Xunit;
using AutoMapper;
using System.Linq;
using FluentAssertions;
using Faculty.AspUI.Tools;
using Microsoft.AspNetCore.Mvc;
using Faculty.AspUI.Controllers;
using System.Collections.Generic;
using Faculty.BusinessLayer.Services;
using Faculty.DataAccessLayer.Models;
using Faculty.DataAccessLayer.Repository;
using Faculty.AspUI.ViewModels.Specialization;
using Faculty.BusinessLayer.Dto.Specialization;

namespace Faculty.UnitTests.AspUI
{
    public class SpecializationControllerActionsTests
    {
        private readonly Mock<IRepository<Specialization>> _mockRepositorySpecialization;
        private readonly IMapper _mapper;

        public SpecializationControllerActionsTests()
        {
            _mockRepositorySpecialization = new Mock<IRepository<Specialization>>();
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            _mapper = new Mapper(mapperConfiguration);
        }

        [Fact]
        public void IndexMethod_ReturnsAViewResult_WithAListOfModelDisplay()
        {
            // Arrange
            _mockRepositorySpecialization.Setup(repository => repository.GetAll()).Returns(GetTestModels()).Verifiable();
            var modelService = new SpecializationService(_mockRepositorySpecialization.Object, _mapper);
            var modelController = new SpecializationController(modelService, _mapper);

            // Act
            var result = modelController.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var models = Assert.IsAssignableFrom<IEnumerable<SpecializationDisplayModify>>(viewResult.ViewData.Model);
            Assert.Equal(3, models.Count());
            _mockRepositorySpecialization.Verify(r => r.GetAll());
        }

        private static IEnumerable<Specialization> GetTestModels()
        {
            var models = new List<Specialization>()
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

            return models;
        }

        [Fact]
        public void CreateMethod_CallInsertMethodRepository_ReturnsRedirectToIndexAction_WhenCorrectModel()
        {
            // Arrange
            var modelAdd = new SpecializationAdd { Name = "test1" };
            var modelDto = _mapper.Map<SpecializationAdd, SpecializationAddDto>(modelAdd);
            var model = _mapper.Map<SpecializationAddDto, Specialization>(modelDto);
            _mockRepositorySpecialization.Setup(repository => repository.Insert(model)).Verifiable();
            var modelService = new SpecializationService(_mockRepositorySpecialization.Object, _mapper);
            var modelController = new SpecializationController(modelService, _mapper);

            // Act
            var result = modelController.Create(modelAdd);

            // Assert
            var redirectToAction = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToAction.ActionName);
            _mockRepositorySpecialization.Verify(r => r.Insert(It.IsAny<Specialization>()), Times.Once);
        }

        [Fact]
        public void DeleteGetMethod_CallDeleteMethodRepository_RedirectToIndexAction_WhenCorrectArgument()
        {
            // Arrange
            const int deleteModelId = 1;
            var model = new Specialization { Id = deleteModelId, Name = "test1" };
            _mockRepositorySpecialization.Setup(repository => repository.GetById(deleteModelId)).Returns(model).Verifiable();
            var modelService = new SpecializationService(_mockRepositorySpecialization.Object, _mapper);
            var modelController = new SpecializationController(modelService, _mapper);

            // Act
            var result = modelController.Delete(deleteModelId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            model.Should().BeEquivalentTo(viewResult.Model);
            _mockRepositorySpecialization.Verify(r => r.GetById(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void DeletePostMethod_CallDeleteMethodRepository_RedirectToIndexAction_WhenCorrectArgument()
        {
            // Arrange
            var modelModify = new SpecializationDisplayModify { Id = 1, Name = "test1" };
            var modelDto = _mapper.Map<SpecializationDisplayModify, SpecializationDisplayModifyDto>(modelModify);
            var model = _mapper.Map<SpecializationDisplayModifyDto, Specialization>(modelDto);
            _mockRepositorySpecialization.Setup(repository => repository.Delete(model)).Verifiable();
            var modelService = new SpecializationService(_mockRepositorySpecialization.Object, _mapper);
            var modelController = new SpecializationController(modelService, _mapper);

            // Act
            var result = modelController.Delete(modelModify);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            _mockRepositorySpecialization.Verify(r => r.Delete(It.IsAny<Specialization>()), Times.Once);
        }

        [Fact]
        public void EditPostMethod_CallUpdateMethodRepository_RedirectToIndexAction_WhenCorrectModel()
        {
            // Arrange
            var modelModify = new SpecializationDisplayModify { Id = 1, Name = "test1" };
            var modelDto = _mapper.Map<SpecializationDisplayModify, SpecializationDisplayModifyDto>(modelModify);
            var model = _mapper.Map<SpecializationDisplayModifyDto, Specialization>(modelDto);
            _mockRepositorySpecialization.Setup(repository => repository.Update(model)).Verifiable();
            var modelService = new SpecializationService(_mockRepositorySpecialization.Object, _mapper);
            var modelController = new SpecializationController(modelService, _mapper);

            // Act
            var result = modelController.Edit(modelModify);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            _mockRepositorySpecialization.Verify(r => r.Update(It.IsAny<Specialization>()), Times.Once);
        }

        [Fact]
        public void EditGetMethod_CallGetByIdMethodRepository_ReturnsViewResultWithModel_WhenCorrectArgument()
        {
            // Arrange
            const int editModelId = 1;
            var modelModify = new SpecializationDisplayModify { Id = editModelId, Name = "test1" };
            var modelDto = _mapper.Map<SpecializationDisplayModify, SpecializationDisplayModifyDto>(modelModify);
            var model = _mapper.Map<SpecializationDisplayModifyDto, Specialization>(modelDto);
            _mockRepositorySpecialization.Setup(repository => repository.GetById(editModelId)).Returns(model).Verifiable();
            var modelService = new SpecializationService(_mockRepositorySpecialization.Object, _mapper);
            var modelController = new SpecializationController(modelService, _mapper);

            // Act
            var result = modelController.Edit(editModelId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            modelModify.Should().BeEquivalentTo(viewResult.Model);
            _mockRepositorySpecialization.Verify(r => r.GetById(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void EditGetMethod_ReturnsRedirectToIndexAction_WhenNotFoundedModel()
        {
            // Arrange
            const int editModelId = 1;
            Specialization model = default;
            _mockRepositorySpecialization.Setup(repository => repository.GetById(editModelId)).Returns(model).Verifiable();
            var modelService = new SpecializationService(_mockRepositorySpecialization.Object, _mapper);
            var modelController = new SpecializationController(modelService, _mapper);

            // Act
            var result = modelController.Edit(editModelId);

            // Assert
            var redirectToAction = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToAction.ActionName);
        }
    }
}
