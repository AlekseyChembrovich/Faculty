using Moq;
using Xunit;
using AutoMapper;
using System.Linq;
using Faculty.AspUI;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Faculty.AspUI.Controllers;
using System.Collections.Generic;
using Faculty.DataAccessLayer.Models;
using Faculty.BusinessLayer.Services;
using Faculty.AspUI.ViewModels.Curator;
using Faculty.BusinessLayer.Dto.Curator;
using Faculty.DataAccessLayer.Repository;

namespace Faculty.UnitTests
{
    public class CuratorControllerActionsTests
    {
        private readonly Mock<IRepository<Curator>> _mockRepositoryCurator;
        private readonly IMapper _mapper;

        public CuratorControllerActionsTests()
        {
            _mockRepositoryCurator = new Mock<IRepository<Curator>>();
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            _mapper = new Mapper(mapperConfiguration);
        }

        [Fact]
        public void IndexMethod_ReturnsViewResult_WithListOfDisplayModelsDisplay()
        {
            // Arrange
            _mockRepositoryCurator.Setup(repository => repository.GetAll()).Returns(GetTestModels()).Verifiable();
            var modelService = new CuratorService(_mockRepositoryCurator.Object, _mapper);
            var modelController = new CuratorController(modelService, _mapper);

            // Act
            var result = modelController.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var models = Assert.IsAssignableFrom<IEnumerable<CuratorDisplayModify>>(viewResult.Model);
            Assert.Equal(3, models.Count());
            _mockRepositoryCurator.Verify(r => r.GetAll());
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
            _mockRepositoryCurator.Setup(repository => repository.Insert(model)).Verifiable();
            var modelService = new CuratorService(_mockRepositoryCurator.Object, _mapper);
            var modelController = new CuratorController(modelService, _mapper);

            // Act
            var result = modelController.Create(modelAdd);

            // Assert
            var redirectToAction = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToAction.ActionName);
            _mockRepositoryCurator.Verify(r => r.Insert(It.IsAny<Curator>()), Times.Once);
        }

        [Fact]
        public void DeleteGetMethod_CallDeleteMethodRepository_RedirectToIndexMethod_ForCorrectArgument()
        {
            // Arrange
            const int deleteModelId = 1;
            var model = new Curator { Id = deleteModelId, Surname = "test1", Name = "test1", Doublename = "test1", Phone = "+375-29-557-06-11" };
            _mockRepositoryCurator.Setup(repository => repository.GetById(deleteModelId)).Returns(model).Verifiable();
            var modelService = new CuratorService(_mockRepositoryCurator.Object, _mapper);
            var modelController = new CuratorController(modelService, _mapper);

            // Act
            var result = modelController.Delete(deleteModelId);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            model.Should().BeEquivalentTo(viewResult.Model);
            _mockRepositoryCurator.Verify(r => r.GetById(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void DeletePostMethod_CallDeleteMethodRepository_RedirectToIndexMethod_ForCorrectArgument()
        {
            // Arrange
            var modelModify = new CuratorDisplayModify { Id = 1, Surname = "test1", Name = "test1", Doublename = "test1", Phone = "+375-29-557-06-11" };
            var modelDto = _mapper.Map<CuratorDisplayModify, CuratorDisplayModifyDto>(modelModify);
            var model = _mapper.Map<CuratorDisplayModifyDto, Curator>(modelDto);
            _mockRepositoryCurator.Setup(repository => repository.Delete(model)).Verifiable();
            var modelService = new CuratorService(_mockRepositoryCurator.Object, _mapper);
            var modelController = new CuratorController(modelService, _mapper);

            // Act
            var result = modelController.Delete(modelModify);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            _mockRepositoryCurator.Verify(r => r.Delete(It.IsAny<Curator>()), Times.Once);
        }

        [Fact]
        public void EditPostMethod_CallUpdateMethodRepository_RedirectToIndexMethod_ForCorrectModel()
        {
            // Arrange
            var modelModify = new CuratorDisplayModify { Id = 1, Surname = "test1", Name = "test1", Doublename = "test1", Phone = "+375-29-557-06-11" };
            var modelDto = _mapper.Map<CuratorDisplayModify, CuratorDisplayModifyDto>(modelModify);
            var model = _mapper.Map<CuratorDisplayModifyDto, Curator>(modelDto);
            _mockRepositoryCurator.Setup(repository => repository.Update(model)).Verifiable();
            var modelService = new CuratorService(_mockRepositoryCurator.Object, _mapper);
            var modelController = new CuratorController(modelService, _mapper);

            // Act
            var result = modelController.Edit(modelModify);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            _mockRepositoryCurator.Verify(r => r.Update(It.IsAny<Curator>()), Times.Once);
        }

        [Fact]
        public void EditGetMethod_CallGetByIdMethodRepository_ReturnsViewResultWithModel_ForCorrectArgument()
        {
            // Arrange
            const int editModelId = 1;
            var modelModify = new CuratorDisplayModify { Id = editModelId, Surname = "test1", Name = "test1", Doublename = "test1", Phone = "+375-29-557-06-11" };
            var modelDto = _mapper.Map<CuratorDisplayModify, CuratorDisplayModifyDto>(modelModify);
            var model = _mapper.Map<CuratorDisplayModifyDto, Curator>(modelDto);
            _mockRepositoryCurator.Setup(repository => repository.GetById(editModelId)).Returns(model).Verifiable();
            var modelService = new CuratorService(_mockRepositoryCurator.Object, _mapper);
            var modelController = new CuratorController(modelService, _mapper);

            // Act
            var result = modelController.Edit(editModelId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            modelModify.Should().BeEquivalentTo(viewResult.Model);
            _mockRepositoryCurator.Verify(r => r.GetById(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void EditGetMethod_RedirectToIndexMethod_ForNotFoundedModel()
        {
            // Arrange
            const int editModelId = 1;
            Curator model = default;
            _mockRepositoryCurator.Setup(repository => repository.GetById(editModelId)).Returns(model).Verifiable();
            var modelService = new CuratorService(_mockRepositoryCurator.Object, _mapper);
            var modelController = new CuratorController(modelService, _mapper);

            // Act
            var result = modelController.Edit(editModelId);

            // Assert
            var redirectToAction = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToAction.ActionName);
        }
    }
}
