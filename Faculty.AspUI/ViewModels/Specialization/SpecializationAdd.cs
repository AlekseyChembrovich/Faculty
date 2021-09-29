using System.ComponentModel.DataAnnotations;

namespace Faculty.AspUI.ViewModels.Specialization
{
    /// <summary>
    /// Entity Specialization.
    /// </summary>
    public class SpecializationAdd
    {
        /// <summary>
        /// Name specialization.
        /// </summary>
        [Required(ErrorMessage = "NameRequired")]
        [StringLength(50, ErrorMessage = "NameLength")]
        public string Name { get; set; }
    }
}
