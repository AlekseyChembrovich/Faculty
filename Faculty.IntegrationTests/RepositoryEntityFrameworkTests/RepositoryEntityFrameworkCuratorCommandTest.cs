using Faculty.DataAccessLayer;
using Faculty.DataAccessLayer.Models;
using Faculty.DataAccessLayer.RepositoryEntityFramework;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using FluentAssertions;

namespace Faculty.IntegrationTests.RepositoryEntityFrameworkTests
{
    [TestFixture]
    public class RepositoryEntityFrameworkCuratorCommandTest
    {
        private IRepository<Curator> _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DbContext>().UseInMemoryDatabase(databaseName: "Test").Options;
            var contextEntity = new DatabaseContextEntityFramework(options);
            _repository = new BaseRepositoryEntityFramework<Curator>(contextEntity);
        }

        [TestCase(1, "Test1", "Test1", "Test1", null)]
        public void InsertMethod_WhenInsertCuratorEntityRepositoryEntityFramework_ThenCuratorEntityInserted(int id, string surname, string name, string doublename, string phone)
        {
            // Arrange
            var curator = new Curator { Id = id, Surname = surname, Name = name, Doublename = doublename, Phone = phone };

            // Act
            _repository.Insert(curator);
            var curatorFound = _repository.GetById(id);

            // Assert
            curator.Should().BeEquivalentTo(curatorFound);
        }

        [TestCase(2, "Test2", "Test2", "Test2", "+375-33-557-06-67")]
        public void UpdateMethod_WhenUpdateCuratorEntityRepositoryEntityFramework_ThenCuratorEntityUpdated(int id, string surname, string name, string doublename, string phone)
        {
            // Arrange
            const string newName = "Test6";
            var curator = new Curator { Id = id, Surname = surname, Name = name, Doublename = doublename, Phone = phone };
            _repository.Insert(curator);

            // Act
            curator.Name = newName;
            _repository.Update(curator);
            var curatorFound = _repository.GetById(id);

            // Assert
            curator.Should().BeEquivalentTo(curatorFound);
        }

        [TestCase(3, "Test3", "Test3", "Test3", "+375-33-557-06-67")]
        public void DeleteMethod_WhenDeleteCuratorEntityRepositoryEntityFramework_ThenCuratorEntityDeleted(int id, string surname, string name, string doublename, string phone)
        {
            // Arrange
            var curator = new Curator { Id = id, Surname = surname, Name = name, Doublename = doublename, Phone = phone };
            _repository.Insert(curator);

            // Act
            _repository.Delete(curator);
            var curatorFound = _repository.GetById(id);

            // Assert
            Assert.IsNull(curatorFound);
        }

        [TestCase(4, "Test4", "Test4", "Test4", "+375-33-557-06-67")]
        public void GetAllMethod_WhenSelectCuratorsEntitiesRepositoryEntityFramework_ThenSpecializationsEntitiesSelected(int id, string surname, string name, string doublename, string phone)
        {
            // Arrange
            var curator = new Curator { Id = id, Surname = surname, Name = name, Doublename = doublename, Phone = phone };
            _repository.Insert(curator);

            // Act
            var listResult = _repository.GetAll().ToList();

            // Assert
            Assert.IsTrue(listResult.Count > 0);
        }

        [TestCase(5, "Test5", "Test5", "Test5", "+375-33-557-06-67")]
        public void GetByIdMethod_WhenSelectCuratorEntityRepositoryEntityFramework_ThenSpecializationEntitySelected(int id, string surname, string name, string doublename, string phone)
        {
            // Arrange
            var curator = new Curator { Id = id, Surname = surname, Name = name, Doublename = doublename, Phone = phone };
            _repository.Insert(curator);

            // Act
            var curatorFound = _repository.GetById(id);

            // Assert
            curator.Should().BeEquivalentTo(curatorFound);
        }
    }
}
