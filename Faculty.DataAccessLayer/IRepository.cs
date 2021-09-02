using System.Collections.Generic;

namespace Faculty.DataAccessLayer
{
    /// <summary>
    /// Interface representing an abstraction for implementing the repository pattern.
    /// </summary>
    /// <typeparam name="T">Database model.</typeparam>
    public interface IRepository<T>
    {
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        IEnumerable<T> GetAll();
    }
}
