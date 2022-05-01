using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Faculty.DataAccessLayer.Models;
using Microsoft.Extensions.DependencyInjection;
using Faculty.DataAccessLayer.Repository.EntityFramework;

namespace Faculty.ResourceServer.Tools
{
    public static class PreparingDatabase
    {
        public static void PrepSeedData(IApplicationBuilder app, bool isProd)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<DatabaseContextEntityFramework>();
                var logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<Startup>>();
                SeedData(context, logger, isProd);
            }
        }

        private static void SeedData(DatabaseContextEntityFramework context, ILogger<Startup> logger, bool isProd)
        {
            bool isApplied = ApplyMigration(isProd, logger, context);
            if (isApplied)
            {
                if (!context.Students.Any())
                {
                    SeedStudents(context);
                }

                if (!context.Curators.Any())
                {
                    SeedCurators(context);
                }

                if (!context.Specializations.Any())
                {
                    SeedSpecializations(context);
                }

                if (!context.Groups.Any())
                {
                    SeedGroups(context);
                }

                if (!context.Faculties.Any())
                {
                    SeedFaculties(context);
                }
            }
            else
            {
                logger.LogWarning("Data did not seed in the database.");
            }
        }

        private static bool ApplyMigration(bool isProd, ILogger<Startup> logger, DatabaseContextEntityFramework context)
        {
            if (isProd)
            {
                logger.LogInformation("--> Attempting to apply migration...");
                try
                {
                    if (context.Database.IsSqlServer())
                    {
                        context.Database.Migrate();
                    }

                    logger.LogInformation("--> Migration applied.");
                    return true;
                }
                catch (Exception ex)
                {
                    logger.LogError($"--> The error occurred in context.Database.Migrate().");
                    logger.LogError($"--> {ex.Message}");
                }
            }

            return false;
        }

        private static void SeedStudents(DatabaseContextEntityFramework context)
        {
            var students = new List<Student>()
            {
                new Student() { Surname = "test1", Name = "test1", Doublename = "test1" },
                new Student() { Surname = "test2", Name = "test2", Doublename = "test2" }
            };

            context.Students.AddRange(students);
            context.SaveChanges();
        }

        private static void SeedCurators(DatabaseContextEntityFramework context)
        {
            var curators = new List<Curator>()
            {
                new Curator() { Surname = "test1", Name = "test1", Doublename = "test1", Phone = "+375-29-557-06-67" },
                new Curator() { Surname = "test2", Name = "test2", Doublename = "test2", Phone = "+375-29-557-06-77" }
            };

            context.Curators.AddRange(curators);
            context.SaveChanges();
        }

        private static void SeedSpecializations(DatabaseContextEntityFramework context)
        {
            var specializations = new List<Specialization>()
            {
                new Specialization() { Name = "test1" },
                new Specialization() { Name = "test2" }
            };

            context.Specializations.AddRange(specializations);
            context.SaveChanges();
        }

        private static void SeedGroups(DatabaseContextEntityFramework context)
        {
            var groups = new List<Group>()
            {
                new Group() { Name = "test1", SpecializationId = 1 },
                new Group() { Name = "test2", SpecializationId = 2 }
            };

            context.Groups.AddRange(groups);
            context.SaveChanges();
        }

        private static void SeedFaculties(DatabaseContextEntityFramework context)
        {
            var faculties = new List<DataAccessLayer.Models.Faculty>()
            {
                new DataAccessLayer.Models.Faculty()
                {
                    StartDateEducation = DateTime.Now,
                    CountYearEducation = 4,
                    CuratorId = 1,
                    GroupId = 1,
                    StudentId = 1
                },
                new DataAccessLayer.Models.Faculty()
                {
                    StartDateEducation = DateTime.Now,
                    CountYearEducation = 5,
                    CuratorId = 2,
                    GroupId = 2,
                    StudentId = 2
                }
            };

            context.Faculties.AddRange(faculties);
            context.SaveChanges();
        }
    }
}
