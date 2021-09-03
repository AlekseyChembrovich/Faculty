using Faculty.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Faculty.DataAccessLayer.RepositoryEntityFramework
{
    public class DatabaseContextEntityFramework : DbContext
    {
        public virtual DbSet<Curator> Curators { get; set; }
        public virtual DbSet<Models.Faculty> Faculties { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Specialization> Specialozations { get; set; }
        public virtual DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-ALEKSEY\\SQLEXPRESS;Initial Catalog=MbTask;Integrated Security=True");
            }
        }
    }
}
