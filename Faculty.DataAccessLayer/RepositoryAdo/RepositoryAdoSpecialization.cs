using System;
using System.Collections.Generic;
using Faculty.DataAccessLayer.Models;
using Microsoft.Data.SqlClient;

namespace Faculty.DataAccessLayer.RepositoryAdo
{
    /// <summary>
    /// Implementation of the repository pattern for the Specialization data model.
    /// </summary>
    public class RepositoryAdoSpecialization : BaseRepositoryAdo<Specialization>
    {
        /// <summary>
        /// Connection context initialization constructor.
        /// </summary>
        /// <param name="context">Database connection.</param>
        public RepositoryAdoSpecialization(DatabaseContextAdo context) : base(context) { }

        /// <summary>
        /// Method for setting the add request parameters.
        /// </summary>
        /// <param name="entity">Entity object.</param>
        /// <param name="command">Object SqlCommand.</param>
        protected override void SetParametersInsertCommand(Specialization entity, SqlCommand command)
        {
            command.CommandText = "INSERT INTO dbo.Specializations (dbo.Specializations.Name) VALUES (@name);";
            command.Parameters.AddWithValue("@name", entity.Name is null ? DBNull.Value : entity.Name);
        }

        /// <summary>
        /// Method for setting change request parameters.
        /// </summary>
        /// <param name="entity">Entity object.</param>
        /// <param name="command">Object SqlCommand.</param>
        protected override void SetParametersUpdateCommand(Specialization entity, SqlCommand command)
        {
            command.CommandText = "UPDATE dbo.Specializations SET dbo.Specializations.Name = @name WHERE dbo.Specializations.Id = @id;";
            command.Parameters.AddWithValue("@name", entity.Name is null ? DBNull.Value : entity.Name);
            command.Parameters.AddWithValue("@id", entity.Id);
        }

        /// <summary>
        /// Method for setting delete request parameters.
        /// </summary>
        /// <param name="entity">Entity object.</param>
        /// <param name="command">Object SqlCommand.</param>
        protected override void SetParametersDeleteCommand(Specialization entity, SqlCommand command)
        {
            command.CommandText = "DELETE FROM dbo.Specializations WHERE dbo.Specializations.Id = @id;";
            command.Parameters.AddWithValue("@id", entity.Id);
        }

        /// <summary>
        /// Method for setting query parameters for fetching all data.
        /// </summary>
        /// <param name="command">Object SqlCommand.</param>
        /// <returns>List of Entity objects.</returns>
        protected override List<Specialization> SetParametersSelectCommandModels(SqlCommand command)
        {
            command.CommandText = "SELECT * FROM dbo.Specializations;";
            var sqlDataReader = command.ExecuteReader();
            var specializations = new List<Specialization>();
            while (sqlDataReader.Read())
            {
                int.TryParse(sqlDataReader.GetValue(0)?.ToString(), out var id);
                var name = TryParseNullableString(sqlDataReader.GetValue(1).ToString());
                var specialization = new Specialization { Id = id, Name = name };
                specializations.Add(specialization);
            }
            sqlDataReader.Close();
            return specializations;

            string? TryParseNullableString(object value) => value.ToString() == "" ? null : value.ToString();
        }

        /// <summary>
        /// Method for selecting data from a database.
        /// </summary>
        /// <param name="id">Unique identificator model.</param>
        /// <returns>Entity object.</returns>
        protected override Specialization SetParametersSelectCommandModel(int id, SqlCommand command)
        {
            command.CommandText = "SELECT * FROM dbo.Specializations WHERE dbo.Specializations.Id = @id;";
            command.Parameters.AddWithValue("@id", id);
            var sqlDataReader = command.ExecuteReader();
            Specialization specialization = null;
            while (sqlDataReader.Read())
            {
                int.TryParse(sqlDataReader.GetValue(0)?.ToString(), out var modelId);
                var name = TryParseNullableString(sqlDataReader.GetValue(1).ToString());
                specialization = new Specialization { Id = modelId, Name = name };
            }
            sqlDataReader.Close();
            return specialization;

            string? TryParseNullableString(object value) => value.ToString() == "" ? null : value.ToString();
        }
    }
}
