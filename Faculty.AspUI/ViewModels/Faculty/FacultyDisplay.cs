using System;

namespace Faculty.AspUI.ViewModels.Faculty
{
    /// <summary>
    /// ViewModel Faculty.
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
        /// Surname student.
        /// </summary>
        public string StudentSurname { get; set; }

        /// <summary>
        /// Name group.
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Surname curator.
        /// </summary>
        public string CuratorSurname { get; set; }
    }
}
