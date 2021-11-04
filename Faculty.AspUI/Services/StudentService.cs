using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Faculty.Common.Dto.Student;
using Faculty.AspUI.Services.Interfaces;

namespace Faculty.AspUI.Services
{
    public class StudentService : BaseHttpService, IStudentService
    {
        public StudentService(HttpClient httpClient) : base(httpClient)
        {

        }

        public async Task<IEnumerable<StudentDto>> GetStudents()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, "api/students");
            var response = await HttpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            var studentDto = await ConvertHttpResponseTo<IEnumerable<StudentDto>>(response);
            return studentDto;
        }

        public async Task<StudentDto> GetStudent(int id)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"api/students/{id}");
            var response = await HttpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            var studentDto = await ConvertHttpResponseTo<StudentDto>(response);
            return studentDto;
        }

        public async Task<HttpResponseMessage> CreateStudent(StudentDto studentDto)
        {
            var response = await HttpClient.PostAsync("api/students", new StringContent(JsonConvert.SerializeObject(studentDto), Encoding.UTF8, "application/json"));
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

        public async Task<HttpResponseMessage> EditStudent(StudentDto studentDto)
        {
            var response = await HttpClient.PutAsync("api/students", new StringContent(JsonConvert.SerializeObject(studentDto), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return response;
        }
    }
}
