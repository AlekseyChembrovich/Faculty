using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Faculty.Common.Dto.User;
using System.Collections.Generic;
using Faculty.AspUI.Services.Interfaces;

namespace Faculty.AspUI.Services
{
    /// <summary>
    /// User service.
    /// </summary>
    public class UserService : BaseHttpService, IUserService
    {
        /// <summary>
        /// Constructor for init Http Client.
        /// </summary>
        /// <param name="httpClient">Http client.</param>
        public UserService(HttpClient httpClient) : base(httpClient)
        {

        }

        /// <summary>
        /// Method for getting all user list.
        /// </summary>
        /// <returns>An instance of the Task class typed by IEnumerable interface of user for display.</returns>
        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, "api/users");
            var response = await HttpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            var usersDto = await ConvertHttpResponseTo<IEnumerable<UserDto>>(response);
            return usersDto;
        }

        /// <summary>
        /// Method for getting user by id.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns>An instance of the Task class typed by UserModify class.</returns>
        public async Task<UserDto> GetUser(string id)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"api/users/{id}");
            var response = await HttpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            var userDto = await ConvertHttpResponseTo<UserDto>(response);
            return userDto;
        }

        /// <summary>
        /// Method for creating user.
        /// </summary>
        /// <param name="userAddDto">Model user for add.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        public async Task<HttpResponseMessage> CreateUser(UserAddDto userAddDto)
        {
            var response = await HttpClient.PostAsync("api/users",
                new StringContent(JsonConvert.SerializeObject(userAddDto), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return response;
        }

        /// <summary>
        /// Method for deleting user.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        public async Task<HttpResponseMessage> DeleteUser(string id)
        {
            var message = new HttpRequestMessage(HttpMethod.Delete, $"api/users/{id}");
            var response = await HttpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            return response;
        }

        /// <summary>
        /// Method for editing user.
        /// </summary>
        /// <param name="userDto">Model user for modify.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        public async Task<HttpResponseMessage> EditUser(UserDto userDto)
        {
            var response = await HttpClient.PutAsync("api/users",
                new StringContent(JsonConvert.SerializeObject(userDto), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return response;
        }

        /// <summary>
        /// Method for editing user password.
        /// </summary>
        /// <param name="userModifyPasswordDto">Model user for modify password.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        public async Task<HttpResponseMessage> EditPasswordUser(UserModifyPasswordDto userModifyPasswordDto)
        {
            var response = await HttpClient.PatchAsync("api/users",
                new StringContent(JsonConvert.SerializeObject(userModifyPasswordDto), Encoding.UTF8,
                    "application/json"));
            response.EnsureSuccessStatusCode();
            return response;
        }

        /// <summary>
        /// Method for getting existing roles.
        /// </summary>
        /// <returns>An instance of the Task class typed by IEnumerable interface of strings.</returns>
        public async Task<IEnumerable<string>> GetRoles()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, "api/users/roles");
            var response = await HttpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            var namesRole = await ConvertHttpResponseTo<IEnumerable<string>>(response);
            return namesRole;
        }
    }
}
