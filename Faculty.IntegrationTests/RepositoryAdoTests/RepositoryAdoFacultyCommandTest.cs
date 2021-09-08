using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Faculty.DataAccessLayer;
using Faculty.DataAccessLayer.RepositoryAdo;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace Faculty.IntegrationTests.RepositoryAdoTests
{
    [TestFixture]
    public class RepositoryAdoFacultyCommandTest
    {
        private IRepository<DataAccessLayer.Models.Faculty> _repository;
        private DatabaseConfiguration _databaseConfiguration;

        [SetUp]
        public void Setup()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile(Path.Combine(Environment.CurrentDirectory, "appsettings.json")).Build();
            _databaseConfiguration = new DatabaseConfiguration(configuration);
            //_databaseConfiguration.DropTestDatabase();
            _databaseConfiguration.DeployTestDatabase();
            _repository = new RepositoryAdoFaculty(_databaseConfiguration.ContextAdo);
        }

        [TestCase(5, 1, 1, 1)]
        public void InsertMethod_WhenInsertCuratorEntityRepositoryAdo_ThenStudentEntityInserted(int countYear, int groupId, int studentId, int curatorId)
        {
            // Arrange
            const int id = 6;
            var faculty = new DataAccessLayer.Models.Faculty
            {
                Id = id,
                StartDateEducation = new DateTime(2000, 09, 01),
                CountYearEducation = countYear,
                GroupId = groupId,
                StudentId = studentId,
                CuratorId = curatorId
            };

            // Act
            _repository.Insert(faculty);
            var facultyInserted = _repository.GetById(id);

            // Assert
            faculty.Should().BeEquivalentTo(facultyInserted);
        }

        [Test]
        public void UpdateMethod_WhenUpdateCuratorEntityRepositoryAdo_ThenStudentEntityUpdated()
        {
            // Arrange
            const int id = 1;
            const int newCountYear = 2;
            var faculty = _repository.GetById(id);

            // Act
            faculty.CountYearEducation = newCountYear;
            _repository.Update(faculty);
            var facultyChanged = _repository.GetById(id);

            // Assert
            faculty.Should().BeEquivalentTo(facultyChanged);
        }

        [Test]
        public void DeleteMethod_WhenDeleteCuratorEntityRepositoryAdo_ThenStudentEntityDeleted()
        {
            // Arrange
            const int id = 2;
            var faculty = _repository.GetById(id);

            // Act
            _repository.Delete(faculty);
            var facultyDeleted = _repository.GetById(id);

            // Assert
            Assert.IsNull(facultyDeleted);
        }

        [Test]
        public void GetAllMethod_WhenSelectCuratorsEntitiesRepositoryAdo_ThenSpecializationsEntitiesSelected()
        {
            // Arrange
            //IRepository<Specialization> repository = new RepositoryAdoSpecialization(_contextAdo);

            // Act
            var listResult = _repository.GetAll().ToList();

            // Assert
            Assert.IsTrue(listResult.Count > 0);
        }

        [Test]
        public void GetByIdMethod_WhenSelectCuratorEntityRepositoryAdo_ThenSpecializationEntitySelected()
        {
            // Arrange
            const int id = 3;

            // Act
            var facultyFinded = _repository.GetById(id);

            // Assert
            Assert.IsTrue(facultyFinded.Id == id);
        }
    }
}
