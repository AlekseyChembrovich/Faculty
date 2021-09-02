﻿using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Faculty.DataAccessLayer.RepositoryAdo.RepositoryModels
{
    public class RepositoryFaculty : BaseRepositoryAdo<Models.Faculty>
    {
        public RepositoryFaculty(DatabaseContextAdo context) : base(context) { }

        protected override void SetParametersInsertCommand(Models.Faculty entity, SqlCommand command)
        {
            command.CommandText = "INSERT INTO dbo.Faculties (dbo.Faculties.StartDateEducation, dbo.Faculties.CountYearEducation, " +
                                  "dbo.Faculties.StudentId, dbo.Faculties.GroupId, dbo.Faculties.CuratorId) " +
                                  "VALUES (@date, @years, @studentId, @groupId, @curatorId);";
            command.Parameters.AddWithValue("@date", entity.StartDateEducation?.ToShortDateString());
            command.Parameters.AddWithValue("@years", entity.CountYearEducation);
            command.Parameters.AddWithValue("@studentId", entity.StudentId);
            command.Parameters.AddWithValue("@groupId", entity.GroupId);
            command.Parameters.AddWithValue("@curatorId", entity.CuratorId);
        }

        protected override void SetParametersUpdateCommand(Models.Faculty entity, SqlCommand command)
        {
            command.CommandText = "UPDATE dbo.Faculties SET dbo.Faculties.StartDateEducation = @date, dbo.Faculties.CountYearEducation = @years, " +
                                  "dbo.Faculties.StudentId = @studentId, dbo.Faculties.GroupId = @curatorId, dbo.Faculties.CuratorId = @curatorId " +
                                  "WHERE dbo.Faculties.Id = @id;";
            command.Parameters.AddWithValue("@date", entity.StartDateEducation?.ToShortDateString());
            command.Parameters.AddWithValue("@years", entity.CountYearEducation);
            command.Parameters.AddWithValue("@studentId", entity.StudentId);
            command.Parameters.AddWithValue("@groupId", entity.GroupId);
            command.Parameters.AddWithValue("@curatorId", entity.CuratorId);
            command.Parameters.AddWithValue("@id", entity.Id);
        }

        protected override void SetParametersDeleteCommand(Models.Faculty entity, SqlCommand command)
        {
            command.CommandText = "DELETE FROM dbo.Faculties WHERE dbo.Faculties.Id = @id;";
            command.Parameters.AddWithValue("@id", entity.Id);
        }

        protected override List<Models.Faculty> SetParametersSelectCommand(SqlCommand command)
        {
            command.CommandText = "SELECT * FROM dbo.Faculties;";
            var sqlDataReader = command.ExecuteReader();
            var faculties = new List<Models.Faculty>();
            while (sqlDataReader.Read())
            {
                int.TryParse(sqlDataReader.GetValue(0)?.ToString() ?? string.Empty, out var id);
                DateTime.TryParse(sqlDataReader.GetValue(1)?.ToString() ?? string.Empty, out var startDateEducation);
                int.TryParse(sqlDataReader.GetValue(2)?.ToString() ?? string.Empty, out var countYearEducation);
                int.TryParse(sqlDataReader.GetValue(3)?.ToString() ?? string.Empty, out var studentId);
                int.TryParse(sqlDataReader.GetValue(4)?.ToString() ?? string.Empty, out var groupId);
                int.TryParse(sqlDataReader.GetValue(5)?.ToString() ?? string.Empty, out var curatorId);
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
        }
    }
}