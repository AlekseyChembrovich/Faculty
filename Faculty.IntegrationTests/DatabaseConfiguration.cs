using System;
using System.IO;
using Faculty.DataAccessLayer.RepositoryAdo;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.SqlServer.Dac;

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
        private const string TableName = "FacultyTest";

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
                CreateNewDatabase = true,
                IgnoreAuthorizer = true,
                IgnoreUserSettingsObjects = true
            };

            var dacService = new DacServices(_connectionString);
            var dacPacPath = _configuration.GetSection("appSettings")["dacpacFilePath"];
            dacPacPath = Environment.CurrentDirectory + dacPacPath;

            if (File.Exists(dacPacPath))
            {
                using (var dacPackage = DacPackage.Load(dacPacPath))
                {
                    dacService.Deploy(dacPackage, TableName, true, dacOptions);
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
            using (var command = ContextAdo.SqlConnection.CreateCommand())
            {
                try
                {
                    command.CommandText = $"USE master; DROP DATABASE IF EXISTS [{TableName}];";
                    command.ExecuteNonQuery();
                }
                catch (Exception) { }
            }
        }
    }
}