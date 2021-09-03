using System.Linq;
using NUnit.Framework;
using System.Configuration;
using Faculty.DataAccessLayer;
using System.Collections.Generic;
using Faculty.DataAccessLayer.Models;
using Faculty.DataAccessLayer.RepositoryAdo;
using Faculty.DataAccessLayer.RepositoryEntityFramework;
using Faculty.DataAccessLayer.RepositoryAdo.RepositoryModels;

namespace Faculty.IntegrationTests
{
    public class RepositoryCommandTest
    {
        private DatabaseContextAdo _contextAdo;
        private DatabaseContextEntityFramework _contextEntity;

        [SetUp]
        public void Setup()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ConnectionStr"]?.ConnectionString;
            _contextAdo = new DatabaseContextAdo(connectionString);
            _contextEntity = new DatabaseContextEntityFramework();
        }

        [Test]
        public void InsertMethod_WhenInsertStudentEntityRepositotyAdo_ThenStudentEntityInserted()
        {
            // Arrange
            IRepository<Student> repository = new RepositoryStudent(_contextAdo);
            var student = new Student { Surname = "�������������", Name = "�����", Doublename = "��������" };
            // Act
            repository.Insert(student);
            var studentInserted = repository.GetAll().FirstOrDefault(st => st.Surname == student.Surname &&
                                                                           st.Name == student.Name &&
                                                                           st.Doublename == student.Doublename);
            repository.Delete(studentInserted);
            // Assert
            Assert.IsNotNull(studentInserted);
        }

        [Test]
        public void InsertMethod_WhenInsertCuratorEntityRepositotyEntityFramework_ThenCuratorEntityInserted()
        {
            // Arrange
            IRepository<Curator> repository = new RepositoryEntityFrameworkImplementation<Curator>(_contextEntity);
            var curator = new Curator { Surname = "�������������", Name = "�����", Doublename = "��������", Phone = "+375-33-557-06-67" };
            // Act
            repository.Insert(curator);
            var curatorInserted = repository.GetAll().FirstOrDefault(c => c.Surname == curator.Surname &&
                                                                           c.Name == curator.Name &&
                                                                           c.Doublename == curator.Doublename &&
                                                                           c.Phone == curator.Phone);
            repository.Delete(curatorInserted);
            // Assert
            Assert.IsNotNull(curatorInserted);
        }

        [Test]
        public void DeleteMethod_WhenDeleteStudentEntityRepositotyAdo_ThenStudentEntityDeleted()
        {
            // Arrange
            IRepository<Student> repository = new RepositoryStudent(_contextAdo);
            var student = new Student { Surname = "�������", Name = "�����", Doublename = "�������������" };
            repository.Insert(student);
            var studentInserted = repository.GetAll().FirstOrDefault(st => st.Surname == student.Surname &&
                                                                           st.Name == student.Name &&
                                                                           st.Doublename == student.Doublename);
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
            var curator = new Curator() { Surname = "�������", Name = "�����", Doublename = "�������������", Phone = "+375-33-557-06-67" };
            repository.Insert(curator);
            var curatorInserted = repository.GetAll().FirstOrDefault(c => c.Surname == curator.Surname &&
                                                                           c.Name == curator.Name &&
                                                                           c.Doublename == curator.Doublename &&
                                                                           c.Phone == curator.Phone);
            // Act
            repository.Delete(curator);
            var curatorDeleted = repository.GetAll().FirstOrDefault(c => curatorInserted != null && c.Id == curatorInserted.Id);
            // Assert
            Assert.IsNull(curatorDeleted);
        }

        [Test]
        public void UpdateMethod_WhenUpdateStudentEntityRepositotyAdo_ThenStudentEntityUpdated()
        {
            // Arrange
            IRepository<Student> repository = new RepositoryStudent(_contextAdo);
            var student = new Student { Surname = "��������", Name = "�������", Doublename = "��������" };
            // Act
            repository.Insert(student);
            var studentInserted = repository.GetAll().FirstOrDefault(st => st.Surname == student.Surname &&
                                                                           st.Name == student.Name &&
                                                                           st.Doublename == student.Doublename);
            studentInserted.Doublename = "���������";
            repository.Update(studentInserted);
            var studentUpdated = repository.GetAll().FirstOrDefault(st => st.Id == studentInserted.Id);
            repository.Delete(studentUpdated);
            // Assert
            Assert.IsTrue(studentUpdated.Doublename == "���������");
        }

        [Test]
        public void UpdateMethod_WhenUpdateCuratorEntityRepositotyEntityFramework_ThenCuratorEntityUpdated()
        {
            // Arrange
            IRepository<Curator> repository = new RepositoryEntityFrameworkImplementation<Curator>(_contextEntity);
            var curator = new Curator() { Surname = "��������", Name = "�������", Doublename = "��������", Phone = "+375-33-557-06-67" };
            // Act
            repository.Insert(curator);
            var curatorInserted = repository.GetAll().FirstOrDefault(c => c.Surname == curator.Surname &&
                                                                          c.Name == curator.Name &&
                                                                          c.Doublename == curator.Doublename &&
                                                                          c.Phone == curator.Phone);
            curatorInserted.Doublename = "���������";
            repository.Update(curatorInserted);
            var curatorUpdated = repository.GetAll().FirstOrDefault(c => c.Id == curatorInserted.Id);
            repository.Delete(curatorUpdated);
            // Assert
            Assert.IsTrue(curatorUpdated.Doublename == "���������");
        }

        [Test]
        public void GetAllMethod_WhenSelectSpecializationsEntitiesRepositotyAdo_ThenSpecializationsEntitiesSelected()
        {
            // Arrange
            IRepository<Specialization> repository = new RepositorySpecialization(_contextAdo);
            var listModel = new List<Specialization> { new Specialization { Name = "������-����������" }, new Specialization { Name = "���������" }, new Specialization { Name = "���������" } };
            // Act
            foreach (var model in listModel)
                repository.Insert(model);
            var listResult = repository.GetAll().ToList();
            foreach (var model in listResult)
                repository.Delete(model);
            // Assert
            Assert.IsTrue(listResult.Count == 3);
        }

        [Test]
        public void GetAllMethod_WhenSelectSpecializationsEntitiesRepositotyEntityFramework_ThenSpecializationsEntitiesSelected()
        {
            // Arrange
            IRepository<Specialization> repository = new RepositoryEntityFrameworkImplementation<Specialization>(_contextEntity);
            var listModel = new List<Specialization> { new Specialization { Name = "������-����������" }, new Specialization { Name = "���������" }, new Specialization { Name = "���������" } };
            // Act
            foreach (var model in listModel)
                repository.Insert(model);
            var listResult = repository.GetAll().ToList();
            foreach (var model in listResult)
                repository.Delete(model);
            // Assert
            Assert.IsTrue(listResult.Count == 3);
        }
    }
}