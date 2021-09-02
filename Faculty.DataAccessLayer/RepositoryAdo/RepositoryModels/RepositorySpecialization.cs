using System.Collections.Generic;
using Faculty.DataAccessLayer.Models;
using Microsoft.Data.SqlClient;

namespace Faculty.DataAccessLayer.RepositoryAdo.RepositoryModels
{
    public class RepositorySpecialization : BaseRepositoryAdo<Specialization>
    {
        public RepositorySpecialization(DatabaseContextAdo context) : base(context) { }

        protected override void SetParametersInsertCommand(Specialization entity, SqlCommand command)
        {
            command.CommandText = "INSERT INTO dbo.Specializations (dbo.Specializations.Name) VALUES (@name);";
            command.Parameters.AddWithValue("@name", entity.Name);
        }

        protected override void SetParametersUpdateCommand(Specialization entity, SqlCommand command)
        {
            command.CommandText = "UPDATE dbo.Specializations SET dbo.Specializations.Name = @name WHERE dbo.Specializations.Id = @id;";
            command.Parameters.AddWithValue("@name", entity.Name);
            command.Parameters.AddWithValue("@id", entity.Id);
        }

        protected override void SetParametersDeleteCommand(Specialization entity, SqlCommand command)
        {
            command.CommandText = "DELETE FROM dbo.Specializations WHERE dbo.Specializations.Id = @id;";
            command.Parameters.AddWithValue("@id", entity.Id);
        }

        protected override List<Specialization> SetParametersSelectCommand(SqlCommand command)
        {
            command.CommandText = "SELECT * FROM dbo.Specializations;";
            var sqlDataReader = command.ExecuteReader();
            var specializations = new List<Specialization>();
            while (sqlDataReader.Read())
            {
                int.TryParse(sqlDataReader.GetValue(0)?.ToString() ?? string.Empty, out var id);
                var name = sqlDataReader.GetValue(1)?.ToString() ?? string.Empty;
                var specialization = new Specialization { Id = id, Name = name };
                specializations.Add(specialization);
            }
            sqlDataReader.Close();
            return specializations;
        }
    }
}