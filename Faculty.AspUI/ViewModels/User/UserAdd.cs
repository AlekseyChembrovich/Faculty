using System;
using Faculty.AspUI.Tools;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Faculty.AspUI.ViewModels.User
{
    public class UserAdd
    {
        [Required(ErrorMessage = "LoginRequired")]
        [StringLength(30, MinimumLength = 10, ErrorMessage = "LoginLength")]
        public string Login { get; set; }

        [Required(ErrorMessage = "PasswordRequired")]
        [DataType(DataType.Password, ErrorMessage = "PasswordType")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "PasswordLength")]
        public string Password { get; set; }

        [RoleAttribute(ErrorMessage = "RoleRequirement")]
        public IList<string> Roles { get; set; }

        [Required(ErrorMessage = "DateRequired")]
        public DateTime? Birthday { get; set; }
    }
}
