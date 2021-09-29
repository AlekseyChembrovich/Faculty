using System.Collections.Generic;
using Faculty.DataAccessLayer.Models;

namespace Faculty.DataAccessLayer.RepositoryEntityFramework
{
    public interface IRepositoryGroup : IRepository<Group>
    {
        /// <summary>
        /// Method for selecting data from a database and include foreign key.
        /// </summary>
        /// <returns>Lots of Entity objects returned.</returns>
        IEnumerable<Group> GetAllIncludeForeignKey();
    }
}