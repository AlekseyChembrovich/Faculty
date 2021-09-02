using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Faculty.DataAccessLayer;
using Faculty.DataAccessLayer.Models;
using Faculty.DataAccessLayer.RepositoryAdo;
using Faculty.DataAccessLayer.RepositoryEntityFramework;
using Faculty.DataAccessLayer.RepositoryAdo.RepositoryModels;

namespace Faculty.IntegrationTests
{
    public class Tests
    {
        private DatabaseContextAdo _contextAdo;
        private DatabaseContextEntityFramework _contextEntity;

        [SetUp]
        public void Setup()
        {
            _contextAdo =
                new DatabaseContextAdo(
                    @"Data Source=DESKTOP-ALEKSEY\SQLEXPRESS;Initial Catalog=MbTask;Integrated Security=True");
            _contextEntity = new DatabaseContextEntityFramework();
        }

        [Test]
        public void CanInsertAdoImplementation()
        {
            // Arrange
            IRepository<Student> repository = new RepositoryStudent(_contextAdo);
            var student = new Student
            {
                Surname = "Хороводоведов",
                Name = "Архыз",
                Doublename = "Иванович"
            };
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
        public void CanInsertEntityFrameworkImplementation()
        {
            // Arrange
            IRepository<Curator> repository = new RepositoryEntityFrameworkImplementation<Curator>(_contextEntity);
            var curator = new Curator()
            {
                Surname = "Хороводоведов",
                Name = "Архыз",
                Doublename = "Иванович",
                Phone = "+375-33-557-06-67"
            };
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
        public void CanDeleteAdoImplementation()
        {
            // Arrange
            IRepository<Student> repository = new RepositoryStudent(_contextAdo);
            var student = new Student
            {
                Surname = "Иванчук",
                Name = "Ирина",
                Doublename = "Александровна"
            };
            repository.Insert(student);
            var studentInserted = repository.GetAll().FirstOrDefault(st => st.Surname == student.Surname &&
                                                                           st.Name == student.Name &&
                                                                           st.Doublename == student.Doublename);
            // Act
            repository.Delete(studentInserted);
            var studentDeleted = repository.GetAll().FirstOrDefault(st => studentInserted != null &&
                                                                          st.Id == studentInserted.Id);
            // Assert
            Assert.IsNull(studentDeleted);
        }

        [Test]
        public void CanDeleteEntityFrameworkImplementation()
        {
            // Arrange
            IRepository<Curator> repository = new RepositoryEntityFrameworkImplementation<Curator>(_contextEntity);
            var curator = new Curator()
            {
                Surname = "Иванчук",
                Name = "Ирина",
                Doublename = "Александровна",
                Phone = "+375-33-557-06-67"
            };
            repository.Insert(curator);
            var curatorInserted = repository.GetAll().FirstOrDefault(c => c.Surname == curator.Surname &&
                                                                           c.Name == curator.Name &&
                                                                           c.Doublename == curator.Doublename &&
                                                                           c.Phone == curator.Phone);
            // Act
            repository.Delete(curator);
            var curatorDeleted = repository.GetAll().FirstOrDefault(c => curatorInserted != null &&
                                                                         c.Id == curatorInserted.Id);
            // Assert
            Assert.IsNull(curatorDeleted);
        }

        [Test]
        public void CanUpdateAdoImplementation()
        {
            // Arrange
            IRepository<Student> repository = new RepositoryStudent(_contextAdo);
            var student = new Student
            {
                Surname = "Малышева",
                Name = "Зинаида",
                Doublename = "Петровна"
            };
            // Act
            repository.Insert(student);
            var studentInserted = repository.GetAll().FirstOrDefault(st => st.Surname == student.Surname &&
                                                                           st.Name == student.Name &&
                                                                           st.Doublename == student.Doublename);
            studentInserted.Doublename = "Сергеевна";
            repository.Update(studentInserted);
            var studentUpdated = repository.GetAll().FirstOrDefault(st => st.Id == studentInserted.Id);
            repository.Delete(studentUpdated);
            // Assert
            Assert.IsTrue(studentUpdated.Doublename == "Сергеевна");
        }

        [Test]
        public void CanUpdateEntityFrameworkImplementation()
        {
            // Arrange
            IRepository<Curator> repository = new RepositoryEntityFrameworkImplementation<Curator>(_contextEntity);
            var curator = new Curator()
            {
                Surname = "Малышева",
                Name = "Зинаида",
                Doublename = "Петровна",
                Phone = "+375-33-557-06-67"
            };
            // Act
            repository.Insert(curator);
            var curatorInserted = repository.GetAll().FirstOrDefault(c => c.Surname == curator.Surname &&
                                                                          c.Name == curator.Name &&
                                                                          c.Doublename == curator.Doublename &&
                                                                          c.Phone == curator.Phone);
            curatorInserted.Doublename = "Сергеевна";
            repository.Update(curatorInserted);
            var curatorUpdated = repository.GetAll().FirstOrDefault(c => c.Id == curatorInserted.Id);
            repository.Delete(curatorUpdated);
            // Assert
            Assert.IsTrue(curatorUpdated.Doublename == "Сергеевна");
        }







        [Test]
        public void CanSelectAllAdoImplementation()
        {
            // Arrange
            IRepository<Specialization> repository = new RepositorySpecialization(_contextAdo);
            var listModel = new List<Specialization>
            {
                new Specialization { Name = "Техник-прогрммист" },
                new Specialization { Name = "Бухгалтер" },
                new Specialization { Name = "Экономист" }
            };
            // Act
            foreach (var model in listModel)
            {
                repository.Insert(model);
            }
            var listResult = repository.GetAll().ToList();
            foreach (var model in listResult)
            {
                repository.Delete(model);
            }
            // Assert
            Assert.IsTrue(listResult.Count == 3);
        }

        [Test]
        public void CanSelectAllEntityFrameworkImplementation()
        {
            // Arrange
            IRepository<Specialization> repository = new RepositoryEntityFrameworkImplementation<Specialization>(_contextEntity);
            var listModel = new List<Specialization>
            {
                new Specialization { Name = "Техник-прогрммист" },
                new Specialization { Name = "Бухгалтер" },
                new Specialization { Name = "Экономист" }
            };
            // Act
            foreach (var model in listModel)
            {
                repository.Insert(model);
            }
            var listResult = repository.GetAll().ToList();
            foreach (var model in listResult)
            {
                repository.Delete(model);
            }
            // Assert
            Assert.IsTrue(listResult.Count == 3);
        }
    }
}