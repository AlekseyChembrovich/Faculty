using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Faculty.DataAccessLayer.Models;
using Faculty.DataAccessLayer.Repository.EntityFramework.Interfaces;

namespace Faculty.DataAccessLayer.Repository.EntityFramework
{
    public class RepositoryEntityFrameworkGroup : BaseRepositoryEntityFramework<Group>, IRepositoryGroup
    {
        /// <summary>
        /// Constructor to initialize the database context.
        /// </summary>
        /// <param name="context">Database context.</param>
        public RepositoryEntityFrameworkGroup(DatabaseContextEntityFramework context) : base(context)
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
