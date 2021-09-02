using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Faculty.DataAccessLayer.Models;

namespace Faculty.DataAccessLayer.RepositoryAdo.RepositoryModels
{
    /// <summary>
    /// Implementation of the repository pattern for the Group data model.
    /// </summary>
    public class RepositoryGroup : BaseRepositoryAdo<Group>
    {
        /// <summary>
        /// Connection context initialization constructor.
        /// </summary>
        /// <param name="context">Database connection.</param>
        public RepositoryGroup(DatabaseContextAdo context) : base(context) { }

        /// <summary>
        /// Method for setting the add request parameters.
        /// </summary>
        /// <param name="entity">Entity object.</param>
        /// <param name="command">Object SqlCommand.</param>
        protected override void SetParametersInsertCommand(Group entity, SqlCommand command)
        {
            command.CommandText = "INSERT INTO dbo.Groups (dbo.Groups.Name, dbo.Groups.SpecializationId) " +
                                  "VALUES (@name, @specializationId);";
            command.Parameters.AddWithValue("@name", entity.Name);
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
            command.Parameters.AddWithValue("@name", entity.Name);
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
        protected override List<Group> SetParametersSelectCommand(SqlCommand command)
        {
            command.CommandText = "SELECT * FROM dbo.Groups;";
            var sqlDataReader = command.ExecuteReader();
            var groups = new List<Group>();
            while (sqlDataReader.Read())
            {
                int.TryParse(sqlDataReader.GetValue(0)?.ToString() ?? string.Empty, out var id);
                var name = sqlDataReader.GetValue(1)?.ToString() ?? string.Empty;
                int.TryParse(sqlDataReader.GetValue(2)?.ToString() ?? string.Empty, out var specializationId);
                var group = new Group { Id = id, Name = name, SpecializationId = specializationId };
                groups.Add(group);
            }
            sqlDataReader.Close();
            return groups;
        }
    }
}
