using Faculty.DataAccessLayer;
using Faculty.DataAccessLayer.Models;
using Faculty.DataAccessLayer.RepositoryEntityFramework;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Faculty.IntegrationTests.RepositoryEntityFrameworkTests
{
    [TestFixture]
    public class RepositoryEntityFrameworkCuratorCommandTest
    {
        private DatabaseContextEntityFramework _contextEntity;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DbContext>().UseInMemoryDatabase(databaseName: "Test").Options;
            _contextEntity = new DatabaseContextEntityFramework(options);
        }

        [TestCase(1, "Test1", "Test1", "Test1", null)]
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

        [TestCase(2, "Test2", null, "Test2", "+375-33-557-06-67")]
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

        [TestCase(3, null, "Test3", "Test3", "+375-33-557-06-67")]
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
    }
}