using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Faculty.DataAccessLayer.RepositoryAdo
{
    /// <summary>
    /// Implementation of the repository pattern for the Faculty data model.
    /// </summary>
    public class RepositoryAdoFaculty : BaseRepositoryAdo<Models.Faculty>
    {
        /// <summary>
        /// Connection context initialization constructor.
        /// </summary>
        /// <param name="context">Database connection.</param>
        public RepositoryAdoFaculty(DatabaseContextAdo context) : base(context) { }

        /// <summary>
        /// Method for setting the add request parameters.
        /// </summary>
        /// <param name="entity">Entity object.</param>
        /// <param name="command">Object SqlCommand.</param>
        protected override void SetParametersInsertCommand(Models.Faculty entity, SqlCommand command)
        {
            command.CommandText = "INSERT INTO dbo.Faculties (dbo.Faculties.StartDateEducation, dbo.Faculties.CountYearEducation, " +
                                  "dbo.Faculties.StudentId, dbo.Faculties.GroupId, dbo.Faculties.CuratorId) " +
                                  "VALUES (@date, @years, @studentId, @groupId, @curatorId);";
            command.Parameters.AddWithValue("@date", entity.StartDateEducation?.ToShortDateString() is null ? DBNull.Value : entity.StartDateEducation);
            command.Parameters.AddWithValue("@years", entity.CountYearEducation is null ? DBNull.Value : entity.CountYearEducation);
            command.Parameters.AddWithValue("@studentId", entity.StudentId is null ? DBNull.Value : entity.StudentId);
            command.Parameters.AddWithValue("@groupId", entity.GroupId is null ? DBNull.Value : entity.GroupId);
            command.Parameters.AddWithValue("@curatorId", entity.CuratorId is null ? DBNull.Value : entity.CuratorId);
        }

        /// <summary>
        /// Method for setting change request parameters.
        /// </summary>
        /// <param name="entity">Entity object.</param>
        /// <param name="command">Object SqlCommand.</param>
        protected override void SetParametersUpdateCommand(Models.Faculty entity, SqlCommand command)
        {
            command.CommandText = "UPDATE dbo.Faculties SET dbo.Faculties.StartDateEducation = @date, dbo.Faculties.CountYearEducation = @years, " +
                                  "dbo.Faculties.StudentId = @studentId, dbo.Faculties.GroupId = @curatorId, dbo.Faculties.CuratorId = @curatorId " +
                                  "WHERE dbo.Faculties.Id = @id;";
            command.Parameters.AddWithValue("@date", entity.StartDateEducation?.ToShortDateString() is null ? DBNull.Value : entity.StartDateEducation);
            command.Parameters.AddWithValue("@years", entity.CountYearEducation is null ? DBNull.Value : entity.CountYearEducation);
            command.Parameters.AddWithValue("@studentId", entity.StudentId is null ? DBNull.Value : entity.StudentId);
            command.Parameters.AddWithValue("@groupId", entity.GroupId is null ? DBNull.Value : entity.GroupId);
            command.Parameters.AddWithValue("@curatorId", entity.CuratorId is null ? DBNull.Value : entity.CuratorId);
            command.Parameters.AddWithValue("@id", entity.Id);
        }

        /// <summary>
        /// Method for setting delete request parameters.
        /// </summary>
        /// <param name="entity">Entity object.</param>
        /// <param name="command">Object SqlCommand.</param>
        protected override void SetParametersDeleteCommand(Models.Faculty entity, SqlCommand command)
        {
            command.CommandText = "DELETE FROM dbo.Faculties WHERE dbo.Faculties.Id = @id;";
            command.Parameters.AddWithValue("@id", entity.Id);
        }

        /// <summary>
        /// Method for setting query parameters for fetching all data.
        /// </summary>
        /// <param name="command">Object SqlCommand.</param>
        /// <returns>List of Entity objects.</returns>
        protected override List<Models.Faculty> SetParametersSelectCommandModels(SqlCommand command)
        {
            command.CommandText = "SELECT * FROM dbo.Faculties;";
            var sqlDataReader = command.ExecuteReader();
            var faculties = new List<Models.Faculty>();
            while (sqlDataReader.Read())
            {
                int.TryParse(sqlDataReader.GetValue(0)?.ToString(), out var id);
                var startDateEducation = TryParseNullableDateTime(sqlDataReader.GetValue(1)?.ToString() ?? string.Empty);
                var countYearEducation = TryParseNullableInt(sqlDataReader.GetValue(2)?.ToString() ?? string.Empty);
                var studentId = TryParseNullableInt(sqlDataReader.GetValue(3)?.ToString() ?? string.Empty);
                var groupId = TryParseNullableInt(sqlDataReader.GetValue(4)?.ToString() ?? string.Empty);
                var curatorId = TryParseNullableInt(sqlDataReader.GetValue(5)?.ToString() ?? string.Empty);
                var faculty = new Models.Faculty
                {
                    Id = id,
                    StartDateEducation = startDateEducation,
                    CountYearEducation = countYearEducation,
                    StudentId = studentId,
                    GroupId = groupId,
                    CuratorId = curatorId
                };
                faculties.Add(faculty);
            }
            sqlDataReader.Close();
            return faculties;

            int? TryParseNullableInt(string value) => int.TryParse(value, out var outValue) ? (int?)outValue : null;
            DateTime? TryParseNullableDateTime(string value) => DateTime.TryParse(value, out var outValue) ? (DateTime?)outValue : null;
        }

        /// <summary>
        /// Method for selecting data from a database.
        /// </summary>
        /// <param name="id">Unique identificator model.</param>
        /// <returns>Entity object.</returns>
        protected override Models.Faculty SetParametersSelectCommandModel(int id, SqlCommand command)
        {
            command.CommandText = "SELECT * FROM dbo.Faculties WHERE dbo.Faculties.Id = @id;";
            command.Parameters.AddWithValue("@id", id);
            var sqlDataReader = command.ExecuteReader();
            Models.Faculty faculty = null;
            while (sqlDataReader.Read())
            {
                int.TryParse(sqlDataReader.GetValue(0)?.ToString(), out var modelId);
                var startDateEducation = TryParseNullableDateTime(sqlDataReader.GetValue(1)?.ToString() ?? string.Empty);
                var countYearEducation = TryParseNullableInt(sqlDataReader.GetValue(2)?.ToString() ?? string.Empty);
                var studentId = TryParseNullableInt(sqlDataReader.GetValue(3)?.ToString() ?? string.Empty);
                var groupId = TryParseNullableInt(sqlDataReader.GetValue(4)?.ToString() ?? string.Empty);
                var curatorId = TryParseNullableInt(sqlDataReader.GetValue(5)?.ToString() ?? string.Empty);
                faculty = new Models.Faculty
                {
                    Id = modelId,
                    StartDateEducation = startDateEducation,
                    CountYearEducation = countYearEducation,
                    StudentId = studentId,
                    GroupId = groupId,
                    CuratorId = curatorId
                };
            }
            sqlDataReader.Close();
            return faculty;

            int? TryParseNullableInt(string value) => int.TryParse(value, out var outValue) ? (int?)outValue : null;
            DateTime? TryParseNullableDateTime(string value) => DateTime.TryParse(value, out var outValue) ? (DateTime?)outValue : null;
        }
    }
}
