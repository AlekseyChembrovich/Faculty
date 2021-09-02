using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Faculty.DataAccessLayer.RepositoryAdo
{
    public abstract class BaseRepositoryAdo<T> : IRepository<T>
    {
        private readonly DatabaseContextAdo _context;

        protected BaseRepositoryAdo(DatabaseContextAdo context)
        {
            _context = context;
        }

        public void Insert(T entity)
        {
            if (entity is null) return;
            using (var command = _context.SqlConnection.CreateCommand())
            {
                SetParametersInsertCommand(entity, command);
                command.ExecuteNonQuery();
            }
        }

        public void Update(T entity)
        {
            if (entity is null) return;
            using (var command = _context.SqlConnection.CreateCommand())
            {
                SetParametersUpdateCommand(entity, command);
                command.ExecuteNonQuery();
            }
        }

        public void Delete(T entity)
        {
            if (entity is null) return;
            using (var command = _context.SqlConnection.CreateCommand())
            {
                SetParametersDeleteCommand(entity, command);
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<T> GetAll()
        {
            List<T> result = null;
            using (var command = _context.SqlConnection.CreateCommand())
            {
                result = SetParametersSelectCommand(command);
            }
            return result;
        }

        protected abstract void SetParametersInsertCommand(T entity, SqlCommand command);
        protected abstract void SetParametersUpdateCommand(T entity, SqlCommand command);
        protected abstract void SetParametersDeleteCommand(T entity, SqlCommand command);
        protected abstract List<T> SetParametersSelectCommand(SqlCommand command);
    }
}