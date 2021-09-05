using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Faculty.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Faculty.DataAccessLayer.Models;
using Microsoft.Extensions.Configuration;
using Faculty.DataAccessLayer.RepositoryAdo;
using Faculty.DataAccessLayer.RepositoryEntityFramework;
using Faculty.DataAccessLayer.RepositoryAdoModel;

namespace Faculty.IntegrationTests
{
    [TestFixture]
    public class RepositoryCommandTest
    {
        private DatabaseContextAdo _contextAdo;
        private DatabaseContextEntityFramework _contextEntity;

        [SetUp]
        public void Setup()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile(Path.Combine(Environment.CurrentDirectory, "appsettings.json")).Build();
            var connectionString = configuration["ConnectionStrings:DefaultConnection"];
            _contextAdo = new DatabaseContextAdo(connectionString);

            var options = new DbContextOptionsBuilder<DbContext>().UseInMemoryDatabase(databaseName: "Test").Options;
            _contextEntity = new DatabaseContextEntityFramework(options);
        }

        [TestCase("Test1", "Test1", "Test1")]
        public void InsertMethod_WhenInsertStudentEntityRepositoryAdo_ThenStudentEntityInserted(string surname, string name, string doublename)
        {
            // Arrange
            IRepository<Student> repository = new RepositoryAdoStudent(_contextAdo);
            var student = new Student { Surname = surname, Name = name, Doublename = doublename };
            // Act
            var countAdded = repository.Insert(student);
            // Assert
            Assert.IsTrue(countAdded > 0);
        }

        [TestCase(1, "Test1", "Test1", "Test1", "+375-33-557-06-67")]
        public void InsertMethod_WhenInsertCuratorEntityRepositoryEntityFramework_ThenCuratorEntityInserted(int id, string surname, string name, string doublename, string phone)
        {
            // Arrange
            IRepository<Curator> repository = new BaseRepositoryEntityFramework<Curator>(_contextEntity);
            var curator = new Curator { Id = id, Surname = surname, Name = name, Doublename = doublename, Phone = phone };
            // Act
            var countAdded = repository.Insert(curator);
            // Assert
            Assert.IsNotNull(countAdded > 0);
        }

        [TestCase("Test2", "Test2", "Test2")]
        public void UpdateMethod_WhenUpdateStudentEntityRepositoryAdo_ThenStudentEntityUpdated(string surname, string name, string doublename)
        {
            // Arrange
            const string newDoublename = "Test4";
            IRepository<Student> repository = new RepositoryAdoStudent(_contextAdo);
            var student = new Student { Surname = surname, Name = name, Doublename = doublename };
            repository.Insert(student);
            var studentInserted = repository.GetAll().FirstOrDefault(st => st.Surname == surname && st.Name == name && st.Doublename == doublename);
            // Act
            studentInserted.Doublename = newDoublename;
            var countChanged = repository.Update(studentInserted);
            // Assert
            Assert.IsTrue(countChanged > 0);
        }

        [TestCase(2, "Test2", "Test2", "Test2", "+375-33-557-06-67")]
        public void UpdateMethod_WhenUpdateCuratorEntityRepositoryEntityFramework_ThenCuratorEntityUpdated(int id, string surname, string name, string doublename, string phone)
        {
            // Arrange
            const string newDoublename = "Test4";
            IRepository<Curator> repository = new BaseRepositoryEntityFramework<Curator>(_contextEntity);
            var curator = new Curator { Id = id, Surname = surname, Name = name, Doublename = doublename, Phone = phone };
            repository.Insert(curator);
            // Act
            curator.Doublename = newDoublename;
            var countChanged = repository.Update(curator);
            // Assert
            Assert.IsTrue(countChanged > 0);
        }

        [TestCase("Test3", "Test3", "Test3")]
        public void DeleteMethod_WhenDeleteStudentEntityRepositoryAdo_ThenStudentEntityDeleted(string surname, string name, string doublename)
        {
            // Arrange
            IRepository<Student> repository = new RepositoryAdoStudent(_contextAdo);
            var student = new Student { Surname = surname, Name = name, Doublename = doublename };
            repository.Insert(student);
            var studentInserted = repository.GetAll().FirstOrDefault(st => st.Surname == surname && st.Name == name && st.Doublename == doublename);
            // Act
            var countDeleted = repository.Delete(studentInserted);
            // Assert
            Assert.IsTrue(countDeleted > 0);
        }

        [TestCase(3, "Test3", "Test3", "Test3", "+375-33-557-06-67")]
        public void DeleteMethod_WhenDeleteCuratorEntityRepositoryEntityFramework_ThenCuratorEntityDeleted(int id, string surname, string name, string doublename, string phone)
        {
            // Arrange
            IRepository<Curator> repository = new BaseRepositoryEntityFramework<Curator>(_contextEntity);
            var curator = new Curator { Id = id, Surname = surname, Name = name, Doublename = doublename, Phone = phone };
            repository.Insert(curator);
            // Act
            var countDeleted = repository.Delete(curator);
            // Assert
            Assert.IsTrue(countDeleted > 0);
        }

        [Test]
        public void GetAllMethod_WhenSelectSpecializationsEntitiesRepositoryAdo_ThenSpecializationsEntitiesSelected()
        {
            // Arrange
            IRepository<Specialization> repository = new RepositoryAdoSpecialization(_contextAdo);
            // Act
            var listResult = repository.GetAll().ToList();
            // Assert
            Assert.IsTrue(listResult.Count > 0);
        }

        [TestCase(1, "Test name")]
        public void GetAllMethod_WhenSelectSpecializationsEntitiesRepositoryEntityFramework_ThenSpecializationsEntitiesSelected(int id, string name)
        {
            // Arrange
            IRepository<Specialization> repository = new BaseRepositoryEntityFramework<Specialization>(_contextEntity);
            var specialization = new Specialization { Id = id, Name = name };
            // Act
            repository.Insert(specialization);
            var listResult = repository.GetAll().ToList();
            // Assert
            Assert.IsTrue(listResult.Count > 0);
        }

        [Test]
        public void GetByIdMethod_WhenSelectSpecializationEntityRepositoryAdo_ThenSpecializationEntitySelected()
        {
            // Arrange
            const int idExistsModel = 12;
            IRepository<Specialization> repository = new RepositoryAdoSpecialization(_contextAdo);
            // Act
            var result = repository.GetById(idExistsModel);
            // Assert
            Assert.IsNotNull(result);
        }

        [TestCase(2, "Test name")]
        public void GetByIdMethod_WhenSelectSpecializationEntityRepositoryEntityFramework_ThenSpecializationEntitySelected(int id, string name)
        {
            // Arrange
            IRepository<Specialization> repository = new BaseRepositoryEntityFramework<Specialization>(_contextEntity);
            var specialization = new Specialization { Id = id, Name = name };
            // Act
            repository.Insert(specialization);
            var result = repository.GetById(id);
            // Assert
            Assert.IsNotNull(result);
        }
    }
}