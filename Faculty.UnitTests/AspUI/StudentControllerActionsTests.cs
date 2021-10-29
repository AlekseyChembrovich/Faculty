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
using Faculty.AspUI.ViewModels.Student;
using Faculty.BusinessLayer.Dto.Student;
using Faculty.DataAccessLayer.Repository;

namespace Faculty.UnitTests
{
    public class StudentControllerActionsTests
    {
        private readonly Mock<IRepository<Student>> _mockRepositoryStudent;
        private readonly IMapper _mapper;

        public StudentControllerActionsTests()
        {
            _mockRepositoryStudent = new Mock<IRepository<Student>>();
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            _mapper = new Mapper(mapperConfiguration);
        }

        [Fact]
        public void IndexMethod_ReturnsViewResult_WithListOfDisplayModelsDisplay()
        {
            // Arrange
            _mockRepositoryStudent.Setup(repository => repository.GetAll()).Returns(GetTestModels()).Verifiable();
            var modelService = new StudentService(_mockRepositoryStudent.Object, _mapper);
            var modelController = new StudentController(modelService, _mapper);

            // Act
            var result = modelController.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var models = Assert.IsAssignableFrom<IEnumerable<StudentDisplayModify>>(viewResult.ViewData.Model);
            Assert.Equal(3, models.Count());
            _mockRepositoryStudent.Verify(r => r.GetAll());
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
            _mockRepositoryStudent.Setup(repository => repository.Insert(model)).Verifiable();
            var modelService = new StudentService(_mockRepositoryStudent.Object, _mapper);
            var modelController = new StudentController(modelService, _mapper);

            // Act
            var result = modelController.Create(modelAdd);

            // Assert
            var redirectToAction = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToAction.ActionName);
            _mockRepositoryStudent.Verify(r => r.Insert(It.IsAny<Student>()), Times.Once);
        }

        [Fact]
        public void DeleteGetMethod_CallDeleteMethodRepository_RedirectToIndexMethod_ForCorrectArgument()
        {
            // Arrange
            const int deleteModelId = 1;
            var model = new Student { Id = deleteModelId, Surname = "test1", Name = "test1", Doublename = "test1" };
            _mockRepositoryStudent.Setup(repository => repository.GetById(deleteModelId)).Returns(model).Verifiable();
            var modelService = new StudentService(_mockRepositoryStudent.Object, _mapper);
            var modelController = new StudentController(modelService, _mapper);

            // Act
            var result = modelController.Delete(deleteModelId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            model.Should().BeEquivalentTo(viewResult.Model);
            _mockRepositoryStudent.Verify(r => r.GetById(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void DeletePostMethod_CallDeleteMethodRepository_RedirectToIndexMethod_ForCorrectArgument()
        {
            // Arrange
            var modelModify = new StudentDisplayModify { Id = 1, Surname = "test1", Name = "test1", Doublename = "test1" };
            var modelDto = _mapper.Map<StudentDisplayModify, StudentDisplayModifyDto>(modelModify);
            var model = _mapper.Map<StudentDisplayModifyDto, Student>(modelDto);
            _mockRepositoryStudent.Setup(repository => repository.Delete(model)).Verifiable();
            var modelService = new StudentService(_mockRepositoryStudent.Object, _mapper);
            var modelController = new StudentController(modelService, _mapper);

            // Act
            var result = modelController.Delete(modelModify);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            _mockRepositoryStudent.Verify(r => r.Delete(It.IsAny<Student>()), Times.Once);
        }

        [Fact]
        public void EditPostMethod_CallUpdateMethodRepository_RedirectToIndexMethod_ForCorrectModel()
        {
            // Arrange
            var modelModify = new StudentDisplayModify { Id = 1, Surname = "test1", Name = "test1", Doublename = "test1" };
            var modelDto = _mapper.Map<StudentDisplayModify, StudentDisplayModifyDto>(modelModify);
            var model = _mapper.Map<StudentDisplayModifyDto, Student>(modelDto);
            _mockRepositoryStudent.Setup(repository => repository.Update(model)).Verifiable();
            var modelService = new StudentService(_mockRepositoryStudent.Object, _mapper);
            var modelController = new StudentController(modelService, _mapper);

            // Act
            var result = modelController.Edit(modelModify);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            _mockRepositoryStudent.Verify(r => r.Update(It.IsAny<Student>()), Times.Once);
        }

        [Fact]
        public void EditGetMethod_CallGetByIdMethodRepository_ReturnsViewResultWithModel_ForCorrectArgument()
        {
            // Arrange
            const int editModelId = 1;
            var modelModify = new StudentDisplayModify { Id = editModelId, Surname = "test1", Name = "test1", Doublename = "test1" };
            var modelDto = _mapper.Map<StudentDisplayModify, StudentDisplayModifyDto>(modelModify);
            var model = _mapper.Map<StudentDisplayModifyDto, Student>(modelDto);
            _mockRepositoryStudent.Setup(repository => repository.GetById(editModelId)).Returns(model).Verifiable();
            var modelService = new StudentService(_mockRepositoryStudent.Object, _mapper);
            var modelController = new StudentController(modelService, _mapper);

            // Act
            var result = modelController.Edit(editModelId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            modelModify.Should().BeEquivalentTo(viewResult.Model);
            _mockRepositoryStudent.Verify(r => r.GetById(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void EditGetMethod_RedirectToIndexMethod_ForNotFoundedModel()
        {
            // Arrange
            const int editModelId = 1;
            Student model = default;
            _mockRepositoryStudent.Setup(repository => repository.GetById(editModelId)).Returns(model).Verifiable();
            var modelService = new StudentService(_mockRepositoryStudent.Object, _mapper);
            var modelController = new StudentController(modelService, _mapper);

            // Act
            var result = modelController.Edit(editModelId);

            // Assert
            var redirectToAction = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToAction.ActionName);
        }
    }
}
