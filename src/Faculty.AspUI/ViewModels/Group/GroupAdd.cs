using System.ComponentModel.DataAnnotations;

namespace Faculty.AspUI.ViewModels.Group
{
    /// <summary>
    /// ViewModel Group.
    /// </summary>
    public class GroupAdd
    {
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
