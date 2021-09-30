using System.ComponentModel.DataAnnotations;

namespace Faculty.AspUI.ViewModels.Student
{
    /// <summary>
    /// ViewModel Student.
    /// </summary>
    public class StudentAdd
    {
        /// <summary>
        /// Surname student.
        /// </summary>
        [Required(ErrorMessage = "SurnameRequired")]
        [StringLength(50, ErrorMessage = "SurnameLength")]
        public string Surname { get; set; }

        /// <summary>
        /// Name student.
        /// </summary>
        [Required(ErrorMessage = "NameRequired")]
        [StringLength(50, ErrorMessage = "NameLength")]
        public string Name { get; set; }

        /// <summary>
        /// Doublename student.
        /// </summary>
        [Required(ErrorMessage = "DoublenameRequired")]
        [StringLength(50, ErrorMessage = "DoublenameLength")]
        public string Doublename { get; set; }
    }
}
