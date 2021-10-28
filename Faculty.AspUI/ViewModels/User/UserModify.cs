using System;
using Faculty.AspUI.Tools;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Faculty.AspUI.ViewModels.User
{
    public class UserModify
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "LoginRequired")]
        [StringLength(30, MinimumLength = 10, ErrorMessage = "LoginLength")]
        public string Login { get; set; }

        [RoleAttribute(ErrorMessage = "RoleRequirement")]
        public IList<string> Roles { get; set; }

        [Required(ErrorMessage = "DateRequired")]
        public DateTime? Birthday { get; set; }
    }
}
