using System.ComponentModel.DataAnnotations;

namespace Faculty.ResourceServer.Models.Group
{
    /// <summary>
    /// ViewModel Group.
    /// </summary>
    public class GroupAdd
    {
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
    }
}
