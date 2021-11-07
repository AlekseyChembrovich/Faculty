using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Faculty.AspUI.Services.Interfaces;
using Faculty.Common.Dto.Specialization;

namespace Faculty.AspUI.Services
{
    /// <summary>
    /// Specialization service.
    /// </summary>
    public class SpecializationService : BaseHttpService, ISpecializationService
    {
        /// <summary>
        /// Constructor for init Http Client.
        /// </summary>
        /// <param name="httpClient">Http client.</param>
        public SpecializationService(HttpClient httpClient) : base(httpClient)
        {

        }

        /// <summary>
        /// Method for getting all specialization list.
        /// </summary>
        /// <returns>An instance of the Task class typed by IEnumerable interface of specialization for display.</returns>
        public async Task<IEnumerable<SpecializationDto>> GetSpecializations()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, "api/specializations");
            var response = await HttpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            var specializationDto = await ConvertHttpResponseTo<IEnumerable<SpecializationDto>>(response);
            return specializationDto;
        }

        /// <summary>
        /// Method for getting specialization by id.
        /// </summary>
        /// <param name="id">Specialization id.</param>
        /// <returns>An instance of the Task class typed by SpecializationDto class.</returns>
        public async Task<SpecializationDto> GetSpecialization(int id)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"api/specializations/{id}");
            var response = await HttpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            var specializationDto = await ConvertHttpResponseTo<SpecializationDto>(response);
            return specializationDto;
        }

        /// <summary>
        /// Method for creating specialization.
        /// </summary>
        /// <param name="specializationDto">Specialization data transfer object.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        public async Task<HttpResponseMessage> CreateSpecialization(SpecializationDto specializationDto)
        {
            var response = await HttpClient.PostAsync("api/specializations",
                new StringContent(JsonConvert.SerializeObject(specializationDto), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return response;
        }

        /// <summary>
        /// Method for deleting specialization.
        /// </summary>
        /// <param name="id">Specialization id.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        public async Task<HttpResponseMessage> DeleteSpecialization(int id)
        {
            var message = new HttpRequestMessage(HttpMethod.Delete, $"api/specializations/{id}");
            var response = await HttpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            return response;
        }

        /// <summary>
        /// Method for editing specialization.
        /// </summary>
        /// <param name="specializationDto">Specialization data transfer object.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        public async Task<HttpResponseMessage> EditSpecialization(SpecializationDto specializationDto)
        {
            var response = await HttpClient.PutAsync("api/specializations",
                new StringContent(JsonConvert.SerializeObject(specializationDto), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return response;
        }
    }
}
