using System.Collections.Generic;

namespace Faculty.DataAccessLayer.Models
{
    public class Group
    {
        public Group()
        {
            Faculties = new HashSet<Faculty>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? SpecializationId { get; set; }

        public virtual Specialization Specialization { get; set; }
        public virtual ICollection<Faculty> Faculties { get; set; }
    }
}
