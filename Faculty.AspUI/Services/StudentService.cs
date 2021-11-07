using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Faculty.Common.Dto.Student;
using Faculty.AspUI.Services.Interfaces;

namespace Faculty.AspUI.Services
{
    /// <summary>
    /// Student service.
    /// </summary>
    public class StudentService : BaseHttpService, IStudentService
    {
        /// <summary>
        /// Constructor for init Http Client.
        /// </summary>
        /// <param name="httpClient">Http client.</param>
        public StudentService(HttpClient httpClient) : base(httpClient)
        {

        }

        /// <summary>
        /// Method for getting all student list.
        /// </summary>
        /// <returns>An instance of the Task class typed by IEnumerable interface of student for display.</returns>
        public async Task<IEnumerable<StudentDto>> GetStudents()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, "api/students");
            var response = await HttpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            var studentDto = await ConvertHttpResponseTo<IEnumerable<StudentDto>>(response);
            return studentDto;
        }

        /// <summary>
        /// Method for getting student by id.
        /// </summary>
        /// <param name="id">Student id.</param>
        /// <returns>An instance of the Task class typed by StudentDto class.</returns>
        public async Task<StudentDto> GetStudent(int id)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"api/students/{id}");
            var response = await HttpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            var studentDto = await ConvertHttpResponseTo<StudentDto>(response);
            return studentDto;
        }

        /// <summary>
        /// Method for creating student.
        /// </summary>
        /// <param name="studentDto">Student data transfer object.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        public async Task<HttpResponseMessage> CreateStudent(StudentDto studentDto)
        {
            var response = await HttpClient.PostAsync("api/students", new StringContent(JsonConvert.SerializeObject(studentDto), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return response;
        }

        /// <summary>
        /// Method for deleting student.
        /// </summary>
        /// <param name="id">Student id.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        public async Task<HttpResponseMessage> DeleteStudent(int id)
        {
            var message = new HttpRequestMessage(HttpMethod.Delete, $"api/students/{id}");
            var response = await HttpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            return response;
        }

        /// <summary>
        /// Method for editing student.
        /// </summary>
        /// <param name="studentDto">Student data transfer object.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        public async Task<HttpResponseMessage> EditStudent(StudentDto studentDto)
        {
            var response = await HttpClient.PutAsync("api/students", new StringContent(JsonConvert.SerializeObject(studentDto), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return response;
        }
    }
}
