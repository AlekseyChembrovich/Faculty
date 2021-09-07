using System;
using System.IO;
using System.Linq;
using Faculty.DataAccessLayer;
using Faculty.DataAccessLayer.Models;
using Faculty.DataAccessLayer.RepositoryAdo;
using Faculty.DataAccessLayer.RepositoryEntityFramework;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace Faculty.IntegrationTests.RepositoryAdoTests
{
    [TestFixture]
    public class RepositoryAdoSpecializationCommandTest
    {
        private DatabaseContextAdo _contextAdo;

        [SetUp]
        public void Setup()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile(Path.Combine(Environment.CurrentDirectory, "appsettings.json")).Build();
            //var connectionString = configuration["ConnectionStrings:DefaultConnection"];
            //_contextAdo = new DatabaseContextAdo(connectionString);

            var databaseConfiguration = new DatabaseConfiguration(configuration);

            //var options = new DbContextOptionsBuilder<DbContext>().UseInMemoryDatabase(databaseName: "Test").Options;
            //_contextEntity = new DatabaseContextEntityFramework(options);
        }

        [Test]
        public void Test()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile(Path.Combine(Environment.CurrentDirectory, "appsettings.json")).Build();
            var databaseConfiguration = new DatabaseConfiguration(configuration);
            databaseConfiguration.DeployTestDatabase();

            Assert.IsTrue(true);
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
    }
}