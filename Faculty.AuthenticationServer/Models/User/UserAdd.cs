using System.Collections.Generic;

namespace Faculty.AuthenticationServer.Models.User
{
    public class UserAdd
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public IList<string> Roles { get; set; }
    }
}
