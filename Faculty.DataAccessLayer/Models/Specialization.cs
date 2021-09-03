using System.Collections.Generic;

namespace Faculty.DataAccessLayer.Models
{
    public class Specialization
    {
        public Specialization()
        {
            Groups = new HashSet<Group>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Group> Groups { get; set; }
    }
}
