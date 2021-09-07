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
        private DatabaseContextEntityFramework _contextEntity;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DbContext>().UseInMemoryDatabase(databaseName: "Test").Options;
            _contextEntity = new DatabaseContextEntityFramework(options);
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