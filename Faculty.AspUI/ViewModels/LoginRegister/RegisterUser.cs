using System;
using System.ComponentModel.DataAnnotations;

namespace Faculty.AspUI.ViewModels.LoginRegister
{
    public class RegisterUser : LoginUser
    {
        [Required(ErrorMessage = "ConfirmPasswordRequired")]
        [Compare("Password", ErrorMessage = "ComparePassword")]
        [DataType(DataType.Password, ErrorMessage = "PasswordType")]
        [field: NonSerialized]
        public string PasswordConfirm { get; set; }
    }
}
