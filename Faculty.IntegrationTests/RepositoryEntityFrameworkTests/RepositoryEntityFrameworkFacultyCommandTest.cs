using System;
using System.Linq;
using Faculty.DataAccessLayer;
using Faculty.DataAccessLayer.RepositoryEntityFramework;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Faculty.IntegrationTests.RepositoryEntityFrameworkTests
{
    [TestFixture]
    public class RepositoryEntityFrameworkFacultyCommandTest
    {
        private IRepository<DataAccessLayer.Models.Faculty> _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DbContext>().UseInMemoryDatabase(databaseName: "Test").Options;
            var contextEntity = new DatabaseContextEntityFramework(options);
            _repository = new BaseRepositoryEntityFramework<DataAccessLayer.Models.Faculty>(contextEntity);
        }

        [TestCase(1, 5, 1, 1, 1)]
        public void InsertMethod_WhenInsertCuratorEntityRepositoryEntityFramework_ThenCuratorEntityInserted(int id, int countYear, int groupId, int studentId, int curatorId)
        {
            // Arrange
            var faculty = new DataAccessLayer.Models.Faculty
            {
                Id = id,
                StartDateEducation = DateTime.Now, 
                CountYearEducation = countYear, 
                GroupId = groupId, 
                StudentId = studentId, 
                CuratorId = curatorId
            };

            // Act
            var countAdded = _repository.Insert(faculty);

            // Assert
            Assert.IsNotNull(countAdded > 0);
        }

        [TestCase(2, 5, 2, 2, 2)]
        public void UpdateMethod_WhenUpdateCuratorEntityRepositoryEntityFramework_ThenCuratorEntityUpdated(int id, int countYear, int groupId, int studentId, int curatorId)
        {
            // Arrange
            const int newCountYear = 4;
            var faculty = new DataAccessLayer.Models.Faculty
            {
                Id = id,
                StartDateEducation = DateTime.Now,
                CountYearEducation = countYear,
                GroupId = groupId,
                StudentId = studentId,
                CuratorId = curatorId
            };
            _repository.Insert(faculty);

            // Act
            faculty.CountYearEducation = newCountYear;
            var countChanged = _repository.Update(faculty);

            // Assert
            Assert.IsTrue(countChanged > 0);
        }

        [TestCase(3, 5, 1, 1, 1)]
        public void DeleteMethod_WhenDeleteCuratorEntityRepositoryEntityFramework_ThenCuratorEntityDeleted(int id, int countYear, int groupId, int studentId, int curatorId)
        {
            // Arrange
            var faculty = new DataAccessLayer.Models.Faculty
            {
                Id = id,
                StartDateEducation = DateTime.Now,
                CountYearEducation = countYear,
                GroupId = groupId,
                StudentId = studentId,
                CuratorId = curatorId
            };
            _repository.Insert(faculty);

            // Act
            var countDeleted = _repository.Delete(faculty);

            // Assert
            Assert.IsTrue(countDeleted > 0);
        }

        [TestCase(4, 5, 3, 3, 1)]
        public void GetAllMethod_WhenSelectCuratorsEntitiesRepositoryEntityFramework_ThenSpecializationsEntitiesSelected(int id, int countYear, int groupId, int studentId, int curatorId)
        {
            // Arrange
            var faculty = new DataAccessLayer.Models.Faculty
            {
                Id = id,
                StartDateEducation = DateTime.Now,
                CountYearEducation = countYear,
                GroupId = groupId,
                StudentId = studentId,
                CuratorId = curatorId
            };
            _repository.Insert(faculty);

            // Act
            var listResult = _repository.GetAll().ToList();

            // Assert
            Assert.IsTrue(listResult.Count > 0);
        }

        [TestCase(5, 5, 2, 2, 1)]
        public void GetByIdMethod_WhenSelectCuratorEntityRepositoryEntityFramework_ThenSpecializationEntitySelected(int id, int countYear, int groupId, int studentId, int curatorId)
        {
            // Arrange
            var faculty = new DataAccessLayer.Models.Faculty
            {
                Id = id,
                StartDateEducation = DateTime.Now,
                CountYearEducation = countYear,
                GroupId = groupId,
                StudentId = studentId,
                CuratorId = curatorId
            };
            _repository.Insert(faculty);

            // Act
            var result = _repository.GetById(id);

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
