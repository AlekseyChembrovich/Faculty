using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Faculty.AspUI.Services.Interfaces;
using Faculty.AspUI.ViewModels.LoginRegister;

namespace Faculty.AspUI.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _userClient;

        public AuthService(HttpClient httpClient)
        {
            _userClient = httpClient;
        }

        public async Task<HttpResponseMessage> Login(LoginUser loginUser)
        {
            var response = await _userClient.PostAsync("auth/login", new StringContent(JsonConvert.SerializeObject(loginUser), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return response;
        }

        public async Task<HttpResponseMessage> Register(RegisterUser registerUser)
        {
            var response = await _userClient.PostAsync("auth/register", new StringContent(JsonConvert.SerializeObject(registerUser), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return response;
        }
    }
}
