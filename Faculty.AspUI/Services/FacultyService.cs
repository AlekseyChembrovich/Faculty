using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Faculty.Common.Dto.Faculty;
using Faculty.AspUI.Services.Interfaces;

namespace Faculty.AspUI.Services
{
    /// <summary>
    /// Faculty service.
    /// </summary>
    public class FacultyService : BaseHttpService, IFacultyService
    {
        /// <summary>
        /// Constructor for init Http Client.
        /// </summary>
        /// <param name="httpClient">Http client.</param>
        public FacultyService(HttpClient httpClient) : base(httpClient)
        {

        }

        /// <summary>
        /// Method for getting all faculty list.
        /// </summary>
        /// <returns>An instance of the Task class typed by IEnumerable interface of faculty for display.</returns>
        public async Task<IEnumerable<FacultyDisplayDto>> GetFaculties()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, "api/faculties");
            var response = await HttpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            var facultiesDisplayDto = await ConvertHttpResponseTo<IEnumerable<FacultyDisplayDto>>(response);
            return facultiesDisplayDto;
        }

        /// <summary>
        /// Method for getting faculty by id.
        /// </summary>
        /// <param name="id">Faculty id.</param>
        /// <returns>An instance of the Task class typed by FacultyDto class.</returns>
        public async Task<FacultyDto> GetFaculty(int id)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"api/faculties/{id}");
            var response = await HttpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            var facultyDto = await ConvertHttpResponseTo<FacultyDto>(response);
            return facultyDto;
        }

        /// <summary>
        /// Method for creating faculty.
        /// </summary>
        /// <param name="facultyDto">Faculty data transfer object.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        public async Task<HttpResponseMessage> CreateFaculty(FacultyDto facultyDto)
        {
            var response = await HttpClient.PostAsync("api/faculties",
                new StringContent(JsonConvert.SerializeObject(facultyDto), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return response;
        }

        /// <summary>
        /// Method for deleting faculty.
        /// </summary>
        /// <param name="id">Faculty id.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        public async Task<HttpResponseMessage> DeleteFaculty(int id)
        {
            var message = new HttpRequestMessage(HttpMethod.Delete, $"api/faculties/{id}");
            var response = await HttpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            return response;
        }

        /// <summary>
        /// Method for editing faculty.
        /// </summary>
        /// <param name="facultyDto">Faculty data transfer object.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        public async Task<HttpResponseMessage> EditFaculty(FacultyDto facultyDto)
        {
            var response = await HttpClient.PutAsync("api/faculties",
                new StringContent(JsonConvert.SerializeObject(facultyDto), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return response;
        }
    }
}
