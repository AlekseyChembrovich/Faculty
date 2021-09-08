using System;
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
    public class RepositoryAdoGroupCommandTest
    {
        private IRepository<Group> _repository;
        private DatabaseConfiguration _databaseConfiguration;

        [SetUp]
        public void Setup()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile(Path.Combine(Environment.CurrentDirectory, "appsettings.json")).Build();
            _databaseConfiguration = new DatabaseConfiguration(configuration);
            //_databaseConfiguration.DropTestDatabase();
            _databaseConfiguration.DeployTestDatabase();
            _repository = new RepositoryAdoGroup(_databaseConfiguration.ContextAdo);
        }

        [TestCase("test6", 1)]
        public void InsertMethod_WhenInsertStudentEntityRepositoryAdo_ThenStudentEntityInserted(string name, int specializationId)
        {
            // Arrange
            const int id = 6;
            var group = new Group { Id = id, Name = name, SpecializationId = specializationId };

            // Act
            _repository.Insert(group);
            var groupInserted = _repository.GetById(id);

            // Assert
            group.Should().BeEquivalentTo(groupInserted);
        }

        [Test]
        public void UpdateMethod_WhenUpdateStudentEntityRepositoryAdo_ThenStudentEntityUpdated()
        {
            // Arrange
            const int id = 1;
            const string newName = "test7";
            var group = _repository.GetById(id);

            // Act
            group.Name = newName;
            _repository.Update(group);
            var groupChanged = _repository.GetById(id);

            // Assert
            group.Should().BeEquivalentTo(groupChanged);
        }

        [Test]
        public void DeleteMethod_WhenDeleteStudentEntityRepositoryAdo_ThenStudentEntityDeleted()
        {
            // Arrange
            const int id = 2;
            var group = _repository.GetById(id);

            // Act
            _repository.Delete(group);
            var groupDeleted = _repository.GetById(id);

            // Assert
            Assert.IsNull(groupDeleted);
        }

        [Test]
        public void GetAllMethod_WhenSelectSpecializationsEntitiesRepositoryAdo_ThenSpecializationsEntitiesSelected()
        {
            // Arrange
            //IRepository<Specialization> repository = new RepositoryAdoSpecialization(_contextAdo);

            // Act
            var listResult = _repository.GetAll().ToList();

            // Assert
            Assert.IsTrue(listResult.Count > 0);
        }

        [Test]
        public void GetByIdMethod_WhenSelectSpecializationEntityRepositoryAdo_ThenSpecializationEntitySelected()
        {
            // Arrange
            const int id = 1;

            // Act
            var group = _repository.GetById(id);

            // Assert
            Assert.IsTrue(group.Id == id);
        }
    }
}
