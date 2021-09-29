using System;
using System.Linq;
using System.Collections.Generic;
using Faculty.DataAccessLayer.Models;
using Faculty.DataAccessLayer.Repository.EntityFramework;

namespace Faculty.IntegrationTests
{
    public class DatabaseFiller
    {
        /// <summary>
        /// Private field to store the database context for executing operations.
        /// </summary>
        private readonly DatabaseContextEntityFramework _context;
        
        /// <summary>
        /// Constructor to initialize the database context.
        /// </summary>
        /// <param name="context">Database context.</param>
        public DatabaseFiller(DatabaseContextEntityFramework context)
        {
            _context = context;
        }

        /// <summary>
        /// Filling the database by overrunning the test.
        /// </summary>
        public void Fill()
        {
            var studentsToFill = new List<Student>
            {
                new() { Id = 1, Surname = "test1", Name = "test1", Doublename = "test1" },
                new() { Id = 2, Surname = "test2", Name = "test2", Doublename = "test2" },
                new() { Id = 3, Surname = "test3", Name = "test3", Doublename = "test3" },
                new() { Id = 4, Surname = "test4", Name = "test4", Doublename = "test4" },
                new() { Id = 5, Surname = "test5", Name = "test5", Doublename = "test5" }
            };
            _context.Students.AddRange(studentsToFill);
            var specializationsToFill = new List<Specialization>
            {
                new() { Id = 1, Name = "test1" },
                new() { Id = 2, Name = "test2" },
                new() { Id = 3, Name = "test3" },
                new() { Id = 4, Name = "test4" },
                new() { Id = 5, Name = "test5" }
            };
            _context.Specializations.AddRange(specializationsToFill);
            var curatorsToFill = new List<Curator>
            {
                new() { Id = 1, Surname = "test1", Name = "test1", Doublename = "test1", Phone = "+375-33-111-11-11" },
                new() { Id = 2, Surname = "test2", Name = "test2", Doublename = "test2", Phone = "+375-33-222-22-22" },
                new() { Id = 3, Surname = "test3", Name = "test3", Doublename = "test3", Phone = "+375-33-333-33-33" },
                new() { Id = 4, Surname = "test4", Name = "test4", Doublename = "test4", Phone = "+375-33-444-44-44" },
                new() { Id = 5, Surname = "test5", Name = "test5", Doublename = "test5", Phone = "+375-33-555-55-55" }
            };
            _context.Curators.AddRange(curatorsToFill);
            var groupsExisting = new List<Group>
            {
                new() { Name = "test1", SpecializationId = 1 },
                new() { Name = "test2", SpecializationId = 2 },
                new() { Name = "test3", SpecializationId = 3 },
                new() { Name = "test4", SpecializationId = 4 },
                new() { Name = "test5", SpecializationId = 5 }
            };
            _context.Groups.AddRange(groupsExisting);
            var facultiesToFill = new List<DataAccessLayer.Models.Faculty>
            {
                new() { StartDateEducation = new DateTime(2021, 09, 01), CountYearEducation = 5, StudentId = 1, GroupId = 1, CuratorId = 1 },
                new() { StartDateEducation = new DateTime(2021, 09, 01), CountYearEducation = 4, StudentId = 2, GroupId = 1, CuratorId = 1 },
                new() { StartDateEducation = new DateTime(2021, 09, 01), CountYearEducation = 5, StudentId = 3, GroupId = 2, CuratorId = 2 },
                new() { StartDateEducation = new DateTime(2021, 09, 01), CountYearEducation = 4, StudentId = 2, GroupId = 3, CuratorId = 3 },
                new() { StartDateEducation = new DateTime(2021, 09, 01), CountYearEducation = 5, StudentId = 1, GroupId = 1, CuratorId = 1 }
            };
            _context.Faculties.AddRange(facultiesToFill);
            _context.SaveChanges();
        }

        /// <summary>
        /// Cleaning up the database after executing a test.
        /// </summary>
        public void Clear()
        {
            var facultiesToFill = _context.Set<DataAccessLayer.Models.Faculty>().ToList();
            _context.Faculties.RemoveRange(facultiesToFill);
            var groupsExisting = _context.Set<Group>().ToList();
            _context.Groups.RemoveRange(groupsExisting);
            var studentsToFill = _context.Set<Student>().ToList();
            _context.Students.RemoveRange(studentsToFill);
            var specializationsToFill = _context.Set<Specialization>().ToList();
            _context.Specializations.RemoveRange(specializationsToFill);
            var curatorsToFill = _context.Set<Curator>().ToList();
            _context.Curators.RemoveRange(curatorsToFill);
            _context.SaveChanges();
        }
    }
}
