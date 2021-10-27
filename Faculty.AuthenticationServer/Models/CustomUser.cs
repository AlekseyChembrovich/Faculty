using Microsoft.AspNetCore.Identity;
using System;

namespace Faculty.AuthenticationServer.Models
{
    public class CustomUser : IdentityUser
    {
        public DateTime Birthday { get; set; }
    }
}
