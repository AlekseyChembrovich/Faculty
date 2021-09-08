using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Faculty.DataAccessLayer;
using Faculty.DataAccessLayer.Models;
using Microsoft.Extensions.Configuration;
using Faculty.DataAccessLayer.RepositoryAdo;
using FluentAssertions;
using System.Collections.Generic;

namespace Faculty.IntegrationTests.RepositoryAdoTests
{
    [TestFixture]
    public class RepositoryAdoStudentCommandTest
    {
        private IRepository<Student> _repository;
        private DatabaseConfiguration _databaseConfiguration;

        [SetUp]
        public void Setup()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile(Path.Combine(Environment.CurrentDirectory, "appsettings.json")).Build();
            _databaseConfiguration = new DatabaseConfiguration(configuration);
            //_databaseConfiguration.DropTestDatabase();
            _databaseConfiguration.DeployTestDatabase();
            _repository = new RepositoryAdoStudent(_databaseConfiguration.ContextAdo);
        }

        [TestCase("test6", "test6", "test6")]
        public void InsertMethod_WhenInsertStudentEntityRepositoryAdo_ThenStudentEntityInserted(string surname, string name, string doublename)
        {
            // Arrange
            const int id = 6;
            var student = new Student { Id = id, Surname = surname, Name = name, Doublename = doublename };
            
            // Act
            _repository.Insert(student);
            var studentInserted = _repository.GetById(id);

            // Assert
            student.Should().BeEquivalentTo(studentInserted);
        }

        [Test]
        public void UpdateMethod_WhenUpdateStudentEntityRepositoryAdo_ThenStudentEntityUpdated()
        {
            // Arrange
            const int id = 1;
            const string newName = "test7";
            var student = _repository.GetById(id);

            // Act
            student.Name = newName;
            _repository.Update(student);
            var studentChanged = _repository.GetById(id);

            // Assert
            student.Should().BeEquivalentTo(studentChanged);
        }

        [Test]
        public void DeleteMethod_WhenDeleteStudentEntityRepositoryAdo_ThenStudentEntityDeleted()
        {
            // Arrange
            const int id = 2;
            var student = _repository.GetById(id);

            // Act
            _repository.Delete(student);
            var studentDeleted = _repository.GetById(id);

            // Assert
            Assert.IsNull(studentDeleted);
        }

        [Test]
        public void GetAllMethod_WhenSelectStudentsEntitiesRepositoryAdo_ThenSpecializationsEntitiesSelected()
        {
            // Arrange
            _databaseConfiguration.DeployTestDatabase();
            var students = new List<Student>()
            {
                new() { Id = 1, Surname = "test1", Name = "test1", Doublename = "test1" },
                new() { Id = 2, Surname = "test2", Name = "test2", Doublename = "test2" },
                new() { Id = 3, Surname = "test1", Name = "test1", Doublename = "test1" },
                new() { Id = 4, Surname = "test1", Name = "test1", Doublename = "test1" },
                new() { Id = 5, Surname = "test5", Name = "test5", Doublename = "test5" }
            };

            // Act
            var studentsFinded = _repository.GetAll().ToList();

            // Assert
            students.Should().BeEquivalentTo(studentsFinded);
        }

        [Test]
        public void GetByIdMethod_WhenSelectStudentEntityRepositoryAdo_ThenSpecializationEntitySelected()
        {
            // Arrange
            const int id = 1;

            // Act
            var student = _repository.GetById(id);

            // Assert
            Assert.IsTrue(student.Id == id);
        }
    }
}
