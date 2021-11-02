using System;
using Microsoft.AspNetCore.Identity;

namespace Faculty.AuthenticationServer.Models
{
    /// <summary>
    /// Identity custom user model.
    /// </summary>
    public class CustomUser : IdentityUser
    {
        /// <summary>
        /// User birthday.
        /// </summary>
        public DateTime Birthday { get; set; }
    }
}
