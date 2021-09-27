using System;

namespace Faculty.BusinessLayer.ModelsDto
{
    /// <summary>
    /// Entity Faculty.
    /// </summary>
    public class DisplayFacultyDto
    {
        /// <summary>
        /// Unique identificator curator.
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
        /// Entity curator.
        /// </summary>
        public string CuratorSurname { get; set; }

        /// <summary>
        /// Entity group.
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Entity student.
        /// </summary>
        public string StudentSurname { get; set; }
    }
}
