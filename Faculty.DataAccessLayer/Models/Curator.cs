using System.Collections.Generic;

namespace Faculty.DataAccessLayer.Models
{
    public class Curator
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Doublename { get; set; }
        public string Phone { get; set; }
        public virtual ICollection<Faculty> Faculties { get; set; }

        public Curator()
        {
            Faculties = new HashSet<Faculty>();
        }
    }
}
