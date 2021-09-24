using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Faculty.BusinessLayer.ModelsDTO
{
    /// <summary>
    /// Entity Specialization.
    /// </summary>
    public class SpecializationDTO
    {
        /// <summary>
        /// Unique identificator specialization.
        /// </summary>
        [Key]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Name specialization.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Groups specialization.
        /// </summary>
        public virtual ICollection<GroupDTO> Groups { get; set; }

        /// <summary>
        /// Constructor for init Groups of this Specialization.
        /// </summary>
        public SpecializationDTO()
        {
            Groups = new HashSet<GroupDTO>();
        }
    }
}
