using System;
using System.IO;
using System.Linq;
using Faculty.DataAccessLayer;
using Faculty.DataAccessLayer.RepositoryAdo;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace Faculty.IntegrationTests.RepositoryAdoTests
{
    [TestFixture]
    public class RepositoryAdoFacultyCommandTest
    {
        private IRepository<DataAccessLayer.Models.Faculty> _repository;

        [SetUp]
        public void Setup()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile(Path.Combine(Environment.CurrentDirectory, "appsettings.json")).Build();
            var connectionString = configuration["ConnectionStrings:DefaultConnection"];
            var contextAdo = new DatabaseContextAdo(connectionString);
            _repository = new RepositoryAdoFaculty(contextAdo);
        }

        [TestCase(5, 1, 1, 1)]
        public void InsertMethod_WhenInsertCuratorEntityRepositoryAdo_ThenStudentEntityInserted(int countYear, int groupId, int studentId, int curatorId)
        {
            // Arrange
            var faculty = new DataAccessLayer.Models.Faculty
            {
                StartDateEducation = DateTime.Now,
                CountYearEducation = countYear,
                GroupId = groupId,
                StudentId = studentId,
                CuratorId = curatorId
            };

            // Act
            var countAdded = _repository.Insert(faculty);

            // Assert
            Assert.IsTrue(countAdded > 0);
        }

        [TestCase(4, 2, 2, 2)]
        public void UpdateMethod_WhenUpdateCuratorEntityRepositoryAdo_ThenStudentEntityUpdated(int countYear, int groupId, int studentId, int curatorId)
        {
            // Arrange
            const int newCountYear = 2;
            var faculty = new DataAccessLayer.Models.Faculty
            {
                StartDateEducation = DateTime.Now,
                CountYearEducation = countYear,
                GroupId = groupId,
                StudentId = studentId,
                CuratorId = curatorId
            };
            _repository.Insert(faculty);
            var facultyInserted = _repository.GetAll().FirstOrDefault(st => st.StudentId == studentId && st.GroupId == groupId && 
                                                                            st.CuratorId == curatorId && st.CountYearEducation == countYear);

            // Act
            facultyInserted.CountYearEducation = newCountYear;
            var countChanged = _repository.Update(facultyInserted);

            // Assert
            Assert.IsTrue(countChanged > 0);
        }

        [TestCase(5, 3, 3, 3)]
        public void DeleteMethod_WhenDeleteCuratorEntityRepositoryAdo_ThenStudentEntityDeleted(int countYear, int groupId, int studentId, int curatorId)
        {
            // Arrange
            // Arrange
            var faculty = new DataAccessLayer.Models.Faculty
            {
                StartDateEducation = DateTime.Now,
                CountYearEducation = countYear,
                GroupId = groupId,
                StudentId = studentId,
                CuratorId = curatorId
            };
            _repository.Insert(faculty);
            var facultyInserted = _repository.GetAll().FirstOrDefault(st => st.StudentId == studentId && st.GroupId == groupId &&
                                                                            st.CuratorId == curatorId && st.CountYearEducation == countYear);

            // Act
            var countDeleted = _repository.Delete(facultyInserted);

            // Assert
            Assert.IsNotNull(countDeleted > 0);
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
            const int idExistsModel = 1;
            //IRepository<Specialization> repository = new RepositoryAdoSpecialization(_contextAdo);

            // Act
            var result = _repository.GetById(idExistsModel);

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
