using Moq;
using Xunit;
using System;
using AutoMapper;
using System.Linq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Faculty.AspUI.Controllers;
using System.Collections.Generic;
using Faculty.DataAccessLayer.Models;
using Faculty.BusinessLayer.Services;
using Faculty.AspUI.ViewModels.Faculty;
using Faculty.BusinessLayer.Dto.Faculty;
using Faculty.DataAccessLayer.Repository;
using Faculty.DataAccessLayer.Repository.EntityFramework.Interfaces;

namespace Faculty.UnitTests
{
    public class FacultyControllerActionsTests
    {
        private readonly Mock<IRepositoryFaculty> _mockRepositoryFaculty;
        private readonly GroupService _groupService;
        private readonly StudentService _studentService;
        private readonly CuratorService _curatorService;
        private readonly IMapper _mapper;

        public FacultyControllerActionsTests()
        {
            _mockRepositoryFaculty = new Mock<IRepositoryFaculty>();
            var mockRepositoryGroup = new Mock<IRepositoryGroup>();
            var mockRepositoryStudent = new Mock<IRepository<Student>>();
            var mockRepositoryCurator = new Mock<IRepository<Curator>>();
            _groupService = new GroupService(mockRepositoryGroup.Object);
            _studentService = new StudentService(mockRepositoryStudent.Object);
            _curatorService = new CuratorService(mockRepositoryCurator.Object);
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            _mapper = new Mapper(mapperConfiguration);
        }

        [Fact]
        public void IndexMethod_ReturnsViewResult_WithListOfDisplayModelsDisplay()
        {
            // Arrange
            _mockRepositoryFaculty.Setup(repository => repository.GetAllIncludeForeignKey()).Returns(GetTestModels()).Verifiable();
            var facultyService = new FacultyService(_mockRepositoryFaculty.Object);
            var modelController = new FacultyController(facultyService, _groupService, _studentService, _curatorService, _mapper);

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
            var modelAdd = new FacultyAdd
            {
                StartDateEducation = DateTime.Now,
                CountYearEducation = 5,
                StudentId = 1,
                CuratorId = 1,
                GroupId = 1
            };
            var modelDto = _mapper.Map<FacultyAdd, FacultyAddDto>(modelAdd);
            var model = _mapper.Map<FacultyAddDto, DataAccessLayer.Models.Faculty>(modelDto);
            _mockRepositoryFaculty.Setup(repository => repository.Insert(model)).Verifiable();
            var facultyService = new FacultyService(_mockRepositoryFaculty.Object);
            var modelController = new FacultyController(facultyService, _groupService, _studentService, _curatorService, _mapper);

            // Act
            var result = modelController.Create(modelAdd);

            // Assert
            var redirectToAction = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToAction.ActionName);
            _mockRepositoryFaculty.Verify(r => r.Insert(It.IsAny<DataAccessLayer.Models.Faculty>()), Times.Once);
        }

        [Fact]
        public void CreateMethod_ReturnsViewResultWithModel_ForInvalidModel()
        {
            // Arrange
            var modelAdd = new FacultyAdd
            {
                StartDateEducation = DateTime.Now,
                CountYearEducation = 55,
                StudentId = 1,
                CuratorId = 1,
                GroupId = 1
            };
            var modelDto = _mapper.Map<FacultyAdd, FacultyAddDto>(modelAdd);
            var model = _mapper.Map<FacultyAddDto, DataAccessLayer.Models.Faculty>(modelDto);
            _mockRepositoryFaculty.Setup(repository => repository.Insert(model)).Verifiable();
            var facultyService = new FacultyService(_mockRepositoryFaculty.Object);
            var modelController = new FacultyController(facultyService, _groupService, _studentService, _curatorService, _mapper);
            modelController.ModelState.AddModelError("YearsRange", "Count year education should be between 3 and 5.");

            // Act
            var result = modelController.Create(modelAdd);

            // Assert
            Assert.IsType<ViewResult>(result);
            _mockRepositoryFaculty.Verify(r => r.Insert(It.IsAny<DataAccessLayer.Models.Faculty>()), Times.Never);
        }

        [Fact]
        public void DeleteGetMethod_CallDeleteMethodRepository_RedirectToIndexMethod_ForCorrectArgument()
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
            var facultyService = new FacultyService(_mockRepositoryFaculty.Object);
            var modelController = new FacultyController(facultyService, _groupService, _studentService, _curatorService, _mapper);

            // Act
            var result = modelController.Delete(deleteModelId);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            _mockRepositoryFaculty.Verify(r => r.Delete(It.IsAny<DataAccessLayer.Models.Faculty>()), Times.Once);
        }

        [Fact]
        public void DeletePostMethod_CallDeleteMethodRepository_RedirectToIndexMethod_ForCorrectArgument()
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
            var modelDto = _mapper.Map<FacultyModify, FacultyModifyDto>(modelModify);
            var model = _mapper.Map<FacultyModifyDto, DataAccessLayer.Models.Faculty>(modelDto);
            _mockRepositoryFaculty.Setup(repository => repository.Delete(model)).Verifiable();
            var facultyService = new FacultyService(_mockRepositoryFaculty.Object);
            var modelController = new FacultyController(facultyService, _groupService, _studentService, _curatorService, _mapper);

            // Act
            var result = modelController.Delete(modelModify);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            _mockRepositoryFaculty.Verify(r => r.Delete(It.IsAny<DataAccessLayer.Models.Faculty>()), Times.Once);
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
            var modelDto = _mapper.Map<FacultyModify, FacultyModifyDto>(modelModify);
            var model = _mapper.Map<FacultyModifyDto, DataAccessLayer.Models.Faculty>(modelDto);
            _mockRepositoryFaculty.Setup(repository => repository.Update(model)).Verifiable();
            var facultyService = new FacultyService(_mockRepositoryFaculty.Object);
            var modelController = new FacultyController(facultyService, _groupService, _studentService, _curatorService, _mapper);

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
                CountYearEducation = 55,
                StudentId = 1,
                CuratorId = 1,
                GroupId = 1
            };
            var modelDto = _mapper.Map<FacultyModify, FacultyModifyDto>(modelModify);
            var model = _mapper.Map<FacultyModifyDto, DataAccessLayer.Models.Faculty>(modelDto);
            _mockRepositoryFaculty.Setup(repository => repository.Update(model)).Verifiable();
            var facultyService = new FacultyService(_mockRepositoryFaculty.Object);
            var modelController = new FacultyController(facultyService, _groupService, _studentService, _curatorService, _mapper);
            modelController.ModelState.AddModelError("YearsRange", "Count year education should be between 3 and 5.");

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
            var modelDto = _mapper.Map<FacultyModify, FacultyModifyDto>(modelModify);
            var model = _mapper.Map<FacultyModifyDto, DataAccessLayer.Models.Faculty>(modelDto);
            _mockRepositoryFaculty.Setup(repository => repository.GetById(editModelId)).Returns(model).Verifiable();
            var facultyService = new FacultyService(_mockRepositoryFaculty.Object);
            var modelController = new FacultyController(facultyService, _groupService, _studentService, _curatorService, _mapper);

            // Act
            var result = modelController.Edit(editModelId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            modelModify.Should().BeEquivalentTo(viewResult.Model);
            _mockRepositoryFaculty.Verify(r => r.GetById(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void EditGetMethod_RedirectToIndexMethod_ForNotFoundedModel()
        {
            // Arrange
            const int editModelId = 1;
            DataAccessLayer.Models.Faculty model = default;
            _mockRepositoryFaculty.Setup(repository => repository.GetById(editModelId)).Returns(model).Verifiable();
            var facultyService = new FacultyService(_mockRepositoryFaculty.Object);
            var modelController = new FacultyController(facultyService, _groupService, _studentService, _curatorService, _mapper);

            // Act
            var result = modelController.Edit(editModelId);

            // Assert
            var redirectToAction = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToAction.ActionName);
        }
    }
}
