using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Faculty.AspUI.ViewModels.Curator;
using Faculty.AspUI.Services.Interfaces;

namespace Faculty.AspUI.Services
{
    public class CuratorService : BaseHttpService, ICuratorService
    {
        public CuratorService(HttpClient httpClient) : base(httpClient)
        {

        }

        public async Task<IEnumerable<CuratorDisplayModify>> GetCurators()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, "api/curators");
            var response = await HttpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            var curatorsDisplay = await ConvertHttpResponseTo<IEnumerable<CuratorDisplayModify>>(response);
            return curatorsDisplay;
        }

        public async Task<CuratorDisplayModify> GetCurator(int id)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"api/curators/{id}");
            var response = await HttpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            var curatorModify = await ConvertHttpResponseTo<CuratorDisplayModify>(response);
            return curatorModify;
        }

        public async Task<HttpResponseMessage> CreateCurator(CuratorAdd curatorAdd)
        {
            var response = await HttpClient.PostAsync("api/curators", new StringContent(JsonConvert.SerializeObject(curatorAdd), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return response;
        }

        public async Task<HttpResponseMessage> DeleteCurator(int id)
        {
            var message = new HttpRequestMessage(HttpMethod.Delete, $"api/curators/{id}");
            var response = await HttpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            return response;
        }

        public async Task<HttpResponseMessage> EditCurator(CuratorDisplayModify curatorModify)
        {
            var response = await HttpClient.PutAsync("api/curators", new StringContent(JsonConvert.SerializeObject(curatorModify), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return response;
        }
    }
}
