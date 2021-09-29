using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Faculty.DataAccessLayer.RepositoryEntityFramework
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
        protected readonly DbContext Context;

        /// <summary>
        /// Constructor to initialize the database context.
        /// </summary>
        /// <param name="context">Database context.</param>
        public BaseRepositoryEntityFramework(DbContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Method for adding data to the database (Entity Framework implementation). 
        /// </summary>
        /// <param name="entity">Entity model.</param>
        /// <returns>Count added models.</returns>
        public int Insert(T entity)
        {
            if (entity is null) return 0;
            Context.Set<T>().Add(entity);
            try
            {
                var count = Context.SaveChanges();
                return count;
            }
            catch (DbUpdateConcurrencyException)
            {

            }

            return 0;
        }

        /// <summary>
        /// Method for changing data to the database (Entity Framework implementation).
        /// </summary>
        /// <param name="entity">Entity model.</param>
        /// <returns>Count changed models.</returns>
        public int Update(T entity)
        {
            if (entity is null) return 0;
            Context.Set<T>().Attach(entity);
            Context.Set<T>().Update(entity);
            try
            {
                var count = Context.SaveChanges();
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
            Context.Set<T>().Attach(entity);
            Context.Set<T>().Remove(entity);
            try
            {
                var count = Context.SaveChanges();
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
            return Context.Set<T>().ToList();
        }

        /// <summary>
        /// Method for selecting data from a database (Entity Framework implementation).
        /// </summary>
        /// <param name="id">Unique identificator model.</param>
        /// <returns>Entity object.</returns>
        public T GetById(int id)
        {
            return Context.Set<T>().Find(id);
        }
    }
}
