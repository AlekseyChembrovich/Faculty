using System;
using System.IO;
using Microsoft.SqlServer.Dac;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Faculty.DataAccessLayer.Repository.Ado;

namespace Faculty.IntegrationTests
{
    /// <summary>
    /// Setting configuration for a test database.
    /// </summary>
    public class DatabaseConfiguration
    {
        /// <summary>
        /// Table name constant.
        /// </summary>
        private const string TestTableName = "FacultyTest";

        /// <summary>
        /// Connection string.
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Context Ado data base.
        /// </summary>
        public DatabaseContextAdo ContextAdo { get; set; }

        /// <summary>
        /// Configuration properties.
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor for init IConfiguration.
        /// </summary>
        /// <param name="configuration"></param>
        public DatabaseConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            ContextAdo = new DatabaseContextAdo(_connectionString);
        }

        /// <summary>
        /// Deploys the test database from file.
        /// </summary>
        public void DeployTestDatabase()
        {
            var dacOptions = new DacDeployOptions
            {
                IncludeCompositeObjects = true,
                BlockOnPossibleDataLoss = false,
                IgnoreAuthorizer = true,
                IgnoreUserSettingsObjects = true,
                VerifyDeployment = false,
                VerifyCollationCompatibility = false,
                CreateNewDatabase = true
            };

            var dacService = new DacServices(_connectionString);
            var dacPacPath = _configuration.GetSection("appSettings")["dacpacFilePath"];
            dacPacPath = Environment.CurrentDirectory + dacPacPath;

            if (File.Exists(dacPacPath))
            {
                using (var dacPackage = DacPackage.Load(dacPacPath))
                {
                    dacService.Deploy(dacPackage, TestTableName, true, dacOptions);
                }
            }
            else
            {
                throw new Exception($"Error load database from dacpac file.({dacPacPath})");
            }
        }

        /// <summary>
        /// Drops the test database.
        /// </summary>
        public void DropTestDatabase()
        {
            ContextAdo.Dispose();
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = $"USE master; DROP DATABASE {TestTableName};";
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
