using System.ComponentModel.DataAnnotations;

namespace Faculty.AspUI.ViewModels.LoginRegister
{
    /// <summary>
    /// ViewModel User for login.
    /// </summary>
    public class LoginUser
    {
        /// <summary>
        /// User login.
        /// </summary>
        [Required(ErrorMessage = "LoginRequired")]
        [StringLength(30, MinimumLength = 10, ErrorMessage = "LoginLength")]
        public string Login { get; set; }

        /// <summary>
        /// User password.
        /// </summary>
        [Required(ErrorMessage = "PasswordRequired")]
        [DataType(DataType.Password, ErrorMessage = "PasswordType")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "PasswordLength")]
        public string Password { get; set; }
    }
}
