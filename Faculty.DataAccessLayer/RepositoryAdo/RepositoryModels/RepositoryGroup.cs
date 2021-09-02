using System.Collections.Generic;
using Faculty.DataAccessLayer.Models;
using Microsoft.Data.SqlClient;

namespace Faculty.DataAccessLayer.RepositoryAdo.RepositoryModels
{
    public class RepositoryGroup : BaseRepositoryAdo<Group>
    {
        public RepositoryGroup(DatabaseContextAdo context) : base(context) { }

        protected override void SetParametersInsertCommand(Group entity, SqlCommand command)
        {
            command.CommandText = "INSERT INTO dbo.Groups (dbo.Groups.Name, dbo.Groups.SpecializationId) " +
                                  "VALUES (@name, @specializationId);";
            command.Parameters.AddWithValue("@name", entity.Name);
            command.Parameters.AddWithValue("@specializationId", entity.SpecializationId);
        }

        protected override void SetParametersUpdateCommand(Group entity, SqlCommand command)
        {
            command.CommandText = "UPDATE dbo.Groups SET dbo.Groups.Name = @name, " +
                                  "dbo.Groups.SpecializationId = @specializationId WHERE dbo.Groups.Id = @id;";
            command.Parameters.AddWithValue("@name", entity.Name);
            command.Parameters.AddWithValue("@specializationId", entity.SpecializationId);
            command.Parameters.AddWithValue("@id", entity.Id);
        }

        protected override void SetParametersDeleteCommand(Group entity, SqlCommand command)
        {
            command.CommandText = "DELETE FROM dbo.Groups WHERE dbo.Groups.Id = @id;";
            command.Parameters.AddWithValue("@id", entity.Id);
        }

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