using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Faculty.DataAccessLayer.Models
{
    /// <summary>
    /// Entity Student.
    /// </summary>
    public class Student
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
        public virtual ICollection<Faculty> Faculties { get; set; }

        /// <summary>
        /// Constructor for init Faculties of this Student.
        /// </summary>
        public Student()
        {
            Faculties = new HashSet<Faculty>();
        }
    }
}
