using System;
using System.ComponentModel.DataAnnotations;

namespace Faculty.AspUI.ViewModels.LoginRegister
{
    /// <summary>
    /// ViewModel User for register.
    /// </summary>
    public class RegisterUser : LoginUser
    {
        /// <summary>
        /// Password for confirm.
        /// </summary>
        [Required(ErrorMessage = "ConfirmPasswordRequired")]
        [Compare("Password", ErrorMessage = "ComparePassword")]
        [DataType(DataType.Password, ErrorMessage = "PasswordType")]
        [field: NonSerialized]
        public string PasswordConfirm { get; set; }
    }
}
