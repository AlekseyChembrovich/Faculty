using System.Linq;
using Faculty.DataAccessLayer;
using Faculty.DataAccessLayer.Models;
using Faculty.DataAccessLayer.RepositoryEntityFramework;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Faculty.IntegrationTests.RepositoryEntityFrameworkTests
{
    [TestFixture]
    public class RepositoryEntityFrameworkStudentCommandTest
    {
        private IRepository<Student> _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DbContext>().UseInMemoryDatabase(databaseName: "Test").Options;
            var contextEntity = new DatabaseContextEntityFramework(options);
            _repository = new BaseRepositoryEntityFramework<Student>(contextEntity);
        }

        [TestCase(1, "Test1", "Test1", "Test1")]
        public void InsertMethod_WhenInsertCuratorEntityRepositoryEntityFramework_ThenCuratorEntityInserted(int id, string surname, string name, string doublename)
        {
            // Arrange
            var student = new Student { Id = id, Surname = surname, Name = name, Doublename = doublename };

            // Act
            _repository.Insert(student);
            var studentFound = _repository.GetById(id);

            // Assert
            student.Should().BeEquivalentTo(studentFound);
        }

        [TestCase(2, "Test2", null, "Test2")]
        public void UpdateMethod_WhenUpdateCuratorEntityRepositoryEntityFramework_ThenCuratorEntityUpdated(int id, string surname, string name, string doublename)
        {
            // Arrange
            const string newName = "Test4";
            var student = new Student { Id = id, Surname = surname, Name = name, Doublename = doublename };
            _repository.Insert(student);

            // Act
            student.Name = newName;
            _repository.Update(student);
            var studentFound = _repository.GetById(id);

            // Assert
            student.Should().BeEquivalentTo(studentFound);
        }

        [TestCase(3, null, "Test3", "Test3")]
        public void DeleteMethod_WhenDeleteCuratorEntityRepositoryEntityFramework_ThenCuratorEntityDeleted(int id, string surname, string name, string doublename)
        {
            // Arrange
            var student = new Student { Id = id, Surname = surname, Name = name, Doublename = doublename };
            _repository.Insert(student);

            // Act
            _repository.Delete(student);
            var studentFound = _repository.GetById(id);

            // Assert
            Assert.IsNull(studentFound);
        }

        [TestCase(4, "Test4", "Test4", "Test4")]
        public void GetAllMethod_WhenSelectCuratorsEntitiesRepositoryEntityFramework_ThenSpecializationsEntitiesSelected(int id, string surname, string name, string doublename)
        {
            // Arrange
            var student = new Student { Id = id, Surname = surname, Name = name, Doublename = doublename };
            _repository.Insert(student);

            // Act
            var listResult = _repository.GetAll().ToList();

            // Assert
            Assert.IsTrue(listResult.Count > 0);
        }

        [TestCase(5, "Test5", "Test5", "Test5")]
        public void GetByIdMethod_WhenSelectCuratorEntityRepositoryEntityFramework_ThenSpecializationEntitySelected(int id, string surname, string name, string doublename)
        {
            // Arrange
            var student = new Student { Id = id, Surname = surname, Name = name, Doublename = doublename };
            _repository.Insert(student);

            // Act
            var studentFound = _repository.GetById(id);

            // Assert
            student.Should().BeEquivalentTo(studentFound);
        }
    }
}
