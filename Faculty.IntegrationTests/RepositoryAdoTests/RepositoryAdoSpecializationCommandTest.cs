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
    public class RepositoryAdoSpecializationCommandTest
    {
        private IRepository<Specialization> _repository;
        private DatabaseConfiguration _databaseConfiguration;

        [SetUp]
        public void Setup()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile(Path.Combine(Environment.CurrentDirectory, "appsettings.json")).Build();
            _databaseConfiguration = new DatabaseConfiguration(configuration);
            _databaseConfiguration.DeployTestDatabase();
            _repository = new RepositoryAdoSpecialization(_databaseConfiguration.ContextAdo);
        }

        [TearDown]
        public void TearDown()
        {
            _databaseConfiguration.DropTestDatabase();
        }

        [TestCase("test6")]
        public void InsertMethod_WhenInsertSpecializationEntity_ThenEntityInserted(string name)
        {
            // Arrange
            var specializationToInsert = new Specialization { Name = name };
            var specializationsExisting = new List<Specialization>
            {
                new() { Name = "test1" },
                new() { Name = "test2" },
                new() { Name = "test3" },
                new() { Name = "test4" },
                new() { Name = "test5" }
            };

            // Act
            _repository.Insert(specializationToInsert);
            specializationsExisting.Add(specializationToInsert);
            var modifiedSpecializationsFromDatabase = _repository.GetAll();

            // Assert
            specializationsExisting.Should().BeEquivalentTo(modifiedSpecializationsFromDatabase, options => options.Excluding(c => c.Id).Excluding(c => c.Groups));
        }

        [Test]
        public void UpdateMethod_WhenUpdateSpecializationEntity_ThenEntityUpdated()
        {
            // Arrange
            const int specializationExistingId = 1;
            const string specializationNewName = "test7";
            var specializationsModified = new List<Specialization>
            {
                new() { Name = specializationNewName },
                new() { Name = "test2" },
                new() { Name = "test3" },
                new() { Name = "test4" },
                new() { Name = "test5" }
            };
            var specializationToChange = _repository.GetById(specializationExistingId);

            // Act
            specializationToChange.Name = specializationNewName;
            _repository.Update(specializationToChange);
            var modifiedSpecializationsFromDatabase = _repository.GetAll();

            // Assert
            specializationsModified.Should().BeEquivalentTo(modifiedSpecializationsFromDatabase, options => options.Excluding(c => c.Id).Excluding(c => c.Groups));
        }

        [Test]
        public void DeleteMethod_WhenDeleteSpecializationEntity_ThenEntityDeleted()
        {
            // Arrange
            const int specializationExistingId = 2;
            var specializationToDelete = _repository.GetById(specializationExistingId);
            var specializationsWithoutDeletedEntity = new List<Specialization>
            {
                new() { Name = "test1" },
                new() { Name = "test3" },
                new() { Name = "test4" },
                new() { Name = "test5" }
            };

            // Act
            _repository.Delete(specializationToDelete);
            var modifiedSpecializationsFromDatabase = _repository.GetAll().ToList();

            // Assert
            specializationsWithoutDeletedEntity.Should().BeEquivalentTo(modifiedSpecializationsFromDatabase, options => options.Excluding(c => c.Id).Excluding(c => c.Groups));
        }

        [Test]
        public void GetAllMethod_WhenSelectSpecializationsEntities_ThenEntitiesSelected()
        {
            // Arrange
            var specializationsExisting = new List<Specialization>
            {
                new() { Name = "test1" },
                new() { Name = "test2" },
                new() { Name = "test3" },
                new() { Name = "test4" },
                new() { Name = "test5" }
            };

            // Act
            var wholeSpecializationsDataSet = _repository.GetAll().ToList();

            // Assert
            specializationsExisting.Should().BeEquivalentTo(wholeSpecializationsDataSet, options => options.Excluding(c => c.Id).Excluding(c => c.Groups));
        }

        [Test]
        public void GetByIdMethod_WhenSelectSpecializationEntity_ThenEntitySelected()
        {
            // Arrange
            const int curatorExistingId = 3;
            var specializationExisting = new Student { Surname = "test3", Name = "test3", Doublename = "test3" };

            // Act
            var specializationFounded = _repository.GetById(curatorExistingId);

            // Assert
            specializationExisting.Should().BeEquivalentTo(specializationFounded, options => options.Excluding(c => c.Id).Excluding(c => c.Groups));
        }
    }
}
