using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Faculty.BusinessLayer.ModelsDTO
{
    /// <summary>
    /// Entity Group.
    /// </summary>
    public class GroupDTO
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
        public SpecializationDTO Specialization { get; set; }

        /// <summary>
        /// Faculties group.
        /// </summary>
        public ICollection<FacultyDTO> Faculties { get; set; }

        /// <summary>
        /// Constructor for init Faculties of this Group.
        /// </summary>
        public GroupDTO()
        {
            Faculties = new HashSet<FacultyDTO>();
        }
    }
}
