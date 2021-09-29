using System;

namespace Faculty.AspUI.ViewModels.Faculty
{
    /// <summary>
    /// Entity Faculty.
    /// </summary>
    public class FacultyDisplay
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
        public string StudentSurname { get; set; }

        /// <summary>
        /// Foreign key for group entity.
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Foreign key for curator entity.
        /// </summary>
        public string CuratorSurname { get; set; }
    }
}
