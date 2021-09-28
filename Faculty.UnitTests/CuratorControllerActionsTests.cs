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
using Faculty.BusinessLayer.ModelsDto.CuratorDto;

namespace Faculty.UnitTests
{
    public class CuratorControllerActionsTests
    {
        [Fact]
        public void IndexMethod_ReturnsViewResult_WithListOfDisplayModelsDisplay()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Curator>>();
            mockRepository.Setup(repository => repository.GetAll()).Returns(GetTestModels()).Verifiable();
            var modelService = new CuratorService(mockRepository.Object);
            var modelController = new CuratorController(modelService);

            // Act
            var result = modelController.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var models = Assert.IsAssignableFrom<IEnumerable<CuratorDisplay>>(viewResult.Model);
            Assert.Equal(3, models.Count());
            mockRepository.Verify(r => r.GetAll());
        }

        private static IEnumerable<Curator> GetTestModels()
        {
            var models = new List<Curator>()
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

            return models;
        }

        [Fact]
        public void CreateMethod_CallInsertMethodRepository_RedirectToIndexMethodWith_ForCorrectModel()
        {
            // Arrange
            var modelModify = new CuratorModify { Surname = "test1", Name = "test1", Doublename = "test1", Phone = "+375-29-557-06-11" };
            Mapper.Initialize(cfg => cfg.CreateMap<CuratorModify, CreateCuratorDto>());
            var modelDto = Mapper.Map<CuratorModify, CreateCuratorDto>(modelModify);
            Mapper.Initialize(cfg => cfg.CreateMap<CreateCuratorDto, Curator>());
            var model = Mapper.Map<CreateCuratorDto, Curator>(modelDto);
            var mockRepository = new Mock<IRepository<Curator>>();
            mockRepository.Setup(repository => repository.Insert(model)).Verifiable();
            var modelService = new CuratorService(mockRepository.Object);
            var modelController = new CuratorController(modelService);

            // Act
            var result = modelController.Create(modelModify);

            // Assert
            var redirectToAction = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToAction.ActionName);
            mockRepository.Verify(r => r.Insert(It.IsAny<Curator>()), Times.Once);
        }

        [Fact]
        public void CreateMethod_ReturnsViewResultWithModel_ForInvalidModel()
        {
            // Arrange
            var modelModify = new CuratorModify { Surname = "test1", Name = null, Doublename = "test1", Phone = "+375-29-557-06-11" };
            Mapper.Initialize(cfg => cfg.CreateMap<CuratorModify, CreateCuratorDto>());
            var modelDto = Mapper.Map<CuratorModify, CreateCuratorDto>(modelModify);
            Mapper.Initialize(cfg => cfg.CreateMap<CreateCuratorDto, Curator>());
            var model = Mapper.Map<CreateCuratorDto, Curator>(modelDto);
            var mockRepository = new Mock<IRepository<Curator>>();
            mockRepository.Setup(repository => repository.Insert(model)).Verifiable();
            var modelService = new CuratorService(mockRepository.Object);
            var modelController = new CuratorController(modelService);
            modelController.ModelState.AddModelError("NameRequired", "Name is required.");

            // Act
            var result = modelController.Create(modelModify);

            // Assert
            Assert.IsType<ViewResult>(result);
            mockRepository.Verify(r => r.Insert(It.IsAny<Curator>()), Times.Never);
        }

        [Fact]
        public void DeleteMethod_CallDeleteMethodRepository_RedirectToIndexMethod_ForCorrectArgument()
        {
            // Arrange
            const int deleteModelId = 1;
            var model = new Curator { Id = deleteModelId, Surname = "test1", Name = "test1", Doublename = "test1", Phone = "+375-29-557-06-11" };
            var mockRepository = new Mock<IRepository<Curator>>();
            mockRepository.Setup(repository => repository.Delete(model)).Verifiable();
            var modelService = new CuratorService(mockRepository.Object);
            var modelController = new CuratorController(modelService);

            // Act
            var result = modelController.Delete(deleteModelId);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mockRepository.Verify(r => r.Delete(It.IsAny<Curator>()), Times.Once);
        }

        [Fact]
        public void DeleteMethod_RedirectToIndexMethod_ForInvalidArgument()
        {
            // Arrange
            int? deleteModelId = null;
            var model = new Curator { Surname = "test1", Name = "test1", Doublename = "test1", Phone = "+375-29-557-06-11" };
            var mockRepository = new Mock<IRepository<Curator>>();
            mockRepository.Setup(repository => repository.Delete(model)).Verifiable();
            var modelService = new CuratorService(mockRepository.Object);
            var modelController = new CuratorController(modelService);

            // Act
            var result = modelController.Delete(deleteModelId);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mockRepository.Verify(r => r.Delete(It.IsAny<Curator>()), Times.Never);
        }

        [Fact]
        public void EditPostMethod_CallUpdateMethodRepository_RedirectToIndexMethod_ForCorrectModel()
        {
            // Arrange
            var modelModify = new CuratorModify { Id = 1, Surname = "test1", Name = "test1", Doublename = "test1", Phone = "+375-29-557-06-11" };
            Mapper.Initialize(cfg => cfg.CreateMap<CuratorModify, EditCuratorDto>());
            var modelDto = Mapper.Map<CuratorModify, EditCuratorDto>(modelModify);
            Mapper.Initialize(cfg => cfg.CreateMap<EditCuratorDto, Curator>());
            var model = Mapper.Map<EditCuratorDto, Curator>(modelDto);
            var mockRepository = new Mock<IRepository<Curator>>();
            mockRepository.Setup(repository => repository.Update(model)).Verifiable();
            var modelService = new CuratorService(mockRepository.Object);
            var modelController = new CuratorController(modelService);

            // Act
            var result = modelController.Edit(modelModify);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mockRepository.Verify(r => r.Update(It.IsAny<Curator>()), Times.Once);
        }

        [Fact]
        public void EditPostMethod_ReturnsViewResultWithModel_ForInvalidModel()
        {
            // Arrange
            var modelModify = new CuratorModify { Id = 1, Surname = "test1", Name = null, Doublename = "test1", Phone = "+375-29-557-06-11" };
            Mapper.Initialize(cfg => cfg.CreateMap<CuratorModify, EditCuratorDto>());
            var modelDto = Mapper.Map<CuratorModify, EditCuratorDto>(modelModify);
            Mapper.Initialize(cfg => cfg.CreateMap<EditCuratorDto, Curator>());
            var model = Mapper.Map<EditCuratorDto, Curator>(modelDto);
            var mockRepository = new Mock<IRepository<Curator>>();
            mockRepository.Setup(repository => repository.Update(model)).Verifiable();
            var modelService = new CuratorService(mockRepository.Object);
            var modelController = new CuratorController(modelService);
            modelController.ModelState.AddModelError("NameRequired", "Name is required.");

            // Act
            var result = modelController.Edit(modelModify);

            // Assert
            Assert.IsType<ViewResult>(result);
            mockRepository.Verify(r => r.Update(It.IsAny<Curator>()), Times.Never);
        }

        [Fact]
        public void EditGetMethod_CallGetByIdMethodRepository_ReturnsViewResultWithModel_ForCorrectArgument()
        {
            // Arrange
            const int editModelId = 1;
            var modelModify = new CuratorModify { Id = editModelId, Surname = "test1", Name = "test1", Doublename = "test1", Phone = "+375-29-557-06-11" };
            Mapper.Initialize(cfg => cfg.CreateMap<CuratorModify, EditCuratorDto>());
            var modelDto = Mapper.Map<CuratorModify, EditCuratorDto>(modelModify);
            Mapper.Initialize(cfg => cfg.CreateMap<EditCuratorDto, Curator>());
            var model = Mapper.Map<EditCuratorDto, Curator>(modelDto);
            var mockRepository = new Mock<IRepository<Curator>>();
            mockRepository.Setup(repository => repository.GetById(editModelId)).Returns(model).Verifiable();
            var modelService = new CuratorService(mockRepository.Object);
            var modelController = new CuratorController(modelService);

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
            var mockRepository = new Mock<IRepository<Curator>>();
            var modelService = new CuratorService(mockRepository.Object);
            var modelController = new CuratorController(modelService);

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
            Curator model = default;
            var mockRepository = new Mock<IRepository<Curator>>();
            mockRepository.Setup(repository => repository.GetById(editModelId)).Returns(model).Verifiable();
            var modelService = new CuratorService(mockRepository.Object);
            var modelController = new CuratorController(modelService);

            // Act
            var result = modelController.Edit(editModelId);

            // Assert
            var redirectToAction = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToAction.ActionName);
        }
    }
}
