using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Faculty.Common.Dto.Curator;
using Faculty.AspUI.Services.Interfaces;

namespace Faculty.AspUI.Services
{
    public class CuratorService : BaseHttpService, ICuratorService
    {
        public CuratorService(HttpClient httpClient) : base(httpClient)
        {

        }

        public async Task<IEnumerable<CuratorDto>> GetCurators()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, "api/curators");
            var response = await HttpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            var curatorDto = await ConvertHttpResponseTo<IEnumerable<CuratorDto>>(response);
            return curatorDto;
        }

        public async Task<CuratorDto> GetCurator(int id)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"api/curators/{id}");
            var response = await HttpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            var curatorDto = await ConvertHttpResponseTo<CuratorDto>(response);
            return curatorDto;
        }

        public async Task<HttpResponseMessage> CreateCurator(CuratorDto curatorDto)
        {
            var response = await HttpClient.PostAsync("api/curators",
                new StringContent(JsonConvert.SerializeObject(curatorDto), Encoding.UTF8, "application/json"));
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

        public async Task<HttpResponseMessage> EditCurator(CuratorDto curatorDto)
        {
            var response = await HttpClient.PutAsync("api/curators",
                new StringContent(JsonConvert.SerializeObject(curatorDto), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return response;
        }
    }
}
