using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Faculty.BusinessLayer.ModelsDTO
{
    /// <summary>
    /// Entity Student.
    /// </summary>
    public class StudentDTO
    {
        /// <summary>
        /// Unique identificator specialization.
        /// </summary>
        [Key]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Surname student.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Surname { get; set; }

        /// <summary>
        /// Name student.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Doublename student.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Doublename { get; set; }

        /// <summary>
        /// Faculties Student.
        /// </summary>
        public virtual ICollection<FacultyDTO> Faculties { get; set; }

        /// <summary>
        /// Constructor for init Faculties of this Student.
        /// </summary>
        public StudentDTO()
        {
            Faculties = new HashSet<FacultyDTO>();
        }
    }
}
