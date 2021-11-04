using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Faculty.Common.Dto.Faculty;
using Faculty.AspUI.Services.Interfaces;

namespace Faculty.AspUI.Services
{
    public class FacultyService : BaseHttpService, IFacultyService
    {
        public FacultyService(HttpClient httpClient) : base(httpClient)
        {

        }

        public async Task<IEnumerable<FacultyDisplayDto>> GetFaculties()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, "api/faculties");
            var response = await HttpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            var facultiesDisplayDto = await ConvertHttpResponseTo<IEnumerable<FacultyDisplayDto>>(response);
            return facultiesDisplayDto;
        }

        public async Task<FacultyDto> GetFaculty(int id)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"api/faculties/{id}");
            var response = await HttpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            var facultyDto = await ConvertHttpResponseTo<FacultyDto>(response);
            return facultyDto;
        }

        public async Task<HttpResponseMessage> CreateFaculty(FacultyDto facultyDto)
        {
            var response = await HttpClient.PostAsync("api/faculties",
                new StringContent(JsonConvert.SerializeObject(facultyDto), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return response;
        }

        public async Task<HttpResponseMessage> DeleteFaculty(int id)
        {
            var message = new HttpRequestMessage(HttpMethod.Delete, $"api/faculties/{id}");
            var response = await HttpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            return response;
        }

        public async Task<HttpResponseMessage> EditFaculty(FacultyDto facultyDto)
        {
            var response = await HttpClient.PutAsync("api/faculties",
                new StringContent(JsonConvert.SerializeObject(facultyDto), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return response;
        }
    }
}
