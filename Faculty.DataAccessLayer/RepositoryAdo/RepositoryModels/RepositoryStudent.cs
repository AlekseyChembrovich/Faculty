using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Faculty.DataAccessLayer.Models;

namespace Faculty.DataAccessLayer.RepositoryAdo.RepositoryModels
{
    /// <summary>
    /// Implementation of the repository pattern for the Student data model.
    /// </summary>
    public class RepositoryStudent : BaseRepositoryAdo<Student>
    {
        /// <summary>
        /// Connection context initialization constructor.
        /// </summary>
        /// <param name="context">Database connection.</param>
        public RepositoryStudent(DatabaseContextAdo context) : base(context) { }

        /// <summary>
        /// Method for setting the add request parameters.
        /// </summary>
        /// <param name="entity">Entity object.</param>
        /// <param name="command">Object SqlCommand.</param>
        protected override void SetParametersInsertCommand(Student entity, SqlCommand command)
        {
            command.CommandText = "INSERT INTO dbo.Students (dbo.Students.Surname, dbo.Students.Name, dbo.Students.Doublename) " +
                                  "VALUES (@surname, @name, @doublename);";
            command.Parameters.AddWithValue("@surname", entity.Surname);
            command.Parameters.AddWithValue("@name", entity.Name);
            command.Parameters.AddWithValue("@doublename", entity.Doublename);
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
            command.Parameters.AddWithValue("@surname", entity.Surname);
            command.Parameters.AddWithValue("@name", entity.Name);
            command.Parameters.AddWithValue("@doublename", entity.Doublename);
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
        protected override List<Student> SetParametersSelectCommand(SqlCommand command)
        {
            command.CommandText = "SELECT * FROM dbo.Students;";
            var sqlDataReader = command.ExecuteReader();
            var students = new List<Student>();
            while (sqlDataReader.Read())
            {
                int.TryParse(sqlDataReader.GetValue(0)?.ToString() ?? string.Empty, out var id);
                var surname = sqlDataReader.GetValue(1)?.ToString() ?? string.Empty;
                var name = sqlDataReader.GetValue(2)?.ToString() ?? string.Empty;
                var doublename = sqlDataReader.GetValue(3)?.ToString() ?? string.Empty;
                var student = new Student { Id = id, Surname = surname, Name = name, Doublename = doublename };
                students.Add(student);
            }
            sqlDataReader.Close();
            return students;
        }
    }
}
