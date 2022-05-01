using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Key]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Surname curator.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Surname { get; set; }

        /// <summary>
        /// Name curator.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Doublename curator.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Doublename { get; set; }

        /// <summary>
        /// Phone curator.
        /// </summary>
        [Required]
        [Phone]
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
