using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Faculty.AspUI.Services.Interfaces;
using Faculty.Common.Dto.Specialization;

namespace Faculty.AspUI.Services
{
    public class SpecializationService : BaseHttpService, ISpecializationService
    {
        public SpecializationService(HttpClient httpClient) : base(httpClient)
        {

        }

        public async Task<IEnumerable<SpecializationDto>> GetSpecializations()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, "api/specializations");
            var response = await HttpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            var specializationDto = await ConvertHttpResponseTo<IEnumerable<SpecializationDto>>(response);
            return specializationDto;
        }

        public async Task<SpecializationDto> GetSpecialization(int id)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"api/specializations/{id}");
            var response = await HttpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            var specializationDto = await ConvertHttpResponseTo<SpecializationDto>(response);
            return specializationDto;
        }

        public async Task<HttpResponseMessage> CreateSpecialization(SpecializationDto specializationDto)
        {
            var response = await HttpClient.PostAsync("api/specializations",
                new StringContent(JsonConvert.SerializeObject(specializationDto), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return response;
        }

        public async Task<HttpResponseMessage> DeleteSpecialization(int id)
        {
            var message = new HttpRequestMessage(HttpMethod.Delete, $"api/specializations/{id}");
            var response = await HttpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            return response;
        }

        public async Task<HttpResponseMessage> EditSpecialization(SpecializationDto specializationDto)
        {
            var response = await HttpClient.PutAsync("api/specializations",
                new StringContent(JsonConvert.SerializeObject(specializationDto), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return response;
        }
    }
}
