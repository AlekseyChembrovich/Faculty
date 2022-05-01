using System.Collections.Generic;

namespace Faculty.DataAccessLayer.Repository.EntityFramework.Interfaces
{
    public interface IRepositoryFaculty : IRepository<Models.Faculty>
    {
        /// <summary>
        /// Method for selecting data from a database and include foreign key.
        /// </summary>
        /// <returns>Lots of Entity objects returned.</returns>
        IEnumerable<Models.Faculty> GetAllIncludeForeignKey();
    }
}