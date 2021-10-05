using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Faculty.AuthenticationServer.Models
{
    public sealed class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
