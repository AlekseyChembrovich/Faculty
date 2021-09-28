using System.ComponentModel.DataAnnotations;

namespace Faculty.AspUI.Models
{
    /// <summary>
    /// Entity Specialization.
    /// </summary>
    public class SpecializationModify
    {
        /// <summary>
        /// Unique identificator specialization.
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Name specialization.
        /// </summary>
        [Required(ErrorMessage = "NameRequired")]
        [StringLength(50, ErrorMessage = "NameLength")]
        public string Name { get; set; }
    }
}
