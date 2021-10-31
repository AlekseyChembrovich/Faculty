using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Faculty.AspUI.ViewModels.User;
using Faculty.AspUI.Services.Interfaces;

namespace Faculty.AspUI.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _userClient;

        public UserService(HttpClient httpClient)
        {
            _userClient = httpClient;
        }

        public async Task<IEnumerable<UserDisplay>> GetUsers()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, "users");
            var response = await _userClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            var usersDisplay = await ConvertHttpResponseTo<IEnumerable<UserDisplay>>(response);
            return usersDisplay;
        }

        public async Task<UserModify> GetUser(string id)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"users/{id}");
            var response = await _userClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            var userModify = await ConvertHttpResponseTo<UserModify>(response);
            return userModify;
        }

        public async Task<HttpResponseMessage> CreateUser(UserAdd userAdd)
        {
            var response = await _userClient.PostAsync("users", new StringContent(JsonConvert.SerializeObject(userAdd), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return response;
        }

        public async Task<HttpResponseMessage> DeleteUser(string id)
        {
            var message = new HttpRequestMessage(HttpMethod.Delete, $"users/{id}");
            var response = await _userClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            return response;
        }

        public async Task<HttpResponseMessage> EditUser(UserModify userModify)
        {
            var response = await _userClient.PutAsync("users", new StringContent(JsonConvert.SerializeObject(userModify), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return response;
        }

        public async Task<HttpResponseMessage> EditPasswordUser(UserModifyPassword userEditPass)
        {
            var response = await _userClient.PatchAsync("users", new StringContent(JsonConvert.SerializeObject(userEditPass), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return response;
        }

        public async Task<IEnumerable<string>> GetRoles()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, "users/roles");
            var response = await _userClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            var namesRole = await ConvertHttpResponseTo<IEnumerable<string>>(response);
            return namesRole;
        }

        private static async Task<T> ConvertHttpResponseTo<T>(HttpResponseMessage response)
        {
            var modelJson = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<T>(modelJson);
            return model;
        }
    }
}
