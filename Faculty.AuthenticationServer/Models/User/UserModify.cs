using System.Collections.Generic;

namespace Faculty.AuthenticationServer.Models.User
{
    public class UserModify
    {
        public string Id { get; set; }

        public string Login { get; set; }

        public IList<string> Roles { get; set; }
    }
}
