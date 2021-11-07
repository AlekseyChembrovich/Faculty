using System.Net.Http;
using System.Threading.Tasks;
using Faculty.Common.Dto.Group;
using System.Collections.Generic;

namespace Faculty.AspUI.Services.Interfaces
{
    /// <summary>
    /// Interface group service.
    /// </summary>
    public interface IGroupService
    {
        /// <summary>
        /// Method for getting all group list.
        /// </summary>
        /// <returns>An instance of the Task class typed by IEnumerable interface of group for display.</returns>
        Task<IEnumerable<GroupDisplayDto>> GetGroups();

        /// <summary>
        /// Method for getting group by id.
        /// </summary>
        /// <param name="id">Group id.</param>
        /// <returns>An instance of the Task class typed by GroupDto class.</returns>
        Task<GroupDto> GetGroup(int id);

        /// <summary>
        /// Method for creating group.
        /// </summary>
        /// <param name="groupDto">Group data transfer object.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        Task<HttpResponseMessage> CreateGroup(GroupDto groupDto);

        /// <summary>
        /// Method for deleting group.
        /// </summary>
        /// <param name="id">Group id.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        Task<HttpResponseMessage> DeleteGroup(int id);

        /// <summary>
        /// Method for editing group.
        /// </summary>
        /// <param name="groupDto">Group data transfer object.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        Task<HttpResponseMessage> EditGroup(GroupDto groupDto);
    }
}
