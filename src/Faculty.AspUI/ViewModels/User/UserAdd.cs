using System;
using Faculty.AspUI.Tools;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Faculty.AspUI.ViewModels.User
{
    /// <summary>
    /// ViewModel User for add.
    /// </summary>
    public class UserAdd
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

        /// <summary>
        /// User roles.
        /// </summary>
        [Role(ErrorMessage = "RoleRequirement")]
        public IList<string> Roles { get; set; }

        /// <summary>
        /// User birthday.
        /// </summary>
        [Required(ErrorMessage = "DateRequired")]
        public DateTime? Birthday { get; set; }
    }
}
