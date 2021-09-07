using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Faculty.DataAccessLayer;
using Faculty.DataAccessLayer.Models;
using Microsoft.Extensions.Configuration;
using Faculty.DataAccessLayer.RepositoryAdo;

namespace Faculty.IntegrationTests.RepositoryAdoTests
{
    [TestFixture]
    public class RepositoryAdoStudentCommandTest
    {
        private IRepository<Student> _repository;

        [SetUp]
        public void Setup()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile(Path.Combine(Environment.CurrentDirectory, "appsettings.json")).Build();
            var connectionString = configuration["ConnectionStrings:DefaultConnection"];
            var contextAdo = new DatabaseContextAdo(connectionString);
            _repository = new RepositoryAdoStudent(contextAdo);
        }

        [TestCase("Test1", null, "Test1")]
        public void InsertMethod_WhenInsertStudentEntityRepositoryAdo_ThenStudentEntityInserted(string surname, string name, string doublename)
        {
            // Arrange
            var student = new Student { Surname = surname, Name = name, Doublename = doublename };
            
            // Act
            var countAdded = _repository.Insert(student);
            
            // Assert
            Assert.IsTrue(countAdded > 0);
        }

        [TestCase("Test2", "Test2", null)]
        public void UpdateMethod_WhenUpdateStudentEntityRepositoryAdo_ThenStudentEntityUpdated(string surname, string name, string doublename)
        {
            // Arrange
            const string newName = "Test4";
            var student = new Student { Surname = surname, Name = name, Doublename = doublename };
            _repository.Insert(student);
            var studentInserted = _repository.GetAll().FirstOrDefault(st => st.Surname == surname && st.Name == name && st.Doublename == doublename);
            
            // Act
            studentInserted.Name = newName;
            var countChanged = _repository.Update(studentInserted);
            
            // Assert
            Assert.IsTrue(countChanged > 0);
        }

        [TestCase("Test3", "Test3", null)]
        public void DeleteMethod_WhenDeleteStudentEntityRepositoryAdo_ThenStudentEntityDeleted(string surname, string name, string doublename)
        {
            // Arrange
            var student = new Student { Surname = surname, Name = name, Doublename = doublename };
            _repository.Insert(student);
            var studentInserted = _repository.GetAll().FirstOrDefault(st => st.Surname == surname && st.Name == name && st.Doublename == doublename);
            
            // Act
            var countDeleted = _repository.Delete(studentInserted);
            
            // Assert
            Assert.IsNotNull(countDeleted > 0);
        }

        [Test]
        public void GetAllMethod_WhenSelectStudentsEntitiesRepositoryAdo_ThenSpecializationsEntitiesSelected()
        {
            // Arrange
            //IRepository<Specialization> repository = new RepositoryAdoSpecialization(_contextAdo);

            // Act
            var listResult = _repository.GetAll().ToList();

            // Assert
            Assert.IsTrue(listResult.Count > 0);
        }

        [Test]
        public void GetByIdMethod_WhenSelectStudentEntityRepositoryAdo_ThenSpecializationEntitySelected()
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
