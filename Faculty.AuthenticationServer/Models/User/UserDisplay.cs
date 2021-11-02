using System;
using System.Collections.Generic;

namespace Faculty.AuthenticationServer.Models.User
{
    /// <summary>
    /// Model User for display.
    /// </summary>
    public class UserDisplay
    {
        /// <summary>
        /// User unique id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// User login.
        /// </summary>
        public string Login { get; set; }

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
