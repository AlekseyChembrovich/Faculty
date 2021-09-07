using System.Linq;
using Faculty.DataAccessLayer;
using Faculty.DataAccessLayer.Models;
using Faculty.DataAccessLayer.RepositoryEntityFramework;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Faculty.IntegrationTests.RepositoryEntityFrameworkTests
{
    [TestFixture]
    public class RepositoryEntityFrameworkSpecializationCommandTest
    {
        private IRepository<Specialization> _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DbContext>().UseInMemoryDatabase(databaseName: "Test").Options;
            var contextEntity = new DatabaseContextEntityFramework(options);
            _repository = new BaseRepositoryEntityFramework<Specialization>(contextEntity);
        }

        [TestCase(1, "Test1")]
        public void InsertMethod_WhenInsertCuratorEntityRepositoryEntityFramework_ThenCuratorEntityInserted(int id, string name)
        {
            // Arrange
            var specialization = new Specialization { Id = id, Name = name };

            // Act
            var countAdded = _repository.Insert(specialization);

            // Assert
            Assert.IsNotNull(countAdded > 0);
        }

        [TestCase(2, "Test2")]
        public void UpdateMethod_WhenUpdateCuratorEntityRepositoryEntityFramework_ThenCuratorEntityUpdated(int id, string name)
        {
            // Arrange
            const string newName = "Test10";
            var specialization = new Specialization { Id = id, Name = name };
            _repository.Insert(specialization);

            // Act
            specialization.Name = newName;
            var countChanged = _repository.Update(specialization);

            // Assert
            Assert.IsTrue(countChanged > 0);
        }

        [TestCase(3, "Test3")]
        public void DeleteMethod_WhenDeleteCuratorEntityRepositoryEntityFramework_ThenCuratorEntityDeleted(int id, string name)
        {
            // Arrange
            var specialization = new Specialization { Id = id, Name = name };
            _repository.Insert(specialization);

            // Act
            var countDeleted = _repository.Delete(specialization);

            // Assert
            Assert.IsTrue(countDeleted > 0);
        }

        [TestCase(4, "Test4")]
        public void GetAllMethod_WhenSelectSpecializationsEntitiesRepositoryEntityFramework_ThenSpecializationsEntitiesSelected(int id, string name)
        {
            // Arrange
            var specialization = new Specialization { Id = id, Name = name };
            _repository.Insert(specialization);

            // Act
            var listResult = _repository.GetAll().ToList();

            // Assert
            Assert.IsTrue(listResult.Count > 0);
        }

        [TestCase(5, "Test5")]
        public void GetByIdMethod_WhenSelectSpecializationEntityRepositoryEntityFramework_ThenSpecializationEntitySelected(int id, string name)
        {
            // Arrange
            var specialization = new Specialization { Id = id, Name = name };
            _repository.Insert(specialization);

            // Act
            var result = _repository.GetById(id);

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
