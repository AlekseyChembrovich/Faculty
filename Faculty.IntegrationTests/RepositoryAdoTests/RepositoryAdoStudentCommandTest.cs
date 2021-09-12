using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using FluentAssertions;
using Faculty.DataAccessLayer;
using System.Collections.Generic;
using Faculty.DataAccessLayer.Models;
using Microsoft.Extensions.Configuration;
using Faculty.DataAccessLayer.RepositoryAdo;

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
            _databaseConfiguration.DeployTestDatabase();
            _repository = new RepositoryAdoStudent(_databaseConfiguration.ContextAdo);
        }

        [TearDown]
        public void TearDown()
        {
            _databaseConfiguration.DropTestDatabase();
        }

        [TestCase("test6", "test6", "test6")]
        public void InsertMethod_WhenInsertStudentEntity_ThenEntityInserted(string surname, string name, string doublename)
        {
            // Arrange
            var studentToInsert = new Student { Surname = surname, Name = name, Doublename = doublename };
            var studentsExisting = new List<Student>
            {
                new() { Surname = "test1", Name = "test1", Doublename = "test1" },
                new() { Surname = "test2", Name = "test2", Doublename = "test2" },
                new() { Surname = "test3", Name = "test3", Doublename = "test3" },
                new() { Surname = "test4", Name = "test4", Doublename = "test4" },
                new() { Surname = "test5", Name = "test5", Doublename = "test5" }
            };

            // Act
            _repository.Insert(studentToInsert);
            studentsExisting.Add(studentToInsert);
            var modifiedStudentsFromDatabase = _repository.GetAll();

            // Assert
            studentsExisting.Should().BeEquivalentTo(modifiedStudentsFromDatabase, options => options.Excluding(c => c.Id).Excluding(c => c.Faculties));
        }

        [Test]
        public void UpdateMethod_WhenUpdateStudentEntity_ThenEntityUpdated()
        {
            // Arrange
            const int studentExistingId = 1;
            const string studentNewName = "test7";
            var studentsModified = new List<Student>
            {
                new() { Surname = "test1", Name = studentNewName, Doublename = "test1" },
                new() { Surname = "test2", Name = "test2", Doublename = "test2" },
                new() { Surname = "test3", Name = "test3", Doublename = "test3" },
                new() { Surname = "test4", Name = "test4", Doublename = "test4" },
                new() { Surname = "test5", Name = "test5", Doublename = "test5" }
            };
            var studentToChange = _repository.GetById(studentExistingId);

            // Act
            studentToChange.Name = studentNewName;
            _repository.Update(studentToChange);
            var modifiedStudentsFromDatabase = _repository.GetAll();

            // Assert
            studentsModified.Should().BeEquivalentTo(modifiedStudentsFromDatabase, options => options.Excluding(c => c.Id).Excluding(c => c.Faculties));
        }

        [Test]
        public void DeleteMethod_WhenDeleteStudentEntity_ThenEntityDeleted()
        {
            // Arrange
            const int studentExistingId = 2;
            var studentToDelete = _repository.GetById(studentExistingId);
            var studentsWithoutDeletedEntity = new List<Student>
            {
                new() { Surname = "test1", Name = "test1", Doublename = "test1" },
                new() { Surname = "test3", Name = "test3", Doublename = "test3" },
                new() { Surname = "test4", Name = "test4", Doublename = "test4" },
                new() { Surname = "test5", Name = "test5", Doublename = "test5" }
            };

            // Act
            _repository.Delete(studentToDelete);
            var modifiedStudentsFromDatabase = _repository.GetAll().ToList();

            // Assert
            studentsWithoutDeletedEntity.Should().BeEquivalentTo(modifiedStudentsFromDatabase, options => options.Excluding(c => c.Id).Excluding(c => c.Faculties));
        }

        [Test]
        public void GetAllMethod_WhenSelectStudentsEntities_ThenEntitiesSelected()
        {
            // Arrange
            var studentsExisting = new List<Student>
            {
                new() { Surname = "test1", Name = "test1", Doublename = "test1" },
                new() { Surname = "test2", Name = "test2", Doublename = "test2" },
                new() { Surname = "test3", Name = "test3", Doublename = "test3" },
                new() { Surname = "test4", Name = "test4", Doublename = "test4" },
                new() { Surname = "test5", Name = "test5", Doublename = "test5" }
            };

            // Act
            var wholeStudentsDataSet = _repository.GetAll().ToList();

            // Assert
            studentsExisting.Should().BeEquivalentTo(wholeStudentsDataSet, options => options.Excluding(c => c.Id).Excluding(c => c.Faculties));
        }

        [Test]
        public void GetByIdMethod_WhenSelectStudentEntity_ThenEntitySelected()
        {
            // Arrange
            const int curatorExistingId = 3;
            var studentExisting = new Student { Surname = "test3", Name = "test3", Doublename = "test3" };

            // Act
            var studentFounded = _repository.GetById(curatorExistingId);

            // Assert
            studentExisting.Should().BeEquivalentTo(studentFounded, options => options.Excluding(c => c.Id).Excluding(c => c.Faculties));
        }
    }
}
