using System.ComponentModel.DataAnnotations;

namespace Faculty.AspUI.Models
{
    /// <summary>
    /// Entity Group.
    /// </summary>
    public class GroupModify
    {
        /// <summary>
        /// Unique identificator group.
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Name group.
        /// </summary>
        [Required(ErrorMessage = "NameRequired")]
        [StringLength(50, ErrorMessage = "NameLength")]
        public string Name { get; set; }

        /// <summary>
        /// Foreign key for specialization entity.
        /// </summary>
        [Required(ErrorMessage = "SpecializationRequired")]
        public int SpecializationId { get; set; }
    }
}
