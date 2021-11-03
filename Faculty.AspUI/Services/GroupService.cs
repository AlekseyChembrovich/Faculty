using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Faculty.AspUI.ViewModels.Group;
using Faculty.AspUI.Services.Interfaces;

namespace Faculty.AspUI.Services
{
    public class GroupService : BaseHttpService, IGroupService
    {
        public GroupService(HttpClient httpClient) : base(httpClient)
        {

        }

        public async Task<IEnumerable<GroupDisplay>> GetGroups()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, "api/groups");
            var response = await HttpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            var groupsDisplay = await ConvertHttpResponseTo<IEnumerable<GroupDisplay>>(response);
            return groupsDisplay;
        }

        public async Task<GroupModify> GetGroup(int id)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"api/groups/{id}");
            var response = await HttpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            var groupModify = await ConvertHttpResponseTo<GroupModify>(response);
            return groupModify;
        }

        public async Task<HttpResponseMessage> CreateGroup(GroupAdd groupAdd)
        {
            var response = await HttpClient.PostAsync("api/groups", new StringContent(JsonConvert.SerializeObject(groupAdd), Encoding.UTF8, "application/json"));
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

        public async Task<HttpResponseMessage> EditGroup(GroupModify groupModify)
        {
            var response = await HttpClient.PutAsync("api/groups", new StringContent(JsonConvert.SerializeObject(groupModify), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return response;
        }
    }
}
