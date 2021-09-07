using System;
using System.IO;
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
        /// Connection string.
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Configuration properties.
        /// </summary>
        private readonly IConfiguration _configuration;

        public DatabaseConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
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
                    dacService.Deploy(dacPackage, "FactoryTest", true, dacOptions);
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
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = @"
                        USE master;
                        drop database[TestMusicCatalog]";
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}