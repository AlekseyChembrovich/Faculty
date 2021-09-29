using Moq;
using Xunit;
using AutoMapper;
using System.Linq;
using FluentAssertions;
using Faculty.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Faculty.AspUI.Controllers;
using System.Collections.Generic;
using Faculty.DataAccessLayer.Models;
using Faculty.BusinessLayer.Services;
using Faculty.AspUI.ViewModels.Curator;
using Faculty.BusinessLayer.Dto.Curator;

namespace Faculty.UnitTests
{
    public class CuratorControllerActionsTests
    {
        private readonly IMapper _mapper;

        public CuratorControllerActionsTests()
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            _mapper = new Mapper(mapperConfiguration);
        }

        [Fact]
        public void IndexMethod_ReturnsViewResult_WithListOfDisplayModelsDisplay()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Curator>>();
            mockRepository.Setup(repository => repository.GetAll()).Returns(GetTestModels()).Verifiable();
            var modelService = new CuratorService(mockRepository.Object);
            var modelController = new CuratorController(modelService, _mapper);

            // Act
            var result = modelController.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var models = Assert.IsAssignableFrom<IEnumerable<CuratorDisplayModify>>(viewResult.Model);
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
            var modelAdd = new CuratorAdd { Surname = "test1", Name = "test1", Doublename = "test1", Phone = "+375-29-557-06-11" };
            var modelDto = _mapper.Map<CuratorAdd, CuratorAddDto>(modelAdd);
            var model = _mapper.Map<CuratorAddDto, Curator>(modelDto);
            var mockRepository = new Mock<IRepository<Curator>>();
            mockRepository.Setup(repository => repository.Insert(model)).Verifiable();
            var modelService = new CuratorService(mockRepository.Object);
            var modelController = new CuratorController(modelService, _mapper);

            // Act
            var result = modelController.Create(modelAdd);

            // Assert
            var redirectToAction = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToAction.ActionName);
            mockRepository.Verify(r => r.Insert(It.IsAny<Curator>()), Times.Once);
        }

        [Fact]
        public void CreateMethod_ReturnsViewResultWithModel_ForInvalidModel()
        {
            // Arrange
            var modelAdd = new CuratorAdd { Surname = "test1", Name = null, Doublename = "test1", Phone = "+375-29-557-06-11" };
            var modelDto = _mapper.Map<CuratorAdd, CuratorAddDto>(modelAdd);
            var model = _mapper.Map<CuratorAddDto, Curator>(modelDto);
            var mockRepository = new Mock<IRepository<Curator>>();
            mockRepository.Setup(repository => repository.Insert(model)).Verifiable();
            var modelService = new CuratorService(mockRepository.Object);
            var modelController = new CuratorController(modelService, _mapper);
            modelController.ModelState.AddModelError("NameRequired", "Name is required.");

            // Act
            var result = modelController.Create(modelAdd);

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
            var modelController = new CuratorController(modelService, _mapper);

            // Act
            var result = modelController.Delete(deleteModelId);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mockRepository.Verify(r => r.Delete(It.IsAny<Curator>()), Times.Once);
        }

        [Fact]
        public void EditPostMethod_CallUpdateMethodRepository_RedirectToIndexMethod_ForCorrectModel()
        {
            // Arrange
            var modelModify = new CuratorDisplayModify { Id = 1, Surname = "test1", Name = "test1", Doublename = "test1", Phone = "+375-29-557-06-11" };
            var modelDto = _mapper.Map<CuratorDisplayModify, CuratorDisplayModifyDto>(modelModify);
            var model = _mapper.Map<CuratorDisplayModifyDto, Curator>(modelDto);
            var mockRepository = new Mock<IRepository<Curator>>();
            mockRepository.Setup(repository => repository.Update(model)).Verifiable();
            var modelService = new CuratorService(mockRepository.Object);
            var modelController = new CuratorController(modelService, _mapper);

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
            var modelModify = new CuratorDisplayModify { Id = 1, Surname = "test1", Name = null, Doublename = "test1", Phone = "+375-29-557-06-11" };
            var modelDto = _mapper.Map<CuratorDisplayModify, CuratorDisplayModifyDto>(modelModify);
            var model = _mapper.Map<CuratorDisplayModifyDto, Curator>(modelDto);
            var mockRepository = new Mock<IRepository<Curator>>();
            mockRepository.Setup(repository => repository.Update(model)).Verifiable();
            var modelService = new CuratorService(mockRepository.Object);
            var modelController = new CuratorController(modelService, _mapper);
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
            var modelModify = new CuratorDisplayModify { Id = editModelId, Surname = "test1", Name = "test1", Doublename = "test1", Phone = "+375-29-557-06-11" };
            var modelDto = _mapper.Map<CuratorDisplayModify, CuratorDisplayModifyDto>(modelModify);
            var model = _mapper.Map<CuratorDisplayModifyDto, Curator>(modelDto);
            var mockRepository = new Mock<IRepository<Curator>>();
            mockRepository.Setup(repository => repository.GetById(editModelId)).Returns(model).Verifiable();
            var modelService = new CuratorService(mockRepository.Object);
            var modelController = new CuratorController(modelService, _mapper);

            // Act
            var result = modelController.Edit(editModelId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            modelModify.Should().BeEquivalentTo(viewResult.Model);
            mockRepository.Verify(r => r.GetById(It.IsAny<int>()), Times.Once);
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
            var modelController = new CuratorController(modelService, _mapper);

            // Act
            var result = modelController.Edit(editModelId);

            // Assert
            var redirectToAction = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToAction.ActionName);
        }
    }
}
