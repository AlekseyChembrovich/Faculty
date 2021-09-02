using System.Collections.Generic;

namespace Faculty.DataAccessLayer.Models
{
    public class Specialization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Group> Groups { get; set; }

        public Specialization()
        {
            Groups = new HashSet<Group>();
        }
    }
}
