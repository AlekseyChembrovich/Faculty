using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using FluentAssertions;
using System.Collections.Generic;
using Faculty.DataAccessLayer.Models;
using Faculty.DataAccessLayer.Repository;
using Microsoft.Extensions.Configuration;
using Faculty.DataAccessLayer.Repository.Ado;

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
            _databaseConfiguration.DeployTestDatabase();
            _repository = new RepositoryAdoGroup(_databaseConfiguration.ContextAdo);
        }

        [TearDown]
        public void TearDown()
        {
            _databaseConfiguration.DropTestDatabase();
        }

        [TestCase("test6", 1)]
        public void InsertMethod_WhenInsertGroupEntity_ThenEntityInserted(string name, int specializationId)
        {
            // Arrange
            var groupToInsert = new Group { Name = name, SpecializationId = specializationId };
            var groupsExisting = new List<Group>
            {
                new() { Name = "test1", SpecializationId = 1 },
                new() { Name = "test2", SpecializationId = 2 },
                new() { Name = "test3", SpecializationId = 1 },
                new() { Name = "test3", SpecializationId = 2 },
                new() { Name = "test3", SpecializationId = 3 }
            };

            // Act
            _repository.Insert(groupToInsert);
            groupsExisting.Add(groupToInsert);
            var modifiedGroupsFromDatabase = _repository.GetAll();

            // Assert
            groupsExisting.Should().BeEquivalentTo(modifiedGroupsFromDatabase, options => options.Excluding(c => c.Id).Excluding(c => c.Faculties).Excluding(c => c.Specialization));
        }

        [Test]
        public void UpdateMethod_WhenUpdateGroupEntity_ThenEntityUpdated()
        {
            // Arrange
            const int groupExistingId = 1;
            const string groupNewName = "test7";
            var groupsModified = new List<Group>
            {
                new() { Name = groupNewName, SpecializationId = 1 },
                new() { Name = "test2", SpecializationId = 2 },
                new() { Name = "test3", SpecializationId = 1 },
                new() { Name = "test3", SpecializationId = 2 },
                new() { Name = "test3", SpecializationId = 3 }
            };
            var groupToChange = _repository.GetById(groupExistingId);

            // Act
            groupToChange.Name = groupNewName;
            _repository.Update(groupToChange);
            var modifiedGroupsFromDatabase = _repository.GetAll();

            // Assert
            groupsModified.Should().BeEquivalentTo(modifiedGroupsFromDatabase, options => options.Excluding(c => c.Id).Excluding(c => c.Faculties).Excluding(c => c.Specialization));
        }

        [Test]
        public void DeleteMethod_WhenDeleteGroupEntity_ThenEntityDeleted()
        {
            // Arrange
            const int groupExistingId = 2;
            var groupToDelete = _repository.GetById(groupExistingId);
            var groupsWithoutDeletedEntity = new List<Group>
            {
                new() { Name = "test1", SpecializationId = 1 },
                new() { Name = "test3", SpecializationId = 1 },
                new() { Name = "test3", SpecializationId = 2 },
                new() { Name = "test3", SpecializationId = 3 }
            };

            // Act
            _repository.Delete(groupToDelete);
            var modifiedGroupsFromDatabase = _repository.GetAll().ToList();

            // Assert
            groupsWithoutDeletedEntity.Should().BeEquivalentTo(modifiedGroupsFromDatabase, options => options.Excluding(c => c.Id).Excluding(c => c.Faculties).Excluding(c => c.Specialization));
        }

        [Test]
        public void GetAllMethod_WhenSelectGroupsEntities_ThenEntitiesSelected()
        {
            // Arrange
            var groupsExisting = new List<Group>
            {
                new() { Name = "test1", SpecializationId = 1 },
                new() { Name = "test2", SpecializationId = 2 },
                new() { Name = "test3", SpecializationId = 1 },
                new() { Name = "test3", SpecializationId = 2 },
                new() { Name = "test3", SpecializationId = 3 }
            };

            // Act
            var wholeGroupsDataSet = _repository.GetAll().ToList();

            // Assert
            groupsExisting.Should().BeEquivalentTo(wholeGroupsDataSet, options => options.Excluding(c => c.Id).Excluding(c => c.Faculties).Excluding(c => c.Specialization));
        }

        [Test]
        public void GetByIdMethod_WhenSelectGroupEntity_ThenEntitySelected()
        {
            // Arrange
            const int groupExistingId = 3;
            var groupExisting = new Group { Name = "test3", SpecializationId = 1 };

            // Act
            var groupFounded = _repository.GetById(groupExistingId);

            // Assert
            groupExisting.Should().BeEquivalentTo(groupFounded, options => options.Excluding(c => c.Id).Excluding(c => c.Faculties).Excluding(c => c.Specialization));
        }
    }
}
