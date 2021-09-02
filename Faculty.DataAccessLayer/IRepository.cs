using System.Collections.Generic;

namespace Faculty.DataAccessLayer
{
    /// <summary>
    /// Interface representing an abstraction for implementing the repository pattern.
    /// </summary>
    /// <typeparam name="T">Database model.</typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Method for adding data to the database.
        /// </summary>
        /// <param name="entity">Entity model.</param>
        void Insert(T entity);

        /// <summary>
        /// Method for changing data to the database.
        /// </summary>
        /// <param name="entity">Entity model.</param>
        void Update(T entity);

        /// <summary>
        /// Method for removing data to the database.
        /// </summary>
        /// <param name="entity">Entity model.</param>
        void Delete(T entity);

        /// <summary>
        /// Method for selecting data from a database.
        /// </summary>
        /// <returns>Lots of Entity objects returned.</returns>
        IEnumerable<T> GetAll();
    }
}
