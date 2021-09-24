using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Faculty.BusinessLayer.ModelsDTO
{
    /// <summary>
    /// Entity Curator.
    /// </summary>
    public class CuratorDTO
    {
        /// <summary>
        /// Unique identificator curator.
        /// </summary>
        [Key]
        [Required]
        public int Id { get; set; }

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

        /// <summary>
        /// Faculties curator.
        /// </summary>
        public virtual ICollection<FacultyDTO> Faculties { get; set; }

        /// <summary>
        /// Constructor for init Faculties of this Curator.
        /// </summary>
        public CuratorDTO()
        {
            Faculties = new HashSet<FacultyDTO>();
        }
    }
}
