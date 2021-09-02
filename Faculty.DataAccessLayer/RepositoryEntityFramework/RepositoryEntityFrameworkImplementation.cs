using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Faculty.DataAccessLayer.RepositoryEntityFramework
{
    /// <summary>
    /// Implementing the repository pattern (Entity Framework).
    /// </summary>
    /// <typeparam name="T">Entity model.</typeparam>
    public class RepositoryEntityFrameworkImplementation<T> : IRepository<T> where T : class, new()
    {
        /// <summary>
        /// Private field to store the database context for executing operations.
        /// </summary>
        private readonly DbContext _context;

        /// <summary>
        /// Constructor to initialize the database context.
        /// </summary>
        /// <param name="context">Database context.</param>
        public RepositoryEntityFrameworkImplementation(DbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method for adding data to the database (Entity Framework implementation). 
        /// </summary>
        /// <param name="entity">Entity model.</param>
        public void Insert(T entity)
        {
            if (entity is null) return;
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        /// <summary>
        /// Method for changing data to the database (Entity Framework implementation).
        /// </summary>
        /// <param name="entity">Entity model.</param>
        public void Update(T entity)
        {
            if (entity is null) return;
            _context.Set<T>().Attach(entity);
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }

        /// <summary>
        /// Method for removing data to the database (Entity Framework implementation).
        /// </summary>
        /// <param name="entity">Entity model.</param>
        public void Delete(T entity)
        {
            if (entity is null) return;
            _context.Set<T>().Attach(entity);
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        /// <summary>
        /// Method for selecting data from a database (Entity Framework implementation).
        /// </summary>
        /// <returns>Lots of Entity objects returned.</returns>
        public IEnumerable<T> GetAll() 
        {
            return _context.Set<T>().ToList();
        }
    }
}
