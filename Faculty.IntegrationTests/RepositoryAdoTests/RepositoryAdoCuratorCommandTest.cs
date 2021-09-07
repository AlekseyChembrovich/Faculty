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
    public class RepositoryAdoCuratorCommandTest
    {
        private IRepository<Curator> _repository;
        private DatabaseConfiguration _databaseConfiguration;

        [SetUp]
        public void Setup()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile(Path.Combine(Environment.CurrentDirectory, "appsettings.json")).Build();
            _databaseConfiguration = new DatabaseConfiguration(configuration);
            _databaseConfiguration.DeployTestDatabase();
            _repository = new RepositoryAdoCurator(_databaseConfiguration.ContextAdo);
        }

        /*[Test]
        public void Test1()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile(Path.Combine(Environment.CurrentDirectory, "appsettings.json")).Build();
            var databaseConfiguration = new DatabaseConfiguration(configuration);

            databaseConfiguration.DropTestDatabase();

            Assert.IsTrue(true);
        }*/

        [TestCase("Test4", "Test4", "Test4", "+375-29-557-06-67")]
        public void InsertMethod_WhenInsertCuratorEntityRepositoryAdo_ThenStudentEntityInserted(string surname, string name, string doublename, string phone)
        {
            // Arrange
            var curator = new Curator { Surname = surname, Name = name, Doublename = doublename };

            // Act
            var countAdded = _repository.Insert(curator);
            _databaseConfiguration.DropTestDatabase();

            // Assert
            Assert.IsTrue(countAdded > 0);
        }

        [TestCase("Test2", "Test2", null, "+375-29-557-06-67")]
        public void UpdateMethod_WhenUpdateCuratorEntityRepositoryAdo_ThenStudentEntityUpdated(string surname, string name, string doublename, string phone)
        {
            // Arrange
            const string newName = "Test6";
            var curator = new Curator { Surname = surname, Name = name, Doublename = doublename };
            _repository.Insert(curator);
            var curatorInserted = _repository.GetAll().FirstOrDefault(st => st.Surname == surname && st.Name == name && st.Doublename == doublename);

            // Act
            curatorInserted.Doublename = newName;
            var countChanged = _repository.Update(curatorInserted);

            // Assert
            Assert.IsTrue(countChanged > 0);
        }

        [TestCase("Test3", "Test3", null, "+375-29-557-06-67")]
        public void DeleteMethod_WhenDeleteCuratorEntityRepositoryAdo_ThenStudentEntityDeleted(string surname, string name, string doublename, string phone)
        {
            // Arrange
            var curator = new Curator { Surname = surname, Name = name, Doublename = doublename };
            _repository.Insert(curator);
            var curatorInserted = _repository.GetAll().FirstOrDefault(st => st.Surname == surname && st.Name == name && st.Doublename == doublename);

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
            const int idExistsModel = 1;
            //IRepository<Specialization> repository = new RepositoryAdoSpecialization(_contextAdo);

            // Act
            var result = _repository.GetById(idExistsModel);

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
