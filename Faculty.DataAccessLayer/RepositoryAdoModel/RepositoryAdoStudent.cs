using System;
using System.Collections.Generic;
using Faculty.DataAccessLayer.Models;
using Faculty.DataAccessLayer.RepositoryAdo;
using Microsoft.Data.SqlClient;

namespace Faculty.DataAccessLayer.RepositoryAdoModel
{
    /// <summary>
    /// Implementation of the repository pattern for the Student data model.
    /// </summary>
    public class RepositoryAdoStudent : BaseRepositoryAdo<Student>
    {
        /// <summary>
        /// Connection context initialization constructor.
        /// </summary>
        /// <param name="context">Database connection.</param>
        public RepositoryAdoStudent(DatabaseContextAdo context) : base(context) { }

        /// <summary>
        /// Method for setting the add request parameters.
        /// </summary>
        /// <param name="entity">Entity object.</param>
        /// <param name="command">Object SqlCommand.</param>
        protected override void SetParametersInsertCommand(Student entity, SqlCommand command)
        {
            command.CommandText = "INSERT INTO dbo.Students (dbo.Students.Surname, dbo.Students.Name, dbo.Students.Doublename) " +
                                  "VALUES (@surname, @name, @doublename);";
            command.Parameters.AddWithValue("@surname", entity.Surname is null ? DBNull.Value : entity.Surname);
            command.Parameters.AddWithValue("@name", entity.Name is null ? DBNull.Value : entity.Name);
            command.Parameters.AddWithValue("@doublename", entity.Doublename is null ? DBNull.Value : entity.Doublename);
        }

        /// <summary>
        /// Method for setting change request parameters.
        /// </summary>
        /// <param name="entity">Entity object.</param>
        /// <param name="command">Object SqlCommand.</param>
        protected override void SetParametersUpdateCommand(Student entity, SqlCommand command)
        {
            command.CommandText = "UPDATE dbo.Students SET dbo.Students.Surname = @surname, dbo.Students.Name = @name, dbo.Students.Doublename = @doublename " +
                                  "WHERE dbo.Students.Id = @id;";
            command.Parameters.AddWithValue("@surname", entity.Surname is null ? DBNull.Value : entity.Surname);
            command.Parameters.AddWithValue("@name", entity.Name is null ? DBNull.Value : entity.Name);
            command.Parameters.AddWithValue("@doublename", entity.Doublename is null ? DBNull.Value : entity.Doublename);
            command.Parameters.AddWithValue("@id", entity.Id);
        }

        /// <summary>
        /// Method for setting delete request parameters.
        /// </summary>
        /// <param name="entity">Entity object.</param>
        /// <param name="command">Object SqlCommand.</param>
        protected override void SetParametersDeleteCommand(Student entity, SqlCommand command)
        {
            command.CommandText = "DELETE FROM dbo.Students WHERE dbo.Students.Id = @id;";
            command.Parameters.AddWithValue("@id", entity.Id);
        }

        /// <summary>
        /// Method for setting query parameters for fetching all data.
        /// </summary>
        /// <param name="command">Object SqlCommand.</param>
        /// <returns>List of Entity objects.</returns>
        protected override List<Student> SetParametersSelectCommandModels(SqlCommand command)
        {
            command.CommandText = "SELECT * FROM dbo.Students;";
            var sqlDataReader = command.ExecuteReader();
            var students = new List<Student>();
            while (sqlDataReader.Read())
            {
                int.TryParse(sqlDataReader.GetValue(0)?.ToString(), out var id);
                var surname = TryParseNullableString(sqlDataReader.GetValue(1).ToString());
                var name = TryParseNullableString(sqlDataReader.GetValue(2).ToString());
                var doublename = TryParseNullableString(sqlDataReader.GetValue(3).ToString());
                var student = new Student { Id = id, Surname = surname, Name = name, Doublename = doublename };
                students.Add(student);
            }
            sqlDataReader.Close();
            return students;

            string? TryParseNullableString(object value) => value.ToString() == "" ? null : value.ToString();
        }

        /// <summary>
        /// Method for selecting data from a database.
        /// </summary>
        /// <param name="id">Unique identificator model.</param>
        /// <returns>Entity object.</returns>
        protected override Student SetParametersSelectCommandModel(int id, SqlCommand command)
        {
            command.CommandText = "SELECT * FROM dbo.Students WHERE dbo.Students.Id = @id;";
            command.Parameters.AddWithValue("@id", id);
            var sqlDataReader = command.ExecuteReader();
            Student student = null;
            while (sqlDataReader.Read())
            {
                int.TryParse(sqlDataReader.GetValue(0)?.ToString(), out var modelId);
                var surname = TryParseNullableString(sqlDataReader.GetValue(1).ToString());
                var name = TryParseNullableString(sqlDataReader.GetValue(2).ToString());
                var doublename = TryParseNullableString(sqlDataReader.GetValue(3).ToString());
                student = new Student { Id = modelId, Surname = surname, Name = name, Doublename = doublename };
            }
            sqlDataReader.Close();
            return student;

            string? TryParseNullableString(object value) => value.ToString() == "" ? null : value.ToString();
        }
    }
}
