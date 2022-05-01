using System;
using System.Collections.Generic;

namespace Faculty.Common.Dto.User
{
    /// <summary>
    /// Model User data transfer object.
    /// </summary>
    public class UserDto
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
