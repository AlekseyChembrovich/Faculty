using Microsoft.EntityFrameworkCore;
using Faculty.DataAccessLayer.Models;

namespace Faculty.DataAccessLayer.Repository.EntityFramework
{
    /// <summary>
    /// Context database.
    /// </summary>
    public class DatabaseContextEntityFramework : DbContext
    {
        /// <summary>
        /// Set of objects entity Curator.
        /// </summary>
        public virtual DbSet<Curator> Curators { get; set; }

        /// <summary>
        /// Set of objects entity Faculty.
        /// </summary>
        public virtual DbSet<Models.Faculty> Faculties { get; set; }

        /// <summary>
        /// Set of objects entity Group.
        /// </summary>
        public virtual DbSet<Group> Groups { get; set; }

        /// <summary>
        /// Set of objects entity Specialization.
        /// </summary>
        public virtual DbSet<Specialization> Specializations { get; set; }

        /// <summary>
        /// Set of objects entity Student.
        /// </summary>
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

        /// <summary>
        /// Constructor for init database context options.
        /// </summary>
        /// <param name="options"></param>
        public DatabaseContextEntityFramework(DbContextOptions options) : base(options) { }

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public DatabaseContextEntityFramework() { }
    }
}
