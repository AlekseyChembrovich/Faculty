using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Faculty.Common.Dto.Group;
using System.Collections.Generic;
using Faculty.AspUI.Services.Interfaces;

namespace Faculty.AspUI.Services
{
    public class GroupService : BaseHttpService, IGroupService
    {
        public GroupService(HttpClient httpClient) : base(httpClient)
        {

        }

        public async Task<IEnumerable<GroupDisplayDto>> GetGroups()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, "api/groups");
            var response = await HttpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            var groupsDisplayDto = await ConvertHttpResponseTo<IEnumerable<GroupDisplayDto>>(response);
            return groupsDisplayDto;
        }

        public async Task<GroupDto> GetGroup(int id)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"api/groups/{id}");
            var response = await HttpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            var groupDto = await ConvertHttpResponseTo<GroupDto>(response);
            return groupDto;
        }

        public async Task<HttpResponseMessage> CreateGroup(GroupDto groupDto)
        {
            var response = await HttpClient.PostAsync("api/groups",
                new StringContent(JsonConvert.SerializeObject(groupDto), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return response;
        }

        public async Task<HttpResponseMessage> DeleteGroup(int id)
        {
            var message = new HttpRequestMessage(HttpMethod.Delete, $"api/groups/{id}");
            var response = await HttpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            return response;
        }

        public async Task<HttpResponseMessage> EditGroup(GroupDto groupDto)
        {
            var response = await HttpClient.PutAsync("api/groups",
                new StringContent(JsonConvert.SerializeObject(groupDto), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return response;
        }
    }
}
