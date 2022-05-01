using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Faculty.Common.Dto.Curator;

namespace Faculty.AspUI.Services.Interfaces
{
    /// <summary>
    /// Interface curator service.
    /// </summary>
    public interface ICuratorService
    {
        /// <summary>
        /// Method for getting all curator list.
        /// </summary>
        /// <returns>An instance of the Task class typed by IEnumerable interface of curator for display.</returns>
        Task<IEnumerable<CuratorDto>> GetCurators();

        /// <summary>
        /// Method for getting curator by id.
        /// </summary>
        /// <param name="id">Curator id.</param>
        /// <returns>An instance of the Task class typed by CuratorDto class.</returns>
        Task<CuratorDto> GetCurator(int id);

        /// <summary>
        /// Method for creating curator.
        /// </summary>
        /// <param name="curatorDto">Curator data transfer object.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        Task<HttpResponseMessage> CreateCurator(CuratorDto curatorDto);

        /// <summary>
        /// Method for deleting curator.
        /// </summary>
        /// <param name="id">Curator id.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        Task<HttpResponseMessage> DeleteCurator(int id);

        /// <summary>
        /// Method for editing curator.
        /// </summary>
        /// <param name="curatorDto">Curator data transfer object.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        Task<HttpResponseMessage> EditCurator(CuratorDto curatorDto);
    }
}
