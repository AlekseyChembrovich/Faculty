using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Faculty.DataAccessLayer.RepositoryAdo
{
    /// <summary>
    /// Implementing the repository pattern (Ado).
    /// </summary>
    /// <typeparam name="T">Entity model.</typeparam>
    public abstract class BaseRepositoryAdo<T> : IRepository<T>
    {
        /// <summary>
        /// Field for establishing a connection to the database.
        /// </summary>
        private readonly DatabaseContextAdo _context;

        /// <summary>
        /// Connection context initialization constructor.
        /// </summary>
        /// <param name="context">Database connection.</param>
        protected BaseRepositoryAdo(DatabaseContextAdo context)
        {
            _context = context;
        }

        /// <summary>
        /// Method for adding data to the database (Ado implementation).
        /// </summary>
        /// <param name="entity">Entity model.</param>
        public void Insert(T entity)
        {
            if (entity is null) return;
            using (var command = _context.SqlConnection.CreateCommand())
            {
                SetParametersInsertCommand(entity, command);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Method for changing data to the database (Ado implementation).
        /// </summary>
        /// <param name="entity">Entity model.</param>
        public void Update(T entity)
        {
            if (entity is null) return;
            using (var command = _context.SqlConnection.CreateCommand())
            {
                SetParametersUpdateCommand(entity, command);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Method for removing data to the database (Ado implementation).
        /// </summary>
        /// <param name="entity">Entity model.</param>
        public void Delete(T entity)
        {
            if (entity is null) return;
            using (var command = _context.SqlConnection.CreateCommand())
            {
                SetParametersDeleteCommand(entity, command);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Method for selecting data from a database (Ado implementation).
        /// </summary>
        /// <returns>Lots of Entity objects returned.</returns>
        public IEnumerable<T> GetAll()
        {
            List<T> result = null;
            using (var command = _context.SqlConnection.CreateCommand())
            {
                result = SetParametersSelectCommand(command);
            }
            return result;
        }

        /// <summary>
        /// Method for setting the add request parameters.
        /// </summary>
        /// <param name="entity">Entity object.</param>
        /// <param name="command">Object SqlCommand.</param>
        protected abstract void SetParametersInsertCommand(T entity, SqlCommand command);

        /// <summary>
        /// Method for setting change request parameters.
        /// </summary>
        /// <param name="entity">Entity object.</param>
        /// <param name="command">Object SqlCommand.</param>
        protected abstract void SetParametersUpdateCommand(T entity, SqlCommand command);

        /// <summary>
        /// Method for setting delete request parameters.
        /// </summary>
        /// <param name="entity">Entity object.</param>
        /// <param name="command">Object SqlCommand.</param>
        protected abstract void SetParametersDeleteCommand(T entity, SqlCommand command);

        /// <summary>
        /// Method for setting query parameters for fetching all data.
        /// </summary>
        /// <param name="command">Object SqlCommand.</param>
        /// <returns>List of Entity objects.</returns>
        protected abstract List<T> SetParametersSelectCommand(SqlCommand command);
    }
}