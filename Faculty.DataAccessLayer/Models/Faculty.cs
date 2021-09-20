using System;

namespace Faculty.DataAccessLayer.Models
{
    /// <summary>
    /// Entity Faculty.
    /// </summary>
    public class Faculty
    {
        /// <summary>
        /// Unique identificator curator.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Date start education student. 
        /// </summary>
        public DateTime? StartDateEducation { get; set; }

        /// <summary>
        /// Count year education student.
        /// </summary>
        public int? CountYearEducation { get; set; }

        /// <summary>
        /// Foreign key for student entity.
        /// </summary>
        public int? StudentId { get; set; }

        /// <summary>
        /// Foreign key for group entity.
        /// </summary>
        public int? GroupId { get; set; }

        /// <summary>
        /// Foreign key for curator entity.
        /// </summary>
        public int? CuratorId { get; set; }

        /// <summary>
        /// Entity curator.
        /// </summary>
        public virtual Curator Curator { get; set; }

        /// <summary>
        /// Entity group.
        /// </summary>
        public virtual Group Group { get; set; }

        /// <summary>
        /// Entity student.
        /// </summary>
        public virtual Student Student { get; set; }
    }
}
