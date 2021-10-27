using System;
using System.Collections.Generic;

namespace Faculty.AspUI.ViewModels.User
{
    public class UserDisplay
    {
        public string Id { get; set; }

        public string Login { get; set; }

        public IList<string> Roles { get; set; }

        public DateTime Birthday { get; set; }
    }
}
