using System;
using System.Linq;
using NUnit.Framework;
using FluentAssertions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Faculty.DataAccessLayer.Models;
using Faculty.DataAccessLayer.Repository;
using Faculty.DataAccessLayer.Repository.EntityFramework;

namespace Faculty.IntegrationTests.RepositoryEntityFrameworkTests
{
    [TestFixture]
    public class RepositoryEntityFrameworkSpecializationCommandTest
    {
        private IRepository<Specialization> _repository;
        private DatabaseContextEntityFramework _context;
        private DatabaseFiller _databaseFiller;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            _context = new DatabaseContextEntityFramework(options);
            _repository = new BaseRepositoryEntityFramework<Specialization>(_context);
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
