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
using Faculty.BusinessLayer.ModelsDto.SpecializationDto;

namespace Faculty.UnitTests
{
    public class SpecializationControllerActionsTests
    {
        [Fact]
        public void IndexMethod_ReturnsViewResult_WithListOfDisplayModelsDisplay()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Specialization>>();
            mockRepository.Setup(repository => repository.GetAll()).Returns(GetTestModels()).Verifiable();
            var modelService = new SpecializationService(mockRepository.Object);
            var modelController = new SpecializationController(modelService);

            // Act
            var result = modelController.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var models = Assert.IsAssignableFrom<IEnumerable<SpecializationDisplay>>(viewResult.ViewData.Model);
            Assert.Equal(3, models.Count());
            mockRepository.Verify(r => r.GetAll());
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
        public void CreateMethod_CallInsertMethodRepository_RedirectToIndexMethodWith_ForCorrectModel()
        {
            // Arrange
            var modelModify = new SpecializationModify { Name = "test1" };
            Mapper.Initialize(cfg => cfg.CreateMap<SpecializationModify, CreateSpecializationDto>());
            var modelDto = Mapper.Map<SpecializationModify, CreateSpecializationDto>(modelModify);
            Mapper.Initialize(cfg => cfg.CreateMap<CreateSpecializationDto, Specialization>());
            var model = Mapper.Map<CreateSpecializationDto, Specialization>(modelDto);
            var mockRepository = new Mock<IRepository<Specialization>>();
            mockRepository.Setup(repository => repository.Insert(model)).Verifiable();
            var modelService = new SpecializationService(mockRepository.Object);
            var modelController = new SpecializationController(modelService);

            // Act
            var result = modelController.Create(modelModify);

            // Assert
            var redirectToAction = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToAction.ActionName);
            mockRepository.Verify(r => r.Insert(It.IsAny<Specialization>()), Times.Once);
        }

        [Fact]
        public void CreateMethod_ReturnsViewResultWithModel_ForInvalidModel()
        {
            // Arrange
            var modelModify = new SpecializationModify { Name = null };
            Mapper.Initialize(cfg => cfg.CreateMap<SpecializationModify, CreateSpecializationDto>());
            var modelDto = Mapper.Map<SpecializationModify, CreateSpecializationDto>(modelModify);
            Mapper.Initialize(cfg => cfg.CreateMap<CreateSpecializationDto, Specialization>());
            var model = Mapper.Map<CreateSpecializationDto, Specialization>(modelDto);
            var mockRepository = new Mock<IRepository<Specialization>>();
            mockRepository.Setup(repository => repository.Insert(model)).Verifiable();
            var modelService = new SpecializationService(mockRepository.Object);
            var modelController = new SpecializationController(modelService);
            modelController.ModelState.AddModelError("NameRequired", "Name is required.");

            // Act
            var result = modelController.Create(modelModify);

            // Assert
            Assert.IsType<ViewResult>(result);
            mockRepository.Verify(r => r.Insert(It.IsAny<Specialization>()), Times.Never);
        }

        [Fact]
        public void DeleteMethod_CallDeleteMethodRepository_RedirectToIndexMethod_ForCorrectArgument()
        {
            // Arrange
            const int deleteModelId = 1;
            var model = new Specialization { Id = deleteModelId, Name = "test1" };
            var mockRepository = new Mock<IRepository<Specialization>>();
            mockRepository.Setup(repository => repository.Delete(model)).Verifiable();
            var modelService = new SpecializationService(mockRepository.Object);
            var modelController = new SpecializationController(modelService);

            // Act
            var result = modelController.Delete(deleteModelId);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mockRepository.Verify(r => r.Delete(It.IsAny<Specialization>()), Times.Once);
        }

        [Fact]
        public void DeleteMethod_RedirectToIndexMethod_ForInvalidArgument()
        {
            // Arrange
            int? deleteModelId = null;
            var model = new Specialization { Name = "test1" };
            var mockRepository = new Mock<IRepository<Specialization>>();
            mockRepository.Setup(repository => repository.Delete(model)).Verifiable();
            var modelService = new SpecializationService(mockRepository.Object);
            var modelController = new SpecializationController(modelService);

            // Act
            var result = modelController.Delete(deleteModelId);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mockRepository.Verify(r => r.Delete(It.IsAny<Specialization>()), Times.Never);
        }

        [Fact]
        public void EditPostMethod_CallUpdateMethodRepository_RedirectToIndexMethod_ForCorrectModel()
        {
            // Arrange
            var modelModify = new SpecializationModify { Id = 1, Name = "test1" };
            Mapper.Initialize(cfg => cfg.CreateMap<SpecializationModify, EditSpecializationDto>());
            var modelDto = Mapper.Map<SpecializationModify, EditSpecializationDto>(modelModify);
            Mapper.Initialize(cfg => cfg.CreateMap<EditSpecializationDto, Specialization>());
            var model = Mapper.Map<EditSpecializationDto, Specialization>(modelDto);
            var mockRepository = new Mock<IRepository<Specialization>>();
            mockRepository.Setup(repository => repository.Update(model)).Verifiable();
            var modelService = new SpecializationService(mockRepository.Object);
            var modelController = new SpecializationController(modelService);

            // Act
            var result = modelController.Edit(modelModify);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mockRepository.Verify(r => r.Update(It.IsAny<Specialization>()), Times.Once);
        }

        [Fact]
        public void EditPostMethod_ReturnsViewResultWithModel_ForInvalidModel()
        {
            // Arrange
            var modelModify = new SpecializationModify { Id = 1,  Name = null };
            Mapper.Initialize(cfg => cfg.CreateMap<SpecializationModify, EditSpecializationDto>());
            var modelDto = Mapper.Map<SpecializationModify, EditSpecializationDto>(modelModify);
            Mapper.Initialize(cfg => cfg.CreateMap<EditSpecializationDto, Specialization>());
            var model = Mapper.Map<EditSpecializationDto, Specialization>(modelDto);
            var mockRepository = new Mock<IRepository<Specialization>>();
            mockRepository.Setup(repository => repository.Update(model)).Verifiable();
            var modelService = new SpecializationService(mockRepository.Object);
            var modelController = new SpecializationController(modelService);
            modelController.ModelState.AddModelError("NameRequired", "Name is required.");

            // Act
            var result = modelController.Edit(modelModify);

            // Assert
            Assert.IsType<ViewResult>(result);
            mockRepository.Verify(r => r.Update(It.IsAny<Specialization>()), Times.Never);
        }

        [Fact]
        public void EditGetMethod_CallGetByIdMethodRepository_ReturnsViewResultWithModel_ForCorrectArgument()
        {
            // Arrange
            const int editModelId = 1;
            var modelModify = new SpecializationModify { Id = editModelId, Name = "test1" };
            Mapper.Initialize(cfg => cfg.CreateMap<SpecializationModify, EditSpecializationDto>());
            var modelDto = Mapper.Map<SpecializationModify, EditSpecializationDto>(modelModify);
            Mapper.Initialize(cfg => cfg.CreateMap<EditSpecializationDto, Specialization>());
            var model = Mapper.Map<EditSpecializationDto, Specialization>(modelDto);
            var mockRepository = new Mock<IRepository<Specialization>>();
            mockRepository.Setup(repository => repository.GetById(editModelId)).Returns(model).Verifiable();
            var modelService = new SpecializationService(mockRepository.Object);
            var modelController = new SpecializationController(modelService);

            // Act
            var result = modelController.Edit(editModelId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            modelModify.Should().BeEquivalentTo(viewResult.Model);
            mockRepository.Verify(r => r.GetById(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void EditGetMethod_RedirectToIndexMethod_ForInvalidArgument()
        {
            // Arrange
            int? editModelId = null;
            var mockRepository = new Mock<IRepository<Specialization>>();
            var modelService = new SpecializationService(mockRepository.Object);
            var modelController = new SpecializationController(modelService);

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
            Specialization model = default;
            var mockRepository = new Mock<IRepository<Specialization>>();
            mockRepository.Setup(repository => repository.GetById(editModelId)).Returns(model).Verifiable();
            var modelService = new SpecializationService(mockRepository.Object);
            var modelController = new SpecializationController(modelService);

            // Act
            var result = modelController.Edit(editModelId);

            // Assert
            var redirectToAction = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToAction.ActionName);
        }
    }
}
