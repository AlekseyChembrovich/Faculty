using System;
using System.Collections.Generic;
using Faculty.DataAccessLayer.Models;
using Faculty.DataAccessLayer.RepositoryAdo;
using Microsoft.Data.SqlClient;

namespace Faculty.DataAccessLayer.RepositoryAdoModel
{
    /// <summary>
    /// Implementation of the repository pattern for the Curator data model.
    /// </summary>
    public class RepositoryAdoCurator : BaseRepositoryAdo<Curator>
    {
        /// <summary>
        /// Connection context initialization constructor.
        /// </summary>
        /// <param name="context">Database connection.</param>
        public RepositoryAdoCurator(DatabaseContextAdo context) : base(context) { }

        /// <summary>
        /// Method for setting the add request parameters.
        /// </summary>
        /// <param name="entity">Entity object.</param>
        /// <param name="command">Object SqlCommand.</param>
        protected override void SetParametersInsertCommand(Curator entity, SqlCommand command)
        {
            command.CommandText = "INSERT INTO dbo.Curators (dbo.Curators.Surname, dbo.Curators.Name, dbo.Curators.Doublename, dbo.Curators.Phone) " +
                                  "VALUES (@surname, @name, @doublename, @phone);";
            command.Parameters.AddWithValue("@surname", entity.Surname is null ? DBNull.Value : entity.Surname);
            command.Parameters.AddWithValue("@name", entity.Name is null ? DBNull.Value : entity.Name);
            command.Parameters.AddWithValue("@doublename", entity.Doublename is null ? DBNull.Value : entity.Doublename);
            command.Parameters.AddWithValue("@phone", entity.Phone is null ? DBNull.Value : entity.Phone);
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
            command.Parameters.AddWithValue("@surname", entity.Surname is null ? DBNull.Value : entity.Surname);
            command.Parameters.AddWithValue("@name", entity.Name is null ? DBNull.Value : entity.Name);
            command.Parameters.AddWithValue("@doublename", entity.Doublename is null ? DBNull.Value : entity.Doublename);
            command.Parameters.AddWithValue("@phone", entity.Phone is null ? DBNull.Value : entity.Phone);
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
        protected override List<Curator> SetParametersSelectCommandModels(SqlCommand command)
        {
            command.CommandText = "SELECT * FROM dbo.Curators;";
            var sqlDataReader = command.ExecuteReader();
            var curators = new List<Curator>();
            while (sqlDataReader.Read())
            {
                int.TryParse(sqlDataReader.GetValue(0)?.ToString(), out var id);
                var surname = TryParseNullableString(sqlDataReader.GetValue(1).ToString());
                var name = TryParseNullableString(sqlDataReader.GetValue(2).ToString());
                var doublename = TryParseNullableString(sqlDataReader.GetValue(3).ToString());
                var phone = TryParseNullableString(sqlDataReader.GetValue(4).ToString());
                var curator = new Curator { Id = id, Surname = surname, Name = name, Doublename = doublename, Phone = phone };
                curators.Add(curator);
            }
            sqlDataReader.Close();
            return curators;

            string? TryParseNullableString(object value) => value.ToString() == "" ? null : value.ToString();
        }

        /// <summary>
        /// Method for selecting data from a database.
        /// </summary>
        /// <param name="id">Unique identificator model.</param>
        /// <returns>Entity object.</returns>
        protected override Curator SetParametersSelectCommandModel(int id, SqlCommand command)
        {
            command.CommandText = "SELECT * FROM dbo.Curators WHERE dbo.Curators.Id = @id;";
            command.Parameters.AddWithValue("@id", id);
            var sqlDataReader = command.ExecuteReader();
            Curator curator = null;
            while (sqlDataReader.Read())
            {
                int.TryParse(sqlDataReader.GetValue(0)?.ToString(), out var modelId);
                var surname = TryParseNullableString(sqlDataReader.GetValue(1).ToString());
                var name = TryParseNullableString(sqlDataReader.GetValue(2).ToString());
                var doublename = TryParseNullableString(sqlDataReader.GetValue(3).ToString());
                var phone = TryParseNullableString(sqlDataReader.GetValue(4).ToString());
                curator = new Curator { Id = modelId, Surname = surname, Name = name, Doublename = doublename, Phone = phone };
            }
            sqlDataReader.Close();
            return curator;

            string? TryParseNullableString(object value) => value.ToString() == "" ? null : value.ToString();
        }
    }
}
