using System;
using System.Collections.Generic;

namespace Faculty.AuthenticationServer.Models.User
{
    /// <summary>
    /// Model User for add.
    /// </summary>
    public class UserAdd
    {
        /// <summary>
        /// User login.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// User password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// User roles.
        /// </summary>
        public IList<string> Roles { get; set; }

        /// <summary>
        /// User birthday.
        /// </summary>
        public DateTime Birthday { get; set; }
    }
}
