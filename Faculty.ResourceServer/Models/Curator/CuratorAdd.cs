using System.ComponentModel.DataAnnotations;

namespace Faculty.ResourceServer.Models.Curator
{
    /// <summary>
    /// ViewModel Curator.
    /// </summary>
    public class CuratorAdd
    {
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
    }
}
