using Moq;
using Xunit;
using System;
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
using Faculty.BusinessLayer.ModelsDto.FacultyDto;
using Faculty.DataAccessLayer.RepositoryEntityFramework;

namespace Faculty.UnitTests
{
    public class FacultyControllerActionsTests
    {
        private readonly Mock<IRepositoryFaculty> _mockRepositoryFaculty;
        private readonly Mock<IRepositoryGroup> _mockRepositoryGroup;
        private readonly Mock<IRepository<Student>> _mockRepositoryStudent;
        private readonly Mock<IRepository<Curator>> _mockRepositoryCurator;

        public FacultyControllerActionsTests()
        {
            _mockRepositoryFaculty = new Mock<IRepositoryFaculty>();
            _mockRepositoryGroup = new Mock<IRepositoryGroup>();
            _mockRepositoryStudent = new Mock<IRepository<Student>>();
            _mockRepositoryCurator = new Mock<IRepository<Curator>>();
        }

        [Fact]
        public void IndexMethod_ReturnsViewResult_WithListOfDisplayModelsDisplay()
        {
            // Arrange
            _mockRepositoryFaculty.Setup(repository => repository.GetAllIncludeForeignKey()).Returns(GetTestModels()).Verifiable();
            var modelService = new FacultyService(_mockRepositoryFaculty.Object, _mockRepositoryGroup.Object, _mockRepositoryStudent.Object, _mockRepositoryCurator.Object);
            var modelController = new FacultyController(modelService);

            // Act
            var result = modelController.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var models = Assert.IsAssignableFrom<IEnumerable<FacultyDisplay>>(viewResult.ViewData.Model);
            Assert.Equal(3, models.Count());
            _mockRepositoryFaculty.Verify(r => r.GetAllIncludeForeignKey());
        }

        private static IEnumerable<DataAccessLayer.Models.Faculty> GetTestModels()
        {
            var models = new List<DataAccessLayer.Models.Faculty>()
            {
                new ()
                {
                    Id = 1,
                    StartDateEducation = DateTime.Now,
                    CountYearEducation = 5,
                    StudentId = 1,
                    CuratorId = 1,
                    GroupId = 1
                },
                new ()
                {
                    Id = 2,
                    StartDateEducation = DateTime.Now,
                    CountYearEducation = 4,
                    StudentId = 2,
                    CuratorId = 2,
                    GroupId = 2
                },
                new ()
                {
                    Id = 3,
                    StartDateEducation = DateTime.Now,
                    CountYearEducation = 5,
                    StudentId = 3,
                    CuratorId = 3,
                    GroupId = 3
                }
            };

            return models;
        }

        [Fact]
        public void CreateMethod_CallInsertMethodRepository_RedirectToIndexMethodWith_ForCorrectModel()
        {
            // Arrange
            var modelModify = new FacultyModify
            {
                StartDateEducation = DateTime.Now,
                CountYearEducation = 5,
                StudentId = 1,
                CuratorId = 1,
                GroupId = 1
            };
            Mapper.Initialize(cfg => cfg.CreateMap<FacultyModify, CreateFacultyDto>());
            var modelDto = Mapper.Map<FacultyModify, CreateFacultyDto>(modelModify);
            Mapper.Initialize(cfg => cfg.CreateMap<CreateFacultyDto, DataAccessLayer.Models.Faculty>());
            var model = Mapper.Map<CreateFacultyDto, DataAccessLayer.Models.Faculty>(modelDto);
            _mockRepositoryFaculty.Setup(repository => repository.Insert(model)).Verifiable();
            var modelService = new FacultyService(_mockRepositoryFaculty.Object, _mockRepositoryGroup.Object, _mockRepositoryStudent.Object, _mockRepositoryCurator.Object);
            var modelController = new FacultyController(modelService);

            // Act
            var result = modelController.Create(modelModify);

            // Assert
            var redirectToAction = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToAction.ActionName);
            _mockRepositoryFaculty.Verify(r => r.Insert(It.IsAny<DataAccessLayer.Models.Faculty>()), Times.Once);
        }

        [Fact]
        public void CreateMethod_ReturnsViewResultWithModel_ForInvalidModel()
        {
            // Arrange
            var modelModify = new FacultyModify
            {
                StartDateEducation = DateTime.Now,
                CountYearEducation = null,
                StudentId = 1,
                CuratorId = 1,
                GroupId = 1
            };
            Mapper.Initialize(cfg => cfg.CreateMap<FacultyModify, CreateFacultyDto>());
            var modelDto = Mapper.Map<FacultyModify, CreateFacultyDto>(modelModify);
            Mapper.Initialize(cfg => cfg.CreateMap<CreateFacultyDto, DataAccessLayer.Models.Faculty>());
            var model = Mapper.Map<CreateFacultyDto, DataAccessLayer.Models.Faculty>(modelDto);
            _mockRepositoryFaculty.Setup(repository => repository.Insert(model)).Verifiable();
            var modelService = new FacultyService(_mockRepositoryFaculty.Object, _mockRepositoryGroup.Object, _mockRepositoryStudent.Object, _mockRepositoryCurator.Object);
            var modelController = new FacultyController(modelService);
            modelController.ModelState.AddModelError("CountYearEducationRequired", "Count year education is required.");

            // Act
            var result = modelController.Create(modelModify);

            // Assert
            Assert.IsType<ViewResult>(result);
            _mockRepositoryFaculty.Verify(r => r.Insert(It.IsAny<DataAccessLayer.Models.Faculty>()), Times.Never);
        }

        [Fact]
        public void DeleteMethod_CallDeleteMethodRepository_RedirectToIndexMethod_ForCorrectArgument()
        {
            // Arrange
            const int deleteModelId = 1;
            var model = new DataAccessLayer.Models.Faculty
            {
                Id = deleteModelId,
                StartDateEducation = DateTime.Now,
                CountYearEducation = 5,
                StudentId = 1,
                CuratorId = 1,
                GroupId = 1
            };
            _mockRepositoryFaculty.Setup(repository => repository.Delete(model)).Verifiable();
            var modelService = new FacultyService(_mockRepositoryFaculty.Object, _mockRepositoryGroup.Object, _mockRepositoryStudent.Object, _mockRepositoryCurator.Object);
            var modelController = new FacultyController(modelService);

            // Act
            var result = modelController.Delete(deleteModelId);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            _mockRepositoryFaculty.Verify(r => r.Delete(It.IsAny<DataAccessLayer.Models.Faculty>()), Times.Once);
        }

        [Fact]
        public void DeleteMethod_RedirectToIndexMethod_ForInvalidArgument()
        {
            // Arrange
            int? deleteModelId = null;
            var model = new DataAccessLayer.Models.Faculty
            {
                StartDateEducation = DateTime.Now,
                CountYearEducation = 5,
                StudentId = 1,
                CuratorId = 1,
                GroupId = 1
            };
            _mockRepositoryFaculty.Setup(repository => repository.Delete(model)).Verifiable();
            var modelService = new FacultyService(_mockRepositoryFaculty.Object, _mockRepositoryGroup.Object, _mockRepositoryStudent.Object, _mockRepositoryCurator.Object);
            var modelController = new FacultyController(modelService);

            // Act
            var result = modelController.Delete(deleteModelId);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            _mockRepositoryFaculty.Verify(r => r.Delete(It.IsAny<DataAccessLayer.Models.Faculty>()), Times.Never);
        }

        [Fact]
        public void EditPostMethod_CallUpdateMethodRepository_RedirectToIndexMethod_ForCorrectModel()
        {
            // Arrange
            var modelModify = new FacultyModify
            {
                Id = 1,
                StartDateEducation = DateTime.Now,
                CountYearEducation = 5,
                StudentId = 1,
                CuratorId = 1,
                GroupId = 1
            };
            Mapper.Initialize(cfg => cfg.CreateMap<FacultyModify, EditFacultyDto>());
            var modelDto = Mapper.Map<FacultyModify, EditFacultyDto>(modelModify);
            Mapper.Initialize(cfg => cfg.CreateMap<EditFacultyDto, DataAccessLayer.Models.Faculty>());
            var model = Mapper.Map<EditFacultyDto, DataAccessLayer.Models.Faculty>(modelDto);
            _mockRepositoryFaculty.Setup(repository => repository.Update(model)).Verifiable();
            var modelService = new FacultyService(_mockRepositoryFaculty.Object, _mockRepositoryGroup.Object, _mockRepositoryStudent.Object, _mockRepositoryCurator.Object);
            var modelController = new FacultyController(modelService);

            // Act
            var result = modelController.Edit(modelModify);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            _mockRepositoryFaculty.Verify(r => r.Update(It.IsAny<DataAccessLayer.Models.Faculty>()), Times.Once);
        }

        [Fact]
        public void EditPostMethod_ReturnsViewResultWithModel_ForInvalidModel()
        {
            // Arrange
            var modelModify = new FacultyModify
            {
                Id = 1,
                StartDateEducation = DateTime.Now,
                CountYearEducation = null,
                StudentId = 1,
                CuratorId = 1,
                GroupId = 1
            };
            Mapper.Initialize(cfg => cfg.CreateMap<FacultyModify, EditFacultyDto>());
            var modelDto = Mapper.Map<FacultyModify, EditFacultyDto>(modelModify);
            Mapper.Initialize(cfg => cfg.CreateMap<EditFacultyDto, DataAccessLayer.Models.Faculty>());
            var model = Mapper.Map<EditFacultyDto, DataAccessLayer.Models.Faculty>(modelDto);
            _mockRepositoryFaculty.Setup(repository => repository.Update(model)).Verifiable();
            var modelService = new FacultyService(_mockRepositoryFaculty.Object, _mockRepositoryGroup.Object, _mockRepositoryStudent.Object, _mockRepositoryCurator.Object);
            var modelController = new FacultyController(modelService);
            modelController.ModelState.AddModelError("CountYearEducationRequired", "Count year education is required.");

            // Act
            var result = modelController.Edit(modelModify);

            // Assert
            Assert.IsType<ViewResult>(result);
            _mockRepositoryFaculty.Verify(r => r.Update(It.IsAny<DataAccessLayer.Models.Faculty>()), Times.Never);
        }

        [Fact]
        public void EditGetMethod_CallGetByIdMethodRepository_ReturnsViewResultWithModel_ForCorrectArgument()
        {
            // Arrange
            const int editModelId = 1;
            var modelModify = new FacultyModify
            {
                Id = editModelId,
                StartDateEducation = DateTime.Now,
                CountYearEducation = 5,
                StudentId = 1,
                CuratorId = 1,
                GroupId = 1
            };
            Mapper.Initialize(cfg => cfg.CreateMap<FacultyModify, EditFacultyDto>());
            var modelDto = Mapper.Map<FacultyModify, EditFacultyDto>(modelModify);
            Mapper.Initialize(cfg => cfg.CreateMap<EditFacultyDto, DataAccessLayer.Models.Faculty>());
            var model = Mapper.Map<EditFacultyDto, DataAccessLayer.Models.Faculty>(modelDto);
            _mockRepositoryFaculty.Setup(repository => repository.GetById(editModelId)).Returns(model).Verifiable();
            var modelService = new FacultyService(_mockRepositoryFaculty.Object, _mockRepositoryGroup.Object, _mockRepositoryStudent.Object, _mockRepositoryCurator.Object);
            var modelController = new FacultyController(modelService);

            // Act
            var result = modelController.Edit(editModelId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            modelModify.Should().BeEquivalentTo(viewResult.Model);
            _mockRepositoryFaculty.Verify(r => r.GetById(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void EditGetMethod_RedirectToIndexMethod_ForInvalidArgument()
        {
            // Arrange
            int? editModelId = null;
            var modelService = new FacultyService(_mockRepositoryFaculty.Object, _mockRepositoryGroup.Object, _mockRepositoryStudent.Object, _mockRepositoryCurator.Object);
            var modelController = new FacultyController(modelService);

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
            DataAccessLayer.Models.Faculty model = default;
            _mockRepositoryFaculty.Setup(repository => repository.GetById(editModelId)).Returns(model).Verifiable();
            var modelService = new FacultyService(_mockRepositoryFaculty.Object, _mockRepositoryGroup.Object, _mockRepositoryStudent.Object, _mockRepositoryCurator.Object);
            var modelController = new FacultyController(modelService);

            // Act
            var result = modelController.Edit(editModelId);

            // Assert
            var redirectToAction = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToAction.ActionName);
        }
    }
}
