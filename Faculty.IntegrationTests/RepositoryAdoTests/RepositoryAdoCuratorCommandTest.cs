using System;
using System.IO;
using System.Linq;
using Faculty.DataAccessLayer;
using Faculty.DataAccessLayer.Models;
using Faculty.DataAccessLayer.RepositoryAdo;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace Faculty.IntegrationTests.RepositoryAdoTests
{
    [TestFixture]
    public class RepositoryAdoCuratorCommandTest
    {
        private IRepository<Curator> _repository;
        private DatabaseConfiguration _databaseConfiguration;

        [SetUp]
        public void Setup()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile(Path.Combine(Environment.CurrentDirectory, "appsettings.json")).Build();
            _databaseConfiguration = new DatabaseConfiguration(configuration);
            //_databaseConfiguration.DropTestDatabase();
            _databaseConfiguration.DeployTestDatabase();
            _repository = new RepositoryAdoCurator(_databaseConfiguration.ContextAdo);
        }

        [TestCase("Test4", "Test4", "Test4", "+375-29-557-06-67")]
        public void InsertMethod_WhenInsertCuratorEntityRepositoryAdo_ThenStudentEntityInserted(string surname, string name, string doublename, string phone)
        {
            // Arrange
            const int id = 4;
            var curator = new Curator { Id = id, Surname = surname, Name = name, Doublename = doublename, Phone = phone };

            // Act
            _repository.Insert(curator);
            var curatorFound = _repository.GetById(id);

            // Assert
            curator.Should().BeEquivalentTo(curatorFound);
        }

        [Test]
        public void UpdateMethod_WhenUpdateCuratorEntityRepositoryAdo_ThenStudentEntityUpdated()
        {
            // Arrange
            const string newName = "Test6";
            const int id = 1;
            var curator = _repository.GetById(id);

            // Act
            curator.Doublename = newName;
            _repository.Update(curator);
            var curatorFound = _repository.GetById(id);

            // Assert
            curator.Should().BeEquivalentTo(curatorFound);
        }

        [Test]
        public void DeleteMethod_WhenDeleteCuratorEntityRepositoryAdo_ThenStudentEntityDeleted()
        {
            // Arrange
            const int id = 2;
            var curatorInserted = _repository.GetById(id);

            // Act
            var countDeleted = _repository.Delete(curatorInserted);

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
            const int id = 3;
            //IRepository<Specialization> repository = new RepositoryAdoSpecialization(_contextAdo);

            // Act
            var result = _repository.GetById(id);

            // Assert
            result.Surname.Should().Be("Test3");
            result.Name.Should().Be("Test3");
            result.Doublename.Should().Be("Test3");
        }
    }
}
