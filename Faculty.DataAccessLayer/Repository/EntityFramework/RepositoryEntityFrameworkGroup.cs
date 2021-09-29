using System.Linq;
using System.Collections.Generic;
using Faculty.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Faculty.DataAccessLayer.RepositoryEntityFramework
{
    public class RepositoryEntityFrameworkGroup : BaseRepositoryEntityFramework<Group>, IRepositoryGroup
    {
        /// <summary>
        /// Constructor to initialize the database context.
        /// </summary>
        /// <param name="context">Database context.</param>
        public RepositoryEntityFrameworkGroup(DbContext context) : base(context)
        {
        }

        /// <summary>
        /// Method for selecting data from a database and include foreign key.
        /// </summary>
        /// <returns>Lots of Entity objects returned.</returns>
        public IEnumerable<Group> GetAllIncludeForeignKey()
        {
            return Context.Set<Group>().Include(x => x.Specialization).ToList();
        }
    }
}
