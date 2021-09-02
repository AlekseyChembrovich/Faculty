using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Faculty.DataAccessLayer.RepositoryAdo
{
    public class DatabaseContextAdo : IDisposable
    {
        private SqlConnection _sqlConnection;
        private readonly string _connectionString;

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

        public DatabaseContextAdo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Dispose()
        {
            if (_sqlConnection is { State: ConnectionState.Open })
            {
                _sqlConnection.Close();
            }
        }
    }
}