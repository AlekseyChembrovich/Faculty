using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Faculty.DataAccessLayer.Repository.EntityFramework.Interfaces;

namespace Faculty.DataAccessLayer.Repository.EntityFramework
{
    public class RepositoryEntityFrameworkFaculty : BaseRepositoryEntityFramework<Models.Faculty>, IRepositoryFaculty
    {
        /// <summary>
        /// Constructor to initialize the database context.
        /// </summary>
        /// <param name="context">Database context.</param>
        public RepositoryEntityFrameworkFaculty(DatabaseContextEntityFramework context) : base(context)
        {
        }

        /// <summary>
        /// Method for selecting data from a database and include foreign key.
        /// </summary>
        /// <returns>Lots of Entity objects returned.</returns>
        public IEnumerable<Models.Faculty> GetAllIncludeForeignKey()
        {
            return Context.Set<Models.Faculty>().Include(x => x.Group).Include(x => x.Student).Include(x => x.Curator).ToList();
        }
    }
}