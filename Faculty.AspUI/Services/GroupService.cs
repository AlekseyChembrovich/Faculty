using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Faculty.Common.Dto.Group;
using System.Collections.Generic;
using Faculty.AspUI.Services.Interfaces;

namespace Faculty.AspUI.Services
{
    /// <summary>
    /// Group service.
    /// </summary>
    public class GroupService : BaseHttpService, IGroupService
    {
        /// <summary>
        /// Constructor for init Http Client.
        /// </summary>
        /// <param name="httpClient">Http client.</param>
        public GroupService(HttpClient httpClient) : base(httpClient)
        {

        }

        /// <summary>
        /// Method for getting all group list.
        /// </summary>
        /// <returns>An instance of the Task class typed by IEnumerable interface of group for display.</returns>
        public async Task<IEnumerable<GroupDisplayDto>> GetGroups()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, "api/groups");
            var response = await HttpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            var groupsDisplayDto = await ConvertHttpResponseTo<IEnumerable<GroupDisplayDto>>(response);
            return groupsDisplayDto;
        }

        /// <summary>
        /// Method for getting group by id.
        /// </summary>
        /// <param name="id">Group id.</param>
        /// <returns>An instance of the Task class typed by GroupDto class.</returns>
        public async Task<GroupDto> GetGroup(int id)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"api/groups/{id}");
            var response = await HttpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            var groupDto = await ConvertHttpResponseTo<GroupDto>(response);
            return groupDto;
        }

        /// <summary>
        /// Method for creating group.
        /// </summary>
        /// <param name="groupDto">Group data transfer object.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        public async Task<HttpResponseMessage> CreateGroup(GroupDto groupDto)
        {
            var response = await HttpClient.PostAsync("api/groups",
                new StringContent(JsonConvert.SerializeObject(groupDto), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return response;
        }

        /// <summary>
        /// Method for deleting group.
        /// </summary>
        /// <param name="id">Group id.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        public async Task<HttpResponseMessage> DeleteGroup(int id)
        {
            var message = new HttpRequestMessage(HttpMethod.Delete, $"api/groups/{id}");
            var response = await HttpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            return response;
        }

        /// <summary>
        /// Method for editing group.
        /// </summary>
        /// <param name="groupDto">Group data transfer object.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        public async Task<HttpResponseMessage> EditGroup(GroupDto groupDto)
        {
            var response = await HttpClient.PutAsync("api/groups",
                new StringContent(JsonConvert.SerializeObject(groupDto), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return response;
        }
    }
}
