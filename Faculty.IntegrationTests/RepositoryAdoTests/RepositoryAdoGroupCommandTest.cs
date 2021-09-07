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
    public class RepositoryAdoGroupCommandTest
    {
        private IRepository<Group> _repository;

        [SetUp]
        public void Setup()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile(Path.Combine(Environment.CurrentDirectory, "appsettings.json")).Build();
            var connectionString = configuration["ConnectionStrings:DefaultConnection"];
            var contextAdo = new DatabaseContextAdo(connectionString);
            _repository = new RepositoryAdoGroup(contextAdo);
        }

        [TestCase("Test1", 1)]
        public void InsertMethod_WhenInsertStudentEntityRepositoryAdo_ThenStudentEntityInserted(string name, int specializationId)
        {
            // Arrange
            var group = new Group { Name = name, SpecializationId = specializationId };

            // Act
            var countAdded = _repository.Insert(group);

            // Assert
            Assert.IsTrue(countAdded > 0);
        }

        [TestCase("Test2", 1)]
        public void UpdateMethod_WhenUpdateStudentEntityRepositoryAdo_ThenStudentEntityUpdated(string name, int specializationId)
        {
            // Arrange
            const string newName = "Test4";
            var group = new Group { Name = name, SpecializationId = specializationId };
            _repository.Insert(group);
            var groupInserted = _repository.GetAll().FirstOrDefault(st => st.Name == name && st.SpecializationId == specializationId);

            // Act
            groupInserted.Name = newName;
            var countChanged = _repository.Update(groupInserted);

            // Assert
            Assert.IsTrue(countChanged > 0);
        }

        [TestCase("Test3", 1)]
        public void DeleteMethod_WhenDeleteStudentEntityRepositoryAdo_ThenStudentEntityDeleted(string name, int specializationId)
        {
            // Arrange
            var group = new Group { Name = name, SpecializationId = specializationId };
            _repository.Insert(group);
            var groupInserted = _repository.GetAll().FirstOrDefault(st => st.Name == name);

            // Act
            var countDeleted = _repository.Delete(groupInserted);

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
