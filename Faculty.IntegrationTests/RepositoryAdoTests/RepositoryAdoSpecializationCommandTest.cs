using System;
using System.IO;
using System.Linq;
using Faculty.DataAccessLayer;
using Faculty.DataAccessLayer.Models;
using Faculty.DataAccessLayer.RepositoryAdo;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace Faculty.IntegrationTests.RepositoryAdoTests
{
    [TestFixture]
    public class RepositoryAdoSpecializationCommandTest
    {
        private IRepository<Specialization> _repository;

        [SetUp]
        public void Setup()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile(Path.Combine(Environment.CurrentDirectory, "appsettings.json")).Build();
            var connectionString = configuration["ConnectionStrings:DefaultConnection"];
            var contextAdo = new DatabaseContextAdo(connectionString);
            _repository = new RepositoryAdoSpecialization(contextAdo);
        }

        [TestCase("Test1")]
        public void InsertMethod_WhenInsertStudentEntityRepositoryAdo_ThenStudentEntityInserted(string name)
        {
            // Arrange
            var specialization = new Specialization { Name = name };

            // Act
            var countAdded = _repository.Insert(specialization);

            // Assert
            Assert.IsTrue(countAdded > 0);
        }

        [TestCase("Test2")]
        public void UpdateMethod_WhenUpdateStudentEntityRepositoryAdo_ThenStudentEntityUpdated(string name)
        {
            // Arrange
            const string newName = "Test4";
            var specialization = new Specialization { Name = name };
            _repository.Insert(specialization);
            var specializationInserted = _repository.GetAll().FirstOrDefault(st => st.Name == name);

            // Act
            specializationInserted.Name = newName;
            var countChanged = _repository.Update(specializationInserted);

            // Assert
            Assert.IsTrue(countChanged > 0);
        }

        [TestCase("Test3")]
        public void DeleteMethod_WhenDeleteStudentEntityRepositoryAdo_ThenStudentEntityDeleted(string name)
        {
            // Arrange
            var specialization = new Specialization { Name = name };
            _repository.Insert(specialization);
            var specializationInserted = _repository.GetAll().FirstOrDefault(st => st.Name == name);

            // Act
            var countDeleted = _repository.Delete(specializationInserted);

            // Assert
            Assert.IsNotNull(countDeleted > 0);
        }

        [Test]
        public void GetAllMethod_WhenSelectSpecializationsEntitiesRepositoryAdo_ThenSpecializationsEntitiesSelected()
        {
            // Arrange
            //IRepository<Specialization> repository = new RepositoryAdoSpecialization(_contextAdo);
            
            // Act
            var listResult = _repository.GetAll().ToList();
            
            // Assert
            Assert.IsTrue(listResult.Count > 0);
        }

        [Test]
        public void GetByIdMethod_WhenSelectSpecializationEntityRepositoryAdo_ThenSpecializationEntitySelected()
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
