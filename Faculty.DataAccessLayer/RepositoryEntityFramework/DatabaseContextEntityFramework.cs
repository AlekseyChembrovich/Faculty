using Microsoft.EntityFrameworkCore;
using Faculty.DataAccessLayer.Models;

namespace Faculty.DataAccessLayer.RepositoryEntityFramework
{
    /// <summary>
    /// Context database.
    /// </summary>
    public class DatabaseContextEntityFramework : DbContext
    {
        public virtual DbSet<Curator> Curators { get; set; }
        public virtual DbSet<Models.Faculty> Faculties { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Specialization> Specializations { get; set; }
        public virtual DbSet<Student> Students { get; set; }

        /// <summary>
        /// Method for connection at a database.
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-ALEKSEY\\SQLEXPRESS;Initial Catalog=MbTask;Integrated Security=True");
            }
        }
    }
}
