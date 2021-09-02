using System;

namespace Faculty.DataAccessLayer.Models
{
    public class Faculty
    {
        public int Id { get; set; }
        public DateTime? StartDateEducation { get; set; }
        public int? CountYearEducation { get; set; }
        public int? StudentId { get; set; }
        public int? GroupId { get; set; }
        public int? CuratorId { get; set; }
        public virtual Curator Curator { get; set; }
        public virtual Group Group { get; set; }
        public virtual Student Student { get; set; }
    }
}
