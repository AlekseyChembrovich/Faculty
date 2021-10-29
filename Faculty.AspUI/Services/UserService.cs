using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Faculty.AspUI.ViewModels.User;
using Faculty.AspUI.ViewModels.LoginRegister;

namespace Faculty.AspUI.Services
{
    public class UserService
    {
        private readonly HttpClient _userClient;

        public UserService(IHttpClientFactory clientFactory)
        {
            _userClient = clientFactory.CreateClient("UsersHttpClient");
        }

        public async Task<IEnumerable<UserDisplay>> GetUsers()
        {
            var response = await SendGet("users");
            var usersDisplay = await ConvertHttpResponseToUser<IEnumerable<UserDisplay>>(response);
            return usersDisplay;
        }

        public async Task<UserModify> GetUser(string id)
        {
            var response = await SendGet("users", id);
            var userModify = await ConvertHttpResponseToUser<UserModify>(response);
            return userModify;
        }

        public async Task<HttpResponseMessage> CreateUser(UserAdd userAdd)
        {
            return await SendPost("users", userAdd);
        }

        public async Task<HttpResponseMessage> DeleteUser(string id)
        {
            var response = await SendDelete("users", id);
            return response;
        }

        public async Task<HttpResponseMessage> EditUser(UserModify userModify)
        {
            return await SendPut("users", userModify);
        }

        public async Task<HttpResponseMessage> EditPasswordUser(UserEditPass userEditPass)
        {
            return await SendPatch("users", userEditPass);
        }

        public async Task<IEnumerable<string>> GetRoles()
        {
            var response = await SendGet("users/roles");
            var namesRole = await ConvertHttpResponseToUser<IEnumerable<string>>(response);
            return namesRole;
        }

        public async Task<HttpResponseMessage> Login(LoginUser loginUser)
        {
            return await SendPost("auth/login", loginUser);
        }

        public async Task<HttpResponseMessage> Register(RegisterUser registerUser)
        {
            return await SendPost("auth/register", registerUser);
        }

        private async Task<HttpResponseMessage> SendGet(string url, string id = null)
        {
            url = (id is null) ? url : url + $"/{id}";
            var message = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await _userClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            return response;
        }

        private async Task<HttpResponseMessage> SendPost<T>(string url, T model)
        {
            var response = await _userClient.PostAsync(url, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return response;
        }

        private async Task<HttpResponseMessage> SendDelete(string url, string id)
        {
            url += $"/{id}";
            var message = new HttpRequestMessage(HttpMethod.Delete, url);
            var response = await _userClient.SendAsync(message);
            response.EnsureSuccessStatusCode();
            return response;
        }

        private async Task<HttpResponseMessage> SendPut<T>(string url, T model)
        {
            var response = await _userClient.PutAsync(url, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return response;
        }

        private async Task<HttpResponseMessage> SendPatch<T>(string url, T model)
        {
            var response = await _userClient.PatchAsync(url, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return response;
        }

        private static async Task<T> ConvertHttpResponseToUser<T>(HttpResponseMessage response)
        {
            var modelJson = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<T>(modelJson);
            return model;
        }
    }
}
