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
using Faculty.AspUI.ViewModels.Student;
using Faculty.BusinessLayer.Dto.Student;

namespace Faculty.UnitTests
{
    public class StudentControllerActionsTests
    {
        private readonly IMapper _mapper;

        public StudentControllerActionsTests()
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            _mapper = new Mapper(mapperConfiguration);
        }

        [Fact]
        public void IndexMethod_ReturnsViewResult_WithListOfDisplayModelsDisplay()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Student>>();
            mockRepository.Setup(repository => repository.GetAll()).Returns(GetTestModels()).Verifiable();
            var modelService = new StudentService(mockRepository.Object);
            var modelController = new StudentController(modelService, _mapper);

            // Act
            var result = modelController.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var models = Assert.IsAssignableFrom<IEnumerable<StudentDisplayModify>>(viewResult.ViewData.Model);
            Assert.Equal(3, models.Count());
            mockRepository.Verify(r => r.GetAll());
        }

        private static IEnumerable<Student> GetTestModels()
        {
            var models = new List<Student>()
            {
                new ()
                {
                    Id = 1,
                    Surname = "test1",
                    Name = "test1",
                    Doublename = "test1"
                },
                new ()
                {
                    Id = 2,
                    Surname = "test2",
                    Name = "test2",
                    Doublename = "test2"
                },
                new ()
                {
                    Id = 3,
                    Surname = "test3",
                    Name = "test3",
                    Doublename = "test3"
                }
            };

            return models;
        }

        [Fact]
        public void CreateMethod_CallInsertMethodRepository_RedirectToIndexMethodWith_ForCorrectModel()
        {
            // Arrange
            var modelAdd = new StudentAdd { Surname = "test1", Name = "test1", Doublename = "test1" };
            var modelDto = _mapper.Map<StudentAdd, StudentAddDto>(modelAdd);
            var model = _mapper.Map<StudentAddDto, Student>(modelDto);
            var mockRepository = new Mock<IRepository<Student>>();
            mockRepository.Setup(repository => repository.Insert(model)).Verifiable();
            var modelService = new StudentService(mockRepository.Object);
            var modelController = new StudentController(modelService, _mapper);

            // Act
            var result = modelController.Create(modelAdd);

            // Assert
            var redirectToAction = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToAction.ActionName);
            mockRepository.Verify(r => r.Insert(It.IsAny<Student>()), Times.Once);
        }

        [Fact]
        public void CreateMethod_ReturnsViewResultWithModel_ForInvalidModel()
        {
            // Arrange
            var modelAdd = new StudentAdd { Surname = "test1", Name = null, Doublename = "test1" };
            var modelDto = _mapper.Map<StudentAdd, StudentAddDto>(modelAdd);
            var model = _mapper.Map<StudentAddDto, Student>(modelDto);
            var mockRepository = new Mock<IRepository<Student>>();
            mockRepository.Setup(repository => repository.Insert(model)).Verifiable();
            var modelService = new StudentService(mockRepository.Object);
            var modelController = new StudentController(modelService, _mapper);
            modelController.ModelState.AddModelError("NameRequired", "Name is required.");

            // Act
            var result = modelController.Create(modelAdd);

            // Assert
            Assert.IsType<ViewResult>(result);
            mockRepository.Verify(r => r.Insert(It.IsAny<Student>()), Times.Never);
        }

        [Fact]
        public void DeleteMethod_CallDeleteMethodRepository_RedirectToIndexMethod_ForCorrectArgument()
        {
            // Arrange
            const int deleteModelId = 1;
            var model = new Student { Id = deleteModelId, Surname = "test1", Name = "test1", Doublename = "test1" };
            var mockRepository = new Mock<IRepository<Student>>();
            mockRepository.Setup(repository => repository.Delete(model)).Verifiable();
            var modelService = new StudentService(mockRepository.Object);
            var modelController = new StudentController(modelService, _mapper);

            // Act
            var result = modelController.Delete(deleteModelId);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mockRepository.Verify(r => r.Delete(It.IsAny<Student>()), Times.Once);
        }

        [Fact]
        public void EditPostMethod_CallUpdateMethodRepository_RedirectToIndexMethod_ForCorrectModel()
        {
            // Arrange
            var modelModify = new StudentDisplayModify { Id = 1, Surname = "test1", Name = "test1", Doublename = "test1" };
            var modelDto = _mapper.Map<StudentDisplayModify, StudentDisplayModifyDto>(modelModify);
            var model = _mapper.Map<StudentDisplayModifyDto, Student>(modelDto);
            var mockRepository = new Mock<IRepository<Student>>();
            mockRepository.Setup(repository => repository.Update(model)).Verifiable();
            var modelService = new StudentService(mockRepository.Object);
            var modelController = new StudentController(modelService, _mapper);

            // Act
            var result = modelController.Edit(modelModify);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mockRepository.Verify(r => r.Update(It.IsAny<Student>()), Times.Once);
        }

        [Fact]
        public void EditPostMethod_ReturnsViewResultWithModel_ForInvalidModel()
        {
            // Arrange
            var modelModify = new StudentDisplayModify { Id = 1, Surname = "test1", Name = null, Doublename = "test1" };
            var modelDto = _mapper.Map<StudentDisplayModify, StudentDisplayModifyDto>(modelModify);
            var model = _mapper.Map<StudentDisplayModifyDto, Student>(modelDto);
            var mockRepository = new Mock<IRepository<Student>>();
            mockRepository.Setup(repository => repository.Update(model)).Verifiable();
            var modelService = new StudentService(mockRepository.Object);
            var modelController = new StudentController(modelService, _mapper);
            modelController.ModelState.AddModelError("NameRequired", "Name is required.");

            // Act
            var result = modelController.Edit(modelModify);

            // Assert
            Assert.IsType<ViewResult>(result);
            mockRepository.Verify(r => r.Update(It.IsAny<Student>()), Times.Never);
        }

        [Fact]
        public void EditGetMethod_CallGetByIdMethodRepository_ReturnsViewResultWithModel_ForCorrectArgument()
        {
            // Arrange
            const int editModelId = 1;
            var modelModify = new StudentDisplayModify { Id = editModelId, Surname = "test1", Name = "test1", Doublename = "test1" };
            var modelDto = _mapper.Map<StudentDisplayModify, StudentDisplayModifyDto>(modelModify);
            var model = _mapper.Map<StudentDisplayModifyDto, Student>(modelDto);
            var mockRepository = new Mock<IRepository<Student>>();
            mockRepository.Setup(repository => repository.GetById(editModelId)).Returns(model).Verifiable();
            var modelService = new StudentService(mockRepository.Object);
            var modelController = new StudentController(modelService, _mapper);

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
            Student model = default;
            var mockRepository = new Mock<IRepository<Student>>();
            mockRepository.Setup(repository => repository.GetById(editModelId)).Returns(model).Verifiable();
            var modelService = new StudentService(mockRepository.Object);
            var modelController = new StudentController(modelService, _mapper);

            // Act
            var result = modelController.Edit(editModelId);

            // Assert
            var redirectToAction = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToAction.ActionName);
        }
    }
}
