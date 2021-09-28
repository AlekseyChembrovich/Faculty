using Moq;
using Xunit;
using AutoMapper;
using System.Linq;
using Faculty.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Faculty.AspUI.Controllers;
using System.Collections.Generic;
using Faculty.DataAccessLayer.Models;
using Faculty.BusinessLayer.Services;
using Faculty.BusinessLayer.ModelsDto;
using Faculty.BusinessLayer.ModelsDto.StudentDto;

namespace Faculty.UnitTests
{
    public class FacultyControllerActionsTests
    {
        [Fact]
        public void IndexMethod_ReturnsViewResult_WithListOfDisplayStudentsDto()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Student>>();
            mockRepository.Setup(repository => repository.GetAll()).Returns(GetTestStudents());
            var studentService = new StudentService(mockRepository.Object);
            var studentController = new StudentController(studentService);

            // Act
            var result = studentController.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var models = Assert.IsAssignableFrom<IEnumerable<DisplayStudentDto>>(viewResult.ViewData.Model);
            Assert.Equal(3, models.Count());
        }

        [Fact]
        public void CreateMethod_()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Student>>();
            var studentService = new StudentService(mockRepository.Object);
            var studentController = new StudentController(studentService);
            var createStudent = new CreateStudentDto { Surname = "test1", Name = "test1", Doublename = "test1" };
            Mapper.Initialize(cfg => cfg.CreateMap<CreateStudentDto, Student>());
            var student = Mapper.Map<CreateStudentDto, Student>(createStudent);

            // Act
            var result = studentController.Create(createStudent);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void DeleteMethod_()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Student>>();
            var studentService = new StudentService(mockRepository.Object);
            var studentController = new StudentController(studentService);
            const int deleteStudentId = 1;

            // Act
            var result = studentController.Delete(deleteStudentId);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void UpdateMethod_()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Student>>();
            var studentService = new StudentService(mockRepository.Object);
            var studentController = new StudentController(studentService);
            var editStudent = new EditStudentDto { Id = 1, Surname = "test1", Name = "test1", Doublename = "test1" };
            Mapper.Initialize(cfg => cfg.CreateMap<EditStudentDto, Student>());
            var student = Mapper.Map<EditStudentDto, Student>(editStudent);

            // Act
            var result = studentController.Edit(editStudent);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        private static IEnumerable<Student> GetTestStudents()
        {
            var students = new List<Student>()
            {
                new ()
                {
                    Id = 1,
                    Surname = "test1",
                    Name = "test1",
                    Doublename = "test1",
                },
                new ()
                {
                    Id = 2,
                    Surname = "test2",
                    Name = "test2",
                    Doublename = "test2",
                },
                new ()
                {
                    Id = 3,
                    Surname = "test3",
                    Name = "test3",
                    Doublename = "test3",
                }
            };

            return students;
        }
    }
}
