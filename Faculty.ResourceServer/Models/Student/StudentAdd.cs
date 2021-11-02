using System.ComponentModel.DataAnnotations;

namespace Faculty.ResourceServer.Models.Student
{
    /// <summary>
    /// ViewModel Student.
    /// </summary>
    public class StudentAdd
    {
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
    }
}
