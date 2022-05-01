using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Faculty.AuthenticationServer.Models
{
    /// <summary>
    /// Custom identity context.
    /// </summary>
    public sealed class CustomIdentityContext : IdentityDbContext<CustomUser>
    {
        /// <summary>
        /// Constructor for init database context options.
        /// </summary>
        /// <param name="options">Database context option</param>
        public CustomIdentityContext(DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        /// Method for set up auth database scheme.
        /// </summary>
        /// <param name="modelBuilder">Model builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("auth");
            base.OnModelCreating(modelBuilder);
        }
    }
}
