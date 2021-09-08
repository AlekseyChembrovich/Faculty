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
    public class RepositoryAdoSpecializationCommandTest
    {
        private IRepository<Specialization> _repository;
        private DatabaseConfiguration _databaseConfiguration;

        [SetUp]
        public void Setup()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile(Path.Combine(Environment.CurrentDirectory, "appsettings.json")).Build();
            _databaseConfiguration = new DatabaseConfiguration(configuration);
            //_databaseConfiguration.DropTestDatabase();
            _databaseConfiguration.DeployTestDatabase();
            _repository = new RepositoryAdoSpecialization(_databaseConfiguration.ContextAdo);
        }

        [TestCase("test1")]
        public void InsertMethod_WhenInsertStudentEntityRepositoryAdo_ThenStudentEntityInserted(string name)
        {
            // Arrange
            const int id = 6;
            var specialization = new Specialization { Id = id, Name = name };

            // Act
            _repository.Insert(specialization);
            var specializationInserted = _repository.GetById(id);

            // Assert
            specialization.Should().BeEquivalentTo(specializationInserted);
        }

        [Test]
        public void UpdateMethod_WhenUpdateStudentEntityRepositoryAdo_ThenStudentEntityUpdated()
        {
            // Arrange
            const int id = 1;
            const string newName = "test7";
            var specialization = _repository.GetById(id);

            // Act
            specialization.Name = newName;
            _repository.Update(specialization);
            var specializationChanged = _repository.GetById(id);

            // Assert
            specialization.Should().BeEquivalentTo(specializationChanged);
        }

        [Test]
        public void DeleteMethod_WhenDeleteStudentEntityRepositoryAdo_ThenStudentEntityDeleted()
        {
            // Arrange
            const int id = 2;
            var specialization = _repository.GetById(id);

            // Act
            _repository.Delete(specialization);
            var specializationDeleted = _repository.GetById(id);

            // Assert
            Assert.IsNull(specializationDeleted);
        }

        [Test]
        public void GetAllMethod_WhenSelectSpecializationsEntitiesRepositoryAdo_ThenSpecializationsEntitiesSelected()
        {
            // Arrange
            _databaseConfiguration.DeployTestDatabase();
            var specializations = new List<Specialization>()
            {
                new() { Id = 1, Name = "test1" },
                new() { Id = 2, Name = "test2" },
                new() { Id = 3, Name = "test3" },
                new() { Id = 4, Name = "test4" },
                new() { Id = 5, Name = "test5" }
            };

            // Act
            var specializationsFinded = _repository.GetAll().ToList();

            // Assert
            specializations.Should().BeEquivalentTo(specializationsFinded);
        }

        [Test]
        public void GetByIdMethod_WhenSelectSpecializationEntityRepositoryAdo_ThenSpecializationEntitySelected()
        {
            // Arrange
            const int id = 1;
            
            // Act
            var specialization = _repository.GetById(id);

            // Assert
            Assert.IsTrue(specialization.Id == id);
        }
    }
}
