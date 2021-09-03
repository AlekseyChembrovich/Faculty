using System.Collections.Generic;
using Faculty.DataAccessLayer.Models;
using Microsoft.Data.SqlClient;

namespace Faculty.DataAccessLayer.RepositoryAdo.RepositoryModels
{
    public class RepositoryStudent : BaseRepositoryAdo<Student>
    {
        public RepositoryStudent(DatabaseContextAdo context) : base(context) { }

        protected override void SetParametersInsertCommand(Student entity, SqlCommand command)
        {
            command.CommandText = "INSERT INTO dbo.Students (dbo.Students.Surname, dbo.Students.Name, dbo.Students.Doublename) " +
                                  "VALUES (@surname, @name, @doublename);";
            command.Parameters.AddWithValue("@surname", entity.Surname);
            command.Parameters.AddWithValue("@name", entity.Name);
            command.Parameters.AddWithValue("@doublename", entity.Doublename);
        }

        protected override void SetParametersUpdateCommand(Student entity, SqlCommand command)
        {
            command.CommandText = "UPDATE dbo.Students SET dbo.Students.Surname = @surname, dbo.Students.Name = @name, dbo.Students.Doublename = @doublename " +
                                  "WHERE dbo.Students.Id = @id;";
            command.Parameters.AddWithValue("@surname", entity.Surname);
            command.Parameters.AddWithValue("@name", entity.Name);
            command.Parameters.AddWithValue("@doublename", entity.Doublename);
            command.Parameters.AddWithValue("@id", entity.Id);
        }

        protected override void SetParametersDeleteCommand(Student entity, SqlCommand command)
        {
            command.CommandText = "DELETE FROM dbo.Students WHERE dbo.Students.Id = @id;";
            command.Parameters.AddWithValue("@id", entity.Id);
        }

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