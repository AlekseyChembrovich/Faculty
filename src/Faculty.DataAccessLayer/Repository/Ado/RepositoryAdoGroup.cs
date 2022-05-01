using System;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Faculty.DataAccessLayer.Models;

namespace Faculty.DataAccessLayer.Repository.Ado
{
    /// <summary>
    /// Implementation of the repository pattern for the Group data model.
    /// </summary>
    public class RepositoryAdoGroup : BaseRepositoryAdo<Group>
    {
        /// <summary>
        /// Connection context initialization constructor.
        /// </summary>
        /// <param name="context">Database connection.</param>
        public RepositoryAdoGroup(DatabaseContextAdo context) : base(context) { }

        /// <summary>
        /// Method for setting the add request parameters.
        /// </summary>
        /// <param name="entity">Entity object.</param>
        /// <param name="command">Object SqlCommand.</param>
        protected override void SetParametersInsertCommand(Group entity, SqlCommand command)
        {
            command.CommandText = "INSERT INTO dbo.Groups (dbo.Groups.Name, dbo.Groups.SpecializationId) " +
                                  "VALUES (@name, @specializationId);";
            command.Parameters.AddWithValue("@name", entity.Name is null ? DBNull.Value : entity.Name);
            command.Parameters.AddWithValue("@specializationId", entity.SpecializationId);
        }

        /// <summary>
        /// Method for setting change request parameters.
        /// </summary>
        /// <param name="entity">Entity object.</param>
        /// <param name="command">Object SqlCommand.</param>
        protected override void SetParametersUpdateCommand(Group entity, SqlCommand command)
        {
            command.CommandText = "UPDATE dbo.Groups SET dbo.Groups.Name = @name, " +
                                  "dbo.Groups.SpecializationId = @specializationId WHERE dbo.Groups.Id = @id;";
            command.Parameters.AddWithValue("@name", entity.Name is null ? DBNull.Value : entity.Name);
            command.Parameters.AddWithValue("@specializationId", entity.SpecializationId);
            command.Parameters.AddWithValue("@id", entity.Id);
        }

        /// <summary>
        /// Method for setting delete request parameters.
        /// </summary>
        /// <param name="entity">Entity object.</param>
        /// <param name="command">Object SqlCommand.</param>
        protected override void SetParametersDeleteCommand(Group entity, SqlCommand command)
        {
            command.CommandText = "DELETE FROM dbo.Groups WHERE dbo.Groups.Id = @id;";
            command.Parameters.AddWithValue("@id", entity.Id);
        }

        /// <summary>
        /// Method for setting query parameters for fetching all data.
        /// </summary>
        /// <param name="command">Object SqlCommand.</param>
        /// <returns>List of Entity objects.</returns>
        protected override List<Group> SetParametersSelectCommandModels(SqlCommand command)
        {
            command.CommandText = "SELECT * FROM dbo.Groups;";
            var sqlDataReader = command.ExecuteReader();
            var groups = new List<Group>();
            while (sqlDataReader.Read())
            {
                int.TryParse(sqlDataReader.GetValue(0)?.ToString(), out var id);
                var name = TryParseNullableString(sqlDataReader.GetValue(1).ToString());
                var specializationId = TryParseNullableInt(sqlDataReader.GetValue(2)?.ToString() ?? string.Empty);
                var group = new Group { Id = id, Name = name, SpecializationId = specializationId ?? 0 };
                groups.Add(group);
            }

            sqlDataReader.Close();
            return groups;

            int? TryParseNullableInt(string value) => int.TryParse(value, out var outValue) ? (int?)outValue : null;
            string? TryParseNullableString(object value) => value.ToString() == "" ? null : value.ToString();
        }

        /// <summary>
        /// Method for selecting data from a database.
        /// </summary>
        /// <param name="id">Unique identificator model.</param>
        /// <returns>Entity object.</returns>
        protected override Group SetParametersSelectCommandModel(int id, SqlCommand command)
        {
            command.CommandText = "SELECT * FROM dbo.Groups WHERE dbo.Groups.Id = @id;";
            command.Parameters.AddWithValue("@id", id);
            var sqlDataReader = command.ExecuteReader();
            Group group = null;
            while (sqlDataReader.Read())
            {
                int.TryParse(sqlDataReader.GetValue(0)?.ToString(), out var modelId);
                var name = TryParseNullableString(sqlDataReader.GetValue(1).ToString());
                var specializationId = TryParseNullableInt(sqlDataReader.GetValue(2)?.ToString() ?? string.Empty);
                group = new Group { Id = modelId, Name = name, SpecializationId = specializationId ?? 0 };
            }

            sqlDataReader.Close();
            return group;

            int? TryParseNullableInt(string value) => int.TryParse(value, out var outValue) ? (int?)outValue : null;
            string? TryParseNullableString(object value) => value.ToString() == "" ? null : value.ToString();
        }
    }
}
