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
            _userClient = clientFactory.CreateClient("usersHttpClient");
        }

        public async Task<IEnumerable<UserDisplay>> GetAllUsers()
        {
            var response = await SendGet("User/GetAll");
            var usersDisplay = await HttpResponseToUser<IEnumerable<UserDisplay>>(response);
            return usersDisplay;
        }

        public async Task<HttpResponseMessage> CreateNewUser(UserAdd userAdd) => await SendPost("User/Create", userAdd);

        public async Task<HttpResponseMessage> DeleteExistUser(string id)
        {
            var response = await SendGet("User/Delete", id);
            return response;
        }

        public async Task<HttpResponseMessage> EditExistUser(UserModify userModify) => await SendPost("User/Edit", userModify);

        public async Task<UserModify> FindByIdUser(string id)
        {
            var response = await SendGet("User/GetById", id);
            var userModify = await HttpResponseToUser<UserModify>(response);
            return userModify;
        }

        public async Task<HttpResponseMessage> EditPasswordExistUser(EditPassUser editPassUser) => await SendPost("User/EditPassword", editPassUser);

        public async Task<IEnumerable<string>> GetAllRoles()
        {
            var response = await SendGet("User/GetRoles");
            var namesRole = await HttpResponseToUser<IEnumerable<string>>(response);
            return namesRole;
        }

        public async Task<HttpResponseMessage> GetLoginResponse(LoginUser loginUser) => await SendPost("LoginRegister/Login", loginUser);

        public async Task<HttpResponseMessage> GetRegisterResponse(RegisterUser registerUser) => await SendPost("LoginRegister/Register", registerUser);

        private async Task<HttpResponseMessage> SendGet(string url, string id = null)
        {
            url = (id is null) ? url : url + $"?id={id}";
            var message = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await _userClient.SendAsync(message);
            return response;
        }

        private async Task<HttpResponseMessage> SendPost<T>(string url, T model)
        {
            var response = await _userClient.PostAsync(url, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));
            return response;
        }

        private static async Task<T> HttpResponseToUser<T>(HttpResponseMessage response)
        {
            var modelJson = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<T>(modelJson);
            return model;
        }
    }
}
