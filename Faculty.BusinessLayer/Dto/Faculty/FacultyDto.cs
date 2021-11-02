using System;

namespace Faculty.BusinessLayer.Dto.Faculty
{
    /// <summary>
    /// Dto Faculty.
    /// </summary>
    public class FacultyDto
    {
        /// <summary>
        /// Unique identificator faculty.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Date start education student. 
        /// </summary>
        public DateTime StartDateEducation { get; set; }

        /// <summary>
        /// Count year education student.
        /// </summary>
        public int CountYearEducation { get; set; }

        /// <summary>
        /// Foreign key for student entity.
        /// </summary>
        public int StudentId { get; set; }

        /// <summary>
        /// Foreign key for group entity.
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// Foreign key for curator entity.
        /// </summary>
        public int CuratorId { get; set; }
    }
}
