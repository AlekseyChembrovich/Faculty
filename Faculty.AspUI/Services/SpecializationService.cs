using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Faculty.AspUI.Services.Interfaces;
using Faculty.AspUI.ViewModels.Specialization;

namespace Faculty.AspUI.Services
{
    public class SpecializationService : BaseHttpService, ISpecializationService
    {
        public SpecializationService(HttpClient httpClient) : base(httpClient)
        {

        }

        public async Task<IEnumerable<SpecializationDisplayModify>> GetSpecializations()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, "api/specializations");
            var response = await HttpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            var specializationsDisplay = await ConvertHttpResponseTo<IEnumerable<SpecializationDisplayModify>>(response);
            return specializationsDisplay;
        }

        public async Task<SpecializationDisplayModify> GetSpecialization(int id)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"api/specializations/{id}");
            var response = await HttpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            var specializationModify = await ConvertHttpResponseTo<SpecializationDisplayModify>(response);
            return specializationModify;
        }

        public async Task<HttpResponseMessage> CreateSpecialization(SpecializationAdd specializationAdd)
        {
            var response = await HttpClient.PostAsync("api/specializations", new StringContent(JsonConvert.SerializeObject(specializationAdd), Encoding.UTF8, "application/json"));
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

        public async Task<HttpResponseMessage> EditSpecialization(SpecializationDisplayModify specializationModify)
        {
            var response = await HttpClient.PutAsync("api/specializations", new StringContent(JsonConvert.SerializeObject(specializationModify), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return response;
        }
    }
}
