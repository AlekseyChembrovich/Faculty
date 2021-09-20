﻿using System;
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
    public class RepositoryAdoCuratorCommandTest
    {
        private IRepository<Curator> _repository;
        private DatabaseConfiguration _databaseConfiguration;

        [SetUp]
        public void Setup()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile(Path.Combine(Environment.CurrentDirectory, "appsettings.json")).Build();
            _databaseConfiguration = new DatabaseConfiguration(configuration);
            _databaseConfiguration.DeployTestDatabase();
            _repository = new RepositoryAdoCurator(_databaseConfiguration.ContextAdo);
        }

        [TearDown]
        public void TearDown()
        {
            _databaseConfiguration.DropTestDatabase();
        }

        [TestCase("test6", "test6", "test6", "+375-29-557-06-67")]
        public void InsertMethod_WhenInsertCuratorEntity_ThenEntityInserted(string surname, string name, string doublename, string phone)
        {
            // Arrange
            var curatorToInsert = new Curator { Surname = surname, Name = name, Doublename = doublename, Phone = phone };
            var curatorsExisting = new List<Curator>
            {
                new() { Surname = "test1", Name = "test1", Doublename = "test1", Phone = "+375-33-111-11-11" },
                new() { Surname = "test2", Name = "test2", Doublename = "test2", Phone = "+375-33-222-22-22" },
                new() { Surname = "test3", Name = "test3", Doublename = "test3", Phone = "+375-33-333-33-33" },
                new() { Surname = "test4", Name = "test4", Doublename = "test4", Phone = "+375-33-444-44-44" },
                new() { Surname = "test5", Name = "test5", Doublename = "test5", Phone = "+375-33-555-55-55" }
            };

            // Act
            _repository.Insert(curatorToInsert);
            curatorsExisting.Add(curatorToInsert);
            var modifiedCuratorsFromDatabase = _repository.GetAll();

            // Assert
            curatorsExisting.Should().BeEquivalentTo(modifiedCuratorsFromDatabase, options => options.Excluding(c => c.Id).Excluding(c => c.Faculties));
        }

        [Test]
        public void UpdateMethod_WhenUpdateCuratorEntity_ThenEntityUpdated()
        {
            // Arrange
            const int curatorExistingId = 1;
            const string curatorNewName = "test7";
            var curatorsModified = new List<Curator>
            {
                new() { Surname = "test1", Name = curatorNewName, Doublename = "test1", Phone = "+375-33-111-11-11" },
                new() { Surname = "test2", Name = "test2", Doublename = "test2", Phone = "+375-33-222-22-22" },
                new() { Surname = "test3", Name = "test3", Doublename = "test3", Phone = "+375-33-333-33-33" },
                new() { Surname = "test4", Name = "test4", Doublename = "test4", Phone = "+375-33-444-44-44" },
                new() { Surname = "test5", Name = "test5", Doublename = "test5", Phone = "+375-33-555-55-55" }
            };
            var curatorToChange = _repository.GetById(curatorExistingId);

            // Act
            curatorToChange.Name = curatorNewName;
            _repository.Update(curatorToChange);
            var modifiedCuratorsFromDatabase = _repository.GetAll();

            // Assert
            curatorsModified.Should().BeEquivalentTo(modifiedCuratorsFromDatabase, options => options.Excluding(c => c.Id).Excluding(c => c.Faculties));
        }

        [Test]
        public void DeleteMethod_WhenDeleteCuratorEntity_ThenEntityDeleted()
        {
            // Arrange
            const int curatorExistingId = 2;
            var curatorToDelete = _repository.GetById(curatorExistingId);
            var curatorsWithoutDeletedEntity = new List<Curator>
            {
                new() { Surname = "test1", Name = "test1", Doublename = "test1", Phone = "+375-33-111-11-11" },
                new() { Surname = "test3", Name = "test3", Doublename = "test3", Phone = "+375-33-333-33-33" },
                new() { Surname = "test4", Name = "test4", Doublename = "test4", Phone = "+375-33-444-44-44" },
                new() { Surname = "test5", Name = "test5", Doublename = "test5", Phone = "+375-33-555-55-55" }
            };

            // Act
            _repository.Delete(curatorToDelete);
            var modifiedCuratorsFromDatabase = _repository.GetAll().ToList();

            // Assert
            curatorsWithoutDeletedEntity.Should().BeEquivalentTo(modifiedCuratorsFromDatabase, options => options.Excluding(c => c.Id).Excluding(c => c.Faculties));
        }

        [Test]
        public void GetAllMethod_WhenSelectCuratorsEntities_ThenEntitiesSelected()
        {
            // Arrange
            var curatorsExisting = new List<Curator>
            {
                new() { Surname = "test1", Name = "test1", Doublename = "test1", Phone = "+375-33-111-11-11" },
                new() { Surname = "test2", Name = "test2", Doublename = "test2", Phone = "+375-33-222-22-22" },
                new() { Surname = "test3", Name = "test3", Doublename = "test3", Phone = "+375-33-333-33-33" },
                new() { Surname = "test4", Name = "test4", Doublename = "test4", Phone = "+375-33-444-44-44" },
                new() { Surname = "test5", Name = "test5", Doublename = "test5", Phone = "+375-33-555-55-55" }
            };

            // Act
            var wholeCuratorsDataSet = _repository.GetAll().ToList();

            // Assert
            curatorsExisting.Should().BeEquivalentTo(wholeCuratorsDataSet, options => options.Excluding(c => c.Id).Excluding(c => c.Faculties));
        }

        [Test]
        public void GetByIdMethod_WhenSelectCuratorEntity_ThenEntitySelected()
        {
            // Arrange
            const int curatorExistingId = 3;
            var curatorExisting = new Curator { Surname = "test3", Name = "test3", Doublename = "test3", Phone = "+375-33-333-33-33" };

            // Act
            var curatorFounded = _repository.GetById(curatorExistingId);

            // Assert
            curatorExisting.Should().BeEquivalentTo(curatorFounded, options => options.Excluding(c => c.Id).Excluding(c => c.Faculties));
        }
    }
}
