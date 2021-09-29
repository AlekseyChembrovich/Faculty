using System;
using System.Linq;
using NUnit.Framework;
using FluentAssertions;
using System.Collections.Generic;
using Faculty.DataAccessLayer.Repository;
using Faculty.DataAccessLayer.Repository.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Faculty.IntegrationTests.RepositoryEntityFrameworkTests
{
    [TestFixture]
    public class RepositoryEntityFrameworkFacultyCommandTest
    {
        private IRepository<DataAccessLayer.Models.Faculty> _repository;
        private DatabaseContextEntityFramework _context;
        private DatabaseFiller _databaseFiller;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            _context = new DatabaseContextEntityFramework(options);
            _repository = new BaseRepositoryEntityFramework<DataAccessLayer.Models.Faculty>(_context);
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

        [TestCase(4, 2, 3, 3)]
        public void InsertMethod_WhenInsertFacultyEntity_ThenEntityInserted(int countYear, int studentId, int groupId, int curatorId)
        {
            // Arrange
            var facultyToInsert = new DataAccessLayer.Models.Faculty
            {
                StartDateEducation = new DateTime(2021, 09, 01), 
                CountYearEducation = countYear, 
                StudentId = studentId, 
                GroupId = groupId, 
                CuratorId = curatorId
            };
            var facultiesExisting = new List<DataAccessLayer.Models.Faculty>
            {
                new() { StartDateEducation = new DateTime(2021, 09, 01), CountYearEducation = 5, StudentId = 1, GroupId = 1, CuratorId = 1 },
                new() { StartDateEducation = new DateTime(2021, 09, 01), CountYearEducation = 4, StudentId = 2, GroupId = 1, CuratorId = 1 },
                new() { StartDateEducation = new DateTime(2021, 09, 01), CountYearEducation = 5, StudentId = 3, GroupId = 2, CuratorId = 2 },
                new() { StartDateEducation = new DateTime(2021, 09, 01), CountYearEducation = 4, StudentId = 2, GroupId = 3, CuratorId = 3 },
                new() { StartDateEducation = new DateTime(2021, 09, 01), CountYearEducation = 5, StudentId = 1, GroupId = 1, CuratorId = 1 }
            };

            // Act
            _repository.Insert(facultyToInsert);
            facultiesExisting.Add(facultyToInsert);
            var modifiedFacultiesFromDatabase = _repository.GetAll();

            // Assert
            facultiesExisting.Should().BeEquivalentTo(modifiedFacultiesFromDatabase, options => options.Excluding(c => c.Id).Excluding(c => c.Curator).Excluding(c => c.Student).Excluding(c => c.Group));
        }

        [Test]
        public void UpdateMethod_WhenUpdateFacultyEntity_ThenEntityUpdated()
        {
            // Arrange
            const int facultyExistingId = 1;
            const int countYearNew = 2;
            var facultiesModified = new List<DataAccessLayer.Models.Faculty>
            {
                new() { StartDateEducation = new DateTime(2021, 09, 01), CountYearEducation = countYearNew, StudentId = 1, GroupId = 1, CuratorId = 1 },
                new() { StartDateEducation = new DateTime(2021, 09, 01), CountYearEducation = 4, StudentId = 2, GroupId = 1, CuratorId = 1 },
                new() { StartDateEducation = new DateTime(2021, 09, 01), CountYearEducation = 5, StudentId = 3, GroupId = 2, CuratorId = 2 },
                new() { StartDateEducation = new DateTime(2021, 09, 01), CountYearEducation = 4, StudentId = 2, GroupId = 3, CuratorId = 3 },
                new() { StartDateEducation = new DateTime(2021, 09, 01), CountYearEducation = 5, StudentId = 1, GroupId = 1, CuratorId = 1 }
            };
            var facultyToChange = _repository.GetById(facultyExistingId);

            // Act
            facultyToChange.CountYearEducation = countYearNew;
            _repository.Update(facultyToChange);
            var modifiedFacultiesFromDatabase = _repository.GetAll();

            // Assert
            facultiesModified.Should().BeEquivalentTo(modifiedFacultiesFromDatabase, options => options.Excluding(c => c.Id).Excluding(c => c.Curator).Excluding(c => c.Student).Excluding(c => c.Group));
        }

        [Test]
        public void DeleteMethod_WhenDeleteFacultyEntity_ThenEntityDeleted()
        {
            // Arrange
            const int facultyExistingId = 2;
            var facultyToDelete = _repository.GetById(facultyExistingId);
            var facultiesWithoutDeletedEntity = new List<DataAccessLayer.Models.Faculty>
            {
                new() { StartDateEducation = new DateTime(2021, 09, 01), CountYearEducation = 5, StudentId = 1, GroupId = 1, CuratorId = 1 },
                new() { StartDateEducation = new DateTime(2021, 09, 01), CountYearEducation = 5, StudentId = 3, GroupId = 2, CuratorId = 2 },
                new() { StartDateEducation = new DateTime(2021, 09, 01), CountYearEducation = 4, StudentId = 2, GroupId = 3, CuratorId = 3 },
                new() { StartDateEducation = new DateTime(2021, 09, 01), CountYearEducation = 5, StudentId = 1, GroupId = 1, CuratorId = 1 }
            };

            // Act
            _repository.Delete(facultyToDelete);
            var modifiedFacultiesFromDatabase = _repository.GetAll().ToList();

            // Assert
            facultiesWithoutDeletedEntity.Should().BeEquivalentTo(modifiedFacultiesFromDatabase, options => options.Excluding(c => c.Id).Excluding(c => c.Curator).Excluding(c => c.Student).Excluding(c => c.Group));
        }

        [Test]
        public void GetAllMethod_WhenSelectFacultiesEntities_ThenEntitiesSelected()
        {
            // Arrange
            var facultiesExisting = new List<DataAccessLayer.Models.Faculty>
            {
                new() { StartDateEducation = new DateTime(2021, 09, 01), CountYearEducation = 5, StudentId = 1, GroupId = 1, CuratorId = 1 },
                new() { StartDateEducation = new DateTime(2021, 09, 01), CountYearEducation = 4, StudentId = 2, GroupId = 1, CuratorId = 1 },
                new() { StartDateEducation = new DateTime(2021, 09, 01), CountYearEducation = 5, StudentId = 3, GroupId = 2, CuratorId = 2 },
                new() { StartDateEducation = new DateTime(2021, 09, 01), CountYearEducation = 4, StudentId = 2, GroupId = 3, CuratorId = 3 },
                new() { StartDateEducation = new DateTime(2021, 09, 01), CountYearEducation = 5, StudentId = 1, GroupId = 1, CuratorId = 1 }
            };

            // Act
            var wholeFacultiesDataSet = _repository.GetAll().ToList();

            // Assert
            facultiesExisting.Should().BeEquivalentTo(wholeFacultiesDataSet, options => options.Excluding(c => c.Id).Excluding(c => c.Curator).Excluding(c => c.Student).Excluding(c => c.Group));
        }

        [Test]
        public void GetByIdMethod_WhenSelectFacultyEntity_ThenEntitySelected()
        {
            // Arrange
            const int facultyExistingId = 3;
            var facultyExisting = new DataAccessLayer.Models.Faculty
            {
                StartDateEducation = new DateTime(2021, 09, 01), 
                CountYearEducation = 5, 
                StudentId = 3, 
                GroupId = 2, 
                CuratorId = 2
            };

            // Act
            var facultyFounded = _repository.GetById(facultyExistingId);

            // Assert
            facultyExisting.Should().BeEquivalentTo(facultyFounded, options => options.Excluding(c => c.Id).Excluding(c => c.Curator).Excluding(c => c.Student).Excluding(c => c.Group));
        }
    }
}
