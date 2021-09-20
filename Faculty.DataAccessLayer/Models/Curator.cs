using System.Collections.Generic;

namespace Faculty.DataAccessLayer.Models
{
    /// <summary>
    /// Entity Curator.
    /// </summary>
    public class Curator
    {
        /// <summary>
        /// Unique identificator curator.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Surname curator.
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Name curator.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Doublename curator.
        /// </summary>
        public string Doublename { get; set; }

        /// <summary>
        /// Phone curator.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Faculties curator.
        /// </summary>
        public virtual ICollection<Faculty> Faculties { get; set; }

        /// <summary>
        /// Constructor for init Faculties of this Curator.
        /// </summary>
        public Curator()
        {
            Faculties = new HashSet<Faculty>();
        }
    }
}
