using System.ComponentModel.DataAnnotations;

namespace Faculty.ResourceServer.Models.Specialization
{
    /// <summary>
    /// ViewModel Specialization.
    /// </summary>
    public class SpecializationAdd
    {
        /// <summary>
        /// Name specialization.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
