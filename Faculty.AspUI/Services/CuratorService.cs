using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Faculty.Common.Dto.Curator;
using Faculty.AspUI.Services.Interfaces;

namespace Faculty.AspUI.Services
{
    /// <summary>
    /// Curator service.
    /// </summary>
    public class CuratorService : BaseHttpService, ICuratorService
    {
        /// <summary>
        /// Constructor for init Http Client.
        /// </summary>
        /// <param name="httpClient">Http client.</param>
        public CuratorService(HttpClient httpClient) : base(httpClient)
        {

        }

        /// <summary>
        /// Method for getting all curator list.
        /// </summary>
        /// <returns>An instance of the Task class typed by IEnumerable interface of curator for display.</returns>
        public async Task<IEnumerable<CuratorDto>> GetCurators()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, "api/curators");
            var response = await HttpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            var curatorDto = await ConvertHttpResponseTo<IEnumerable<CuratorDto>>(response);
            return curatorDto;
        }

        /// <summary>
        /// Method for getting curator by id.
        /// </summary>
        /// <param name="id">Curator id.</param>
        /// <returns>An instance of the Task class typed by CuratorDto class.</returns>
        public async Task<CuratorDto> GetCurator(int id)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"api/curators/{id}");
            var response = await HttpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            var curatorDto = await ConvertHttpResponseTo<CuratorDto>(response);
            return curatorDto;
        }

        /// <summary>
        /// Method for creating curator.
        /// </summary>
        /// <param name="curatorDto">Curator data transfer object.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        public async Task<HttpResponseMessage> CreateCurator(CuratorDto curatorDto)
        {
            var response = await HttpClient.PostAsync("api/curators",
                new StringContent(JsonConvert.SerializeObject(curatorDto), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return response;
        }

        /// <summary>
        /// Method for deleting curator.
        /// </summary>
        /// <param name="id">Curator id.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        public async Task<HttpResponseMessage> DeleteCurator(int id)
        {
            var message = new HttpRequestMessage(HttpMethod.Delete, $"api/curators/{id}");
            var response = await HttpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            return response;
        }

        /// <summary>
        /// Method for editing curator.
        /// </summary>
        /// <param name="curatorDto">Curator data transfer object.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        public async Task<HttpResponseMessage> EditCurator(CuratorDto curatorDto)
        {
            var response = await HttpClient.PutAsync("api/curators",
                new StringContent(JsonConvert.SerializeObject(curatorDto), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return response;
        }
    }
}
