using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Faculty.AuthenticationServer.Models
{
    public sealed class CustomIdentityContext : IdentityDbContext<CustomUser>
    {
        public CustomIdentityContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("auth");
            base.OnModelCreating(modelBuilder);
        }
    }
}
