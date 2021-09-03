using System.Collections.Generic;
using Faculty.DataAccessLayer.Models;
using Microsoft.Data.SqlClient;

namespace Faculty.DataAccessLayer.RepositoryAdo.RepositoryModels
{
    public class RepositoryCurator : BaseRepositoryAdo<Curator>
    {
        public RepositoryCurator(DatabaseContextAdo context) : base(context) { }

        protected override void SetParametersInsertCommand(Curator entity, SqlCommand command)
        {
            command.CommandText = "INSERT INTO dbo.Curators (dbo.Curators.Surname, dbo.Curators.Name, dbo.Curators.Doublename, dbo.Curators.Phone) " +
                                  "VALUES (@surname, @name, @doublename, @phone);";
            command.Parameters.AddWithValue("@surname", entity.Surname);
            command.Parameters.AddWithValue("@name", entity.Name);
            command.Parameters.AddWithValue("@doublename", entity.Doublename);
            command.Parameters.AddWithValue("@phone", entity.Phone);
        }

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

        protected override void SetParametersDeleteCommand(Curator entity, SqlCommand command)
        {
            command.CommandText = "DELETE FROM dbo.Curators WHERE dbo.Curators.Id = @id;";
            command.Parameters.AddWithValue("@id", entity.Id);
        }

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