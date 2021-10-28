using System;

namespace Faculty.AuthenticationServer.Models.LoginRegister
{
    public class RegisterUser
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public DateTime Birthday { get; set; }
    }
}
