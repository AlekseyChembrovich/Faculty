using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Faculty.DataAccessLayer.RepositoryEntityFramework
{
    public class RepositoryEntityFrameworkImplementation<T> : IRepository<T> where T : class, new()
    {
        private readonly DbContext _context;

        public RepositoryEntityFrameworkImplementation(DbContext context) => _context = context;

        public void Insert(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<T> GetAll() => _context.Set<T>().ToList();
    }
}