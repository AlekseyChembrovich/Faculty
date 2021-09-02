using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Faculty.DataAccessLayer.Models;

namespace Faculty.DataAccessLayer.RepositoryAdo.RepositoryModels
{
    /// <summary>
    /// Implementation of the repository pattern for the Curator data model.
    /// </summary>
    public class RepositoryCurator : BaseRepositoryAdo<Curator>
    {
        /// <summary>
        /// Connection context initialization constructor.
        /// </summary>
        /// <param name="context">Database connection.</param>
        public RepositoryCurator(DatabaseContextAdo context) : base(context) { }

        /// <summary>
        /// Method for setting the add request parameters.
        /// </summary>
        /// <param name="entity">Entity object.</param>
        /// <param name="command">Object SqlCommand.</param>
        protected override void SetParametersInsertCommand(Curator entity, SqlCommand command)
        {
            command.CommandText = "INSERT INTO dbo.Curators (dbo.Curators.Surname, dbo.Curators.Name, dbo.Curators.Doublename, dbo.Curators.Phone) " +
                                  "VALUES (@surname, @name, @doublename, @phone);";
            command.Parameters.AddWithValue("@surname", entity.Surname);
            command.Parameters.AddWithValue("@name", entity.Name);
            command.Parameters.AddWithValue("@doublename", entity.Doublename);
            command.Parameters.AddWithValue("@phone", entity.Phone);
        }

        /// <summary>
        /// Method for setting change request parameters.
        /// </summary>
        /// <param name="entity">Entity object.</param>
        /// <param name="command">Object SqlCommand.</param>
        protected override void SetParametersUpdateCommand(Curator entity, SqlCommand command)
        {
            command.CommandText = "UPDATE dbo.Curators SET dbo.Curators.Surname = @surname, dbo.Curators.Name = @name, dbo.Curators.Doublename = @doublename," +
                                  "dbo.Curators.Phone = @phone WHERE dbo.Curators.Id = @id;";
            command.Parameters.AddWithValue("@surname", entity.Surname);
            command.Parameters.AddWithValue("@name", entity.Name);
            command.Parameters.AddWithValue("@doublename", entity.Doublename);
            command.Parameters.AddWithValue("@phone", entity.Phone);
            command.Parameters.AddWithValue("@id", entity.Id);
        }

        /// <summary>
        /// Method for setting delete request parameters.
        /// </summary>
        /// <param name="entity">Entity object.</param>
        /// <param name="command">Object SqlCommand.</param>
        protected override void SetParametersDeleteCommand(Curator entity, SqlCommand command)
        {
            command.CommandText = "DELETE FROM dbo.Curators WHERE dbo.Curators.Id = @id;";
            command.Parameters.AddWithValue("@id", entity.Id);
        }

        /// <summary>
        /// Method for setting query parameters for fetching all data.
        /// </summary>
        /// <param name="command">Object SqlCommand.</param>
        /// <returns>List of Entity objects.</returns>
        protected override List<Curator> SetParametersSelectCommand(SqlCommand command)
        {
            command.CommandText = "SELECT * FROM dbo.Curators;";
            var sqlDataReader = command.ExecuteReader();
            var curators = new List<Curator>();
            while (sqlDataReader.Read())
            {
                int.TryParse(sqlDataReader.GetValue(0)?.ToString() ?? string.Empty, out var id);
                var surname = sqlDataReader.GetValue(1)?.ToString() ?? string.Empty;
                var name = sqlDataReader.GetValue(2)?.ToString() ?? string.Empty;
                var doublename = sqlDataReader.GetValue(3)?.ToString() ?? string.Empty;
                var phone = sqlDataReader.GetValue(4)?.ToString() ?? string.Empty;
                var curator = new Curator { Id = id, Surname = surname, Name = name, Doublename = doublename, Phone = phone };
                curators.Add(curator);
            }
            sqlDataReader.Close();
            return curators;
        }
    }
}
