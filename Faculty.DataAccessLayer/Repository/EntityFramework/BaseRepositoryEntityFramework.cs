using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Faculty.DataAccessLayer.Repository.EntityFramework
{
    /// <summary>
    /// Implementing the repository pattern (Entity Framework).
    /// </summary>
    /// <typeparam name="T">Entity model.</typeparam>
    public class BaseRepositoryEntityFramework<T> : IRepository<T> where T : class, new()
    {
        /// <summary>
        /// Private field to store the database context for executing operations.
        /// </summary>
        private readonly DbContext _context;

        /// <summary>
        /// Constructor to initialize the database context.
        /// </summary>
        /// <param name="context">Database context.</param>
        public BaseRepositoryEntityFramework(DbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method for adding data to the database (Entity Framework implementation). 
        /// </summary>
        /// <param name="entity">Entity model.</param>
        /// <returns>Count added models.</returns>
        public T Insert(T entity)
        {
            if (entity is null) return null;
            _context.Set<T>().Add(entity);
            try
            {
                _context.SaveChanges();
                return entity;
            }
            catch (DbUpdateConcurrencyException)
            {

            }

            return null;
        }

        /// <summary>
        /// Method for changing data to the database (Entity Framework implementation).
        /// </summary>
        /// <param name="entity">Entity model.</param>
        /// <returns>Count changed models.</returns>
        public int Update(T entity)
        {
            if (entity is null) return 0;
            _context.Set<T>().Attach(entity);
            _context.Set<T>().Update(entity);
            try
            {
                var count = _context.SaveChanges();
                return count;
            }
            catch (DbUpdateConcurrencyException)
            {

            }

            return 0;
        }

        /// <summary>
        /// Method for removing data to the database (Entity Framework implementation).
        /// </summary>
        /// <param name="entity">Entity model.</param>
        /// <returns>Count deleted models.</returns>
        public int Delete(T entity)
        {
            if (entity is null) return 0;
            _context.Set<T>().Attach(entity);
            _context.Set<T>().Remove(entity);
            try
            {
                var count = _context.SaveChanges();
                return count;
            }
            catch (DbUpdateConcurrencyException)
            {

            }

            return 0;
        }

        /// <summary>
        /// Method for selecting data from a database (Entity Framework implementation).
        /// </summary>
        /// <returns>Lots of Entity objects returned.</returns>
        public IEnumerable<T> GetAll() 
        {
            return _context.Set<T>().ToList();
        }

        /// <summary>
        /// Method for selecting data from a database (Entity Framework implementation).
        /// </summary>
        /// <param name="id">Unique identificator model.</param>
        /// <returns>Entity object.</returns>
        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }
    }
}
