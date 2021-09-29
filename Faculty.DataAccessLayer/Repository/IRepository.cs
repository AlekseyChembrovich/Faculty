using System.Collections.Generic;

namespace Faculty.DataAccessLayer.Repository
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
        /// <returns>Count added models.</returns>
        int Insert(T entity);

        /// <summary>
        /// Method for changing data to the database.
        /// </summary>
        /// <param name="entity">Entity model.</param>
        /// <returns>Count changed models.</returns>
        int Update(T entity);

        /// <summary>
        /// Method for removing data to the database.
        /// </summary>
        /// <param name="entity">Entity model.</param>
        /// <returns>Count deleted models.</returns>
        int Delete(T entity);

        /// <summary>
        /// Method for selecting data from a database.
        /// </summary>
        /// <returns>Lots of Entity objects returned.</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Method for selecting data from a database.
        /// </summary>
        /// <param name="id">Unique identificator model.</param>
        /// <returns>Entity object.</returns>
        T GetById(int id);
    }
}
