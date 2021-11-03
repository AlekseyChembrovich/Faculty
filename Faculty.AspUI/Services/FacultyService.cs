using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Faculty.AspUI.ViewModels.Faculty;
using Faculty.AspUI.Services.Interfaces;

namespace Faculty.AspUI.Services
{
    public class FacultyService : BaseHttpService, IFacultyService
    {
        public FacultyService(HttpClient httpClient) : base(httpClient)
        {

        }

        public async Task<IEnumerable<FacultyDisplay>> GetFaculties()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, "api/faculties");
            var response = await HttpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            var facultiesDisplay = await ConvertHttpResponseTo<IEnumerable<FacultyDisplay>>(response);
            return facultiesDisplay;
        }

        public async Task<FacultyModify> GetFaculty(int id)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"api/faculties/{id}");
            var response = await HttpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            var facultyModify = await ConvertHttpResponseTo<FacultyModify>(response);
            return facultyModify;
        }

        public async Task<HttpResponseMessage> CreateFaculty(FacultyAdd facultyAdd)
        {
            var response = await HttpClient.PostAsync("api/faculties", new StringContent(JsonConvert.SerializeObject(facultyAdd), Encoding.UTF8, "application/json"));
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

        public async Task<HttpResponseMessage> EditFaculty(FacultyModify facultyModify)
        {
            var response = await HttpClient.PutAsync("api/faculties", new StringContent(JsonConvert.SerializeObject(facultyModify), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return response;
        }
    }
}
