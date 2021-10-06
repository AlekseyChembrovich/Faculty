using Faculty.AspUI.Validation;
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

        [OneAndMore(ErrorMessage = "RoleOneAndMore")]
        public IList<string> Roles { get; set; }
    }
}
