using System;
using System.ComponentModel.DataAnnotations;

namespace Faculty.AspUI.ViewModels.LoginRegister
{
    public class RegisterUser
    {
        [Required(ErrorMessage = "LoginRequired")]
        [StringLength(30, MinimumLength = 10, ErrorMessage = "LoginLength")]
        public string Login { get; set; }

        [Required(ErrorMessage = "PasswordRequired")]
        [DataType(DataType.Password, ErrorMessage = "PasswordType")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "PasswordLength")]
        public string Password { get; set; }

        [Required(ErrorMessage = "ConfirmPasswordRequired")]
        [Compare("Password", ErrorMessage = "ComparePassword")]
        [DataType(DataType.Password, ErrorMessage = "PasswordType")]
        [field: NonSerialized]
        public string PasswordConfirm { get; set; }

        [Required(ErrorMessage = "DateRequired")]
        public DateTime? Birthday { get; set; }
    }
}
