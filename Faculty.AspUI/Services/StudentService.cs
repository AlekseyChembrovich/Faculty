using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Faculty.AspUI.ViewModels.Student;
using Faculty.AspUI.Services.Interfaces;

namespace Faculty.AspUI.Services
{
    public class StudentService : BaseHttpService, IStudentService
    {
        public StudentService(HttpClient httpClient) : base(httpClient)
        {

        }

        public async Task<IEnumerable<StudentDisplayModify>> GetStudents()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, "api/students");
            var response = await HttpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            var studentsDisplay = await ConvertHttpResponseTo<IEnumerable<StudentDisplayModify>>(response);
            return studentsDisplay;
        }

        public async Task<StudentDisplayModify> GetStudent(int id)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"api/students/{id}");
            var response = await HttpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            var studentModify = await ConvertHttpResponseTo<StudentDisplayModify>(response);
            return studentModify;
        }

        public async Task<HttpResponseMessage> CreateStudent(StudentAdd studentAdd)
        {
            var response = await HttpClient.PostAsync("api/students", new StringContent(JsonConvert.SerializeObject(studentAdd), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return response;
        }

        public async Task<HttpResponseMessage> DeleteStudent(int id)
        {
            var message = new HttpRequestMessage(HttpMethod.Delete, $"api/students/{id}");
            var response = await HttpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            return response;
        }

        public async Task<HttpResponseMessage> EditStudent(StudentDisplayModify studentModify)
        {
            var response = await HttpClient.PutAsync("api/students", new StringContent(JsonConvert.SerializeObject(studentModify), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return response;
        }
    }
}
