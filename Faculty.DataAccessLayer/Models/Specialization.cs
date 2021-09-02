using System.Collections.Generic;

namespace Faculty.DataAccessLayer.Models
{
    /// <summary>
    /// Entity Specialization.
    /// </summary>
    public class Specialization
    {
        /// <summary>
        /// Unique identificator specialization.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name specialization.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Groups specialization.
        /// </summary>
        public virtual ICollection<Group> Groups { get; set; }

        /// <summary>
        /// Constructor for init Groups of this Specialization.
        /// </summary>
        public Specialization()
        {
            Groups = new HashSet<Group>();
        }
    }
}
