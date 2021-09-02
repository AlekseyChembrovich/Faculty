using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Faculty.DataAccessLayer.RepositoryAdo
{
    /// <summary>
    /// Database connection context.
    /// </summary>
    public class DatabaseContextAdo : IDisposable
    {
        /// <summary>
        /// Database connection.
        /// </summary>
        private SqlConnection _sqlConnection;
        /// <summary>
        /// Database connection string.
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Property to return the connection to the database.
        /// </summary>
        public SqlConnection SqlConnection
        {
            get
            {
                _sqlConnection ??= new SqlConnection(_connectionString);
                if (_sqlConnection.State != ConnectionState.Open)
                {
                    _sqlConnection.Open();
                }
                return _sqlConnection;
            }
        }

        /// <summary>
        /// Constructor for setting the connection string.
        /// </summary>
        /// <param name="connectionString"></param>
        public DatabaseContextAdo(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Method for freeing up resources.
        /// </summary>
        public void Dispose()
        {
            if (_sqlConnection is { State: ConnectionState.Open })
            {
                _sqlConnection.Close();
            }
        }
    }
}
