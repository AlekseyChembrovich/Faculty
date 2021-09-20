using System;
using System.Linq;
using NUnit.Framework;
using FluentAssertions;
using Faculty.DataAccessLayer;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Faculty.DataAccessLayer.Models;
using Faculty.DataAccessLayer.RepositoryEntityFramework;

namespace Faculty.IntegrationTests.RepositoryEntityFrameworkTests
{
    [TestFixture]
    public class RepositoryEntityFrameworkGroupCommandTest
    {
        private IRepository<Group> _repository;
        private DatabaseContextEntityFramework _context;
        private DatabaseFiller _databaseFiller;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            _context = new DatabaseContextEntityFramework(options);
            _repository = new BaseRepositoryEntityFramework<Group>(_context);
            _databaseFiller = new DatabaseFiller(_context);
            _databaseFiller.Fill();
        }

        [TearDown]
        public void TearDown()
        {
            _databaseFiller.Clear();
            _context.Database.EnsureDeleted();
            _context.Dispose();
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
                new() { Name = "test3", SpecializationId = 3 },
                new() { Name = "test4", SpecializationId = 4 },
                new() { Name = "test5", SpecializationId = 5 }
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
                new() { Name = "test3", SpecializationId = 3 },
                new() { Name = "test4", SpecializationId = 4 },
                new() { Name = "test5", SpecializationId = 5 }
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
                new() { Name = "test3", SpecializationId = 3 },
                new() { Name = "test4", SpecializationId = 4 },
                new() { Name = "test5", SpecializationId = 5 }
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
                new() { Name = "test3", SpecializationId = 3 },
                new() { Name = "test4", SpecializationId = 4 },
                new() { Name = "test5", SpecializationId = 5 }
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
            var groupExisting = new Group { Name = "test3", SpecializationId = 3 };

            // Act
            var groupFounded = _repository.GetById(groupExistingId);

            // Assert
            groupExisting.Should().BeEquivalentTo(groupFounded, options => options.Excluding(c => c.Id).Excluding(c => c.Faculties).Excluding(c => c.Specialization));
        }
    }
}
