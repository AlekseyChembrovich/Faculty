using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Faculty.DataAccessLayer.Models
{
    /// <summary>
    /// Entity Group.
    /// </summary>
    public class Group
    {
        /// <summary>
        /// Unique identificator group.
        /// </summary>
        [Key]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Name group.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Foreign key for specialization entity.
        /// </summary>
        [Required]
        public int SpecializationId { get; set; }

        /// <summary>
        /// Entity specialization.
        /// </summary>
        public Specialization Specialization { get; set; }

        /// <summary>
        /// Faculties group.
        /// </summary>
        public ICollection<Faculty> Faculties { get; set; }

        /// <summary>
        /// Constructor for init Faculties of this Group.
        /// </summary>
        public Group()
        {
            Faculties = new HashSet<Faculty>();
        }
    }
}
