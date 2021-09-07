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
        private DatabaseContextAdo _contextAdo;

        [SetUp]
        public void Setup()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile(Path.Combine(Environment.CurrentDirectory, "appsettings.json")).Build();
            var connectionString = configuration["ConnectionStrings:DefaultConnection"];
            _contextAdo = new DatabaseContextAdo(connectionString);
        }

        [TestCase("Test1", null, "Test1")]
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

        [TestCase("Test2", "Test2", null)]
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

        [TestCase("Test3", "Test3", null)]
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
            Assert.IsNotNull(studentInserted);
        }
    }
}
