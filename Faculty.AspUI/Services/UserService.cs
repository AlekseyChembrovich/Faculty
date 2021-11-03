using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Faculty.AspUI.ViewModels.User;
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
        public async Task<IEnumerable<UserDisplay>> GetUsers()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, "api/users");
            var response = await HttpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            var usersDisplay = await ConvertHttpResponseTo<IEnumerable<UserDisplay>>(response);
            return usersDisplay;
        }

        /// <summary>
        /// Method for getting user by id.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns>An instance of the Task class typed by UserModify class.</returns>
        public async Task<UserModify> GetUser(string id)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"api/users/{id}");
            var response = await HttpClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            var userModify = await ConvertHttpResponseTo<UserModify>(response);
            return userModify;
        }

        /// <summary>
        /// Method for creating user.
        /// </summary>
        /// <param name="userAdd">Model user for add.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        public async Task<HttpResponseMessage> CreateUser(UserAdd userAdd)
        {
            var response = await HttpClient.PostAsync("api/users", new StringContent(JsonConvert.SerializeObject(userAdd), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return response;
        }

        /// <summary>
        /// Method for deleting user.
        /// </summary>
        /// <param name="id">Model user for delete.</param>
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
        /// <param name="userModify">Model user for modify.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        public async Task<HttpResponseMessage> EditUser(UserModify userModify)
        {
            var response = await HttpClient.PutAsync("api/users", new StringContent(JsonConvert.SerializeObject(userModify), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return response;
        }

        /// <summary>
        /// Method for editing user password.
        /// </summary>
        /// <param name="userEditPass">Model user for modify password.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        public async Task<HttpResponseMessage> EditPasswordUser(UserModifyPassword userEditPass)
        {
            var response = await HttpClient.PatchAsync("api/users", new StringContent(JsonConvert.SerializeObject(userEditPass), Encoding.UTF8, "application/json"));
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
