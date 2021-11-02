using System.ComponentModel.DataAnnotations;

namespace Faculty.AspUI.ViewModels.User
{
    /// <summary>
    /// ViewModel User for modify password.
    /// </summary>
    public class UserModifyPassword
    {
        /// <summary>
        /// User unique id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// User new password.
        /// </summary>
        [Required(ErrorMessage = "PasswordRequired")]
        [DataType(DataType.Password, ErrorMessage = "PasswordType")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "PasswordLength")]
        public string NewPassword { get; set; }
    }
}
