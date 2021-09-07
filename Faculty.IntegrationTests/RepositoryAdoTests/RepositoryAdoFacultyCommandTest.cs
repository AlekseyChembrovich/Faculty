using System;
using System.IO;
using Faculty.DataAccessLayer.RepositoryAdo;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace Faculty.IntegrationTests.RepositoryAdoTests
{
    [TestFixture]
    public class RepositoryAdoFacultyCommandTest
    {
        private DatabaseContextAdo _contextAdo;

        [SetUp]
        public void Setup()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile(Path.Combine(Environment.CurrentDirectory, "appsettings.json")).Build();
            var connectionString = configuration["ConnectionStrings:DefaultConnection"];
            _contextAdo = new DatabaseContextAdo(connectionString);
        }
    }
}