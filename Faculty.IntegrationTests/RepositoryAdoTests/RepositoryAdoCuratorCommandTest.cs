using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Faculty.DataAccessLayer;
using Faculty.DataAccessLayer.Models;
using Faculty.DataAccessLayer.RepositoryAdo;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace Faculty.IntegrationTests.RepositoryAdoTests
{
    [TestFixture]
    public class RepositoryAdoCuratorCommandTest
    {
        private IRepository<Curator> _repository;
        private DatabaseConfiguration _databaseConfiguration;

        [SetUp]
        public void Setup()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile(Path.Combine(Environment.CurrentDirectory, "appsettings.json")).Build();
            _databaseConfiguration = new DatabaseConfiguration(configuration);
            //_databaseConfiguration.DropTestDatabase();
            //_databaseConfiguration.DeployTestDatabase();
            _repository = new RepositoryAdoCurator(_databaseConfiguration.ContextAdo);
        }

        [Test]
        public void Test()
        {
            _databaseConfiguration.DeployTestDatabase();
            Assert.IsTrue(true);
        }

        [TestCase("test6", "test6", "test6", "+375-29-557-06-67")]
        public void InsertMethod_WhenInsertCuratorEntityRepositoryAdo_ThenStudentEntityInserted(string surname, string name, string doublename, string phone)
        {
            // Arrange
            const int id = 6;
            var curator = new Curator { Id = id, Surname = surname, Name = name, Doublename = doublename, Phone = phone };

            // Act
            _repository.Insert(curator);
            var curatorInserted = _repository.GetById(id);

            // Assert
            curator.Should().BeEquivalentTo(curatorInserted);
        }

        [Test]
        public void UpdateMethod_WhenUpdateCuratorEntityRepositoryAdo_ThenStudentEntityUpdated()
        {
            // Arrange
            const int id = 1;
            const string newName = "test7";
            var curator = _repository.GetById(id);

            // Act
            curator.Doublename = newName;
            _repository.Update(curator);
            var curatorChanged = _repository.GetById(id);

            // Assert
            curator.Should().BeEquivalentTo(curatorChanged);
        }

        [Test]
        public void DeleteMethod_WhenDeleteCuratorEntityRepositoryAdo_ThenStudentEntityDeleted()
        {
            // Arrange
            const int id = 2;
            var curator = _repository.GetById(id);

            // Act
            _repository.Delete(curator);
            var curatorDeleted = _repository.GetById(id);

            // Assert
            Assert.IsNull(curatorDeleted);
        }

        [Test]
        public void GetAllMethod_WhenSelectCuratorsEntitiesRepositoryAdo_ThenSpecializationsEntitiesSelected()
        {
            // Arrange
            _databaseConfiguration.DeployTestDatabase();
            var curators = new List<Curator>() 
            {
                new() { Id = 1, Surname = "test1", Name = "test1", Doublename = "test1", Phone = "+375-33-111-11-11" },
                new() { Id = 2, Surname = "test2", Name = "test2", Doublename = "test2", Phone = "+375-33-222-22-22" },
                new() { Id = 3, Surname = "test3", Name = "test3", Doublename = "test3", Phone = "+375-33-333-33-33" },
                new() { Id = 4, Surname = "test4", Name = "test4", Doublename = "test4", Phone = "+375-33-444-44-44" },
                new() { Id = 5, Surname = "test5", Name = "test5", Doublename = "test5", Phone = "+375-33-555-55-55" }
            };

            // Act
            var curatorsFinded = _repository.GetAll().ToList();

            // Assert
            curators.Should().BeEquivalentTo(curatorsFinded);
        }

        [Test]
        public void GetByIdMethod_WhenSelectCuratorEntityRepositoryAdo_ThenSpecializationEntitySelected()
        {
            // Arrange
            const int id = 3;

            // Act
            var curatorFinded = _repository.GetById(id);

            // Assert
            Assert.IsTrue(curatorFinded.Id == id);
        }
    }
}
