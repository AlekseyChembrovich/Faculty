using System;
using System.ComponentModel.DataAnnotations;

namespace Faculty.ResourceServer.Models.Faculty
{
    /// <summary>
    /// ViewModel Faculty.
    /// </summary>
    public class FacultyAdd
    {
        /// <summary>
        /// Date start education student. 
        /// </summary>
        [Required]
        public DateTime? StartDateEducation { get; set; }

        /// <summary>
        /// Count year education student.
        /// </summary>
        [Required]
        [Range(3, 5)]
        public int? CountYearEducation { get; set; }

        /// <summary>
        /// Foreign key for student entity.
        /// </summary>
        [Required]
        public int StudentId { get; set; }

        /// <summary>
        /// Foreign key for group entity.
        /// </summary>
        [Required]
        public int GroupId { get; set; }

        /// <summary>
        /// Foreign key for curator entity.
        /// </summary>
        [Required]
        public int CuratorId { get; set; }
    }
}
