using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Faculty.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Faculty.DataAccessLayer.Models;
using Microsoft.Extensions.Configuration;
using Faculty.DataAccessLayer.RepositoryAdo;
using Microsoft.Extensions.Configuration.Json;
using Faculty.DataAccessLayer.RepositoryEntityFramework;
using Faculty.DataAccessLayer.RepositoryAdo.RepositoryModels;

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
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(Environment.CurrentDirectory, "appsettings.json"))
                .Build();
            var connectionString = configuration["ConnectionStrings:DefaultConnection"];
            _contextAdo = new DatabaseContextAdo(connectionString);
            var options = new DbContextOptionsBuilder<DbContext>().UseInMemoryDatabase(databaseName: "Test").Options;
            _contextEntity = new DatabaseContextEntityFramework(options);
        }

        [Test]
        public void InsertMethod_WhenInsertStudentEntityRepositotyAdo_ThenStudentEntityInserted()
        {
            // Arrange
            IRepository<Student> repository = new RepositoryStudent(_contextAdo);
            var student = new Student { Surname = "Хороводоведов", Name = "Архыз", Doublename = "Иванович" };
            // Act
            repository.Insert(student);
            var studentInserted = repository.GetAll().FirstOrDefault(st => st.Surname == student.Surname && st.Name == student.Name && st.Doublename == student.Doublename);
            repository.Delete(studentInserted);
            // Assert
            Assert.IsNotNull(studentInserted);
        }

        [Test]
        public void InsertMethod_WhenInsertCuratorEntityRepositotyEntityFramework_ThenCuratorEntityInserted()
        {
            // Arrange
            IRepository<Curator> repository = new RepositoryEntityFrameworkImplementation<Curator>(_contextEntity);
            var curator = new Curator { Surname = "Хороводоведов", Name = "Архыз", Doublename = "Иванович", Phone = "+375-33-557-06-67" };
            // Act
            repository.Insert(curator);
            var curatorInserted = repository.GetAll().FirstOrDefault(c => c.Surname == curator.Surname && c.Name == curator.Name && c.Doublename == curator.Doublename && c.Phone == curator.Phone);
            // Assert
            Assert.IsNotNull(curatorInserted);
        }

        [Test]
        public void DeleteMethod_WhenDeleteStudentEntityRepositotyAdo_ThenStudentEntityDeleted()
        {
            // Arrange
            IRepository<Student> repository = new RepositoryStudent(_contextAdo);
            var student = new Student { Surname = "Иванчук", Name = "Ирина", Doublename = "Александровна" };
            repository.Insert(student);
            var studentInserted = repository.GetAll().FirstOrDefault(st => st.Surname == student.Surname && st.Name == student.Name && st.Doublename == student.Doublename);
            // Act
            repository.Delete(studentInserted);
            var studentDeleted = repository.GetAll().FirstOrDefault(st => studentInserted != null && st.Id == studentInserted.Id);
            // Assert
            Assert.IsNull(studentDeleted);
        }

        [Test]
        public void DeleteMethod_WhenDeleteCuratorEntityRepositotyEntityFramework_ThenCuratorEntityDeleted()
        {
            // Arrange
            IRepository<Curator> repository = new RepositoryEntityFrameworkImplementation<Curator>(_contextEntity);
            var curator = new Curator() { Surname = "Иванчук", Name = "Ирина", Doublename = "Александровна", Phone = "+375-33-557-06-67" };
            repository.Insert(curator);
            var curatorInserted = repository.GetAll().FirstOrDefault(c => c.Surname == curator.Surname && c.Name == curator.Name && c.Doublename == curator.Doublename && c.Phone == curator.Phone);
            // Act
            repository.Delete(curatorInserted);
            var curatorDeleted = repository.GetAll().FirstOrDefault(c => curatorInserted != null && c.Id == curatorInserted.Id);
            // Assert
            Assert.IsNull(curatorDeleted);
        }

        [Test]
        [TestCase("Сергеевна")]
        public void UpdateMethod_WhenUpdateStudentEntityRepositotyAdo_ThenStudentEntityUpdated(string changedDoublename)
        {
            // Arrange
            IRepository<Student> repository = new RepositoryStudent(_contextAdo);
            var student = new Student { Surname = "Малышева", Name = "Зинаида", Doublename = "Петровна" };
            // Act
            repository.Insert(student);
            var studentInserted = repository.GetAll().FirstOrDefault(st => st.Surname == student.Surname && st.Name == student.Name && st.Doublename == student.Doublename);
            studentInserted.Doublename = changedDoublename;
            repository.Update(studentInserted);
            var studentUpdated = repository.GetAll().FirstOrDefault(st => st.Id == studentInserted.Id);
            repository.Delete(studentUpdated);
            // Assert
            Assert.IsTrue(studentUpdated.Doublename == changedDoublename);
        }

        [Test]
        [TestCase("Сергеевна")]
        public void UpdateMethod_WhenUpdateCuratorEntityRepositotyEntityFramework_ThenCuratorEntityUpdated(string changedDoublename)
        {
            // Arrange
            IRepository<Curator> repository = new RepositoryEntityFrameworkImplementation<Curator>(_contextEntity);
            var curator = new Curator() { Id = 1, Surname = "Малышева", Name = "Зинаида", Doublename = "Петровна", Phone = "+375-33-557-06-67" };
            // Act
            repository.Insert(curator);
            curator.Doublename = changedDoublename;
            repository.Update(curator);
            var curatorUpdated = repository.GetAll().FirstOrDefault(c => c.Id == curator.Id);
            // Assert
            Assert.IsTrue(curatorUpdated.Doublename == changedDoublename);
        }

        [Test]
        [TestCase("Техник-прогрммист")]
        public void GetAllMethod_WhenSelectSpecializationsEntitiesRepositotyAdo_ThenSpecializationsEntitiesSelected(string name)
        {
            // Arrange
            const int countRecords = 1;
            IRepository<Specialization> repository = new RepositorySpecialization(_contextAdo);
            var specialization = new Specialization { Name = name };
            // Act
            repository.Insert(specialization);
            var specializationInserted = repository.GetAll().FirstOrDefault(sp => sp.Name == specialization.Name);
            var listResult = repository.GetAll().ToList();
            repository.Delete(specializationInserted);
            // Assert
            Assert.IsTrue(listResult.Count == countRecords);
        }

        [Test]
        [TestCase("Техник-прогрммист")]
        public void GetAllMethod_WhenSelectSpecializationsEntitiesRepositotyEntityFramework_ThenSpecializationsEntitiesSelected(string name)
        {
            // Arrange
            const int countRecords = 1;
            IRepository<Specialization> repository = new RepositoryEntityFrameworkImplementation<Specialization>(_contextEntity);
            var specialization = new Specialization { Name = name };
            // Act
            repository.Insert(specialization);
            var listResult = repository.GetAll().ToList();
            // Assert
            Assert.IsTrue(listResult.Count == countRecords);
        }
    }
}