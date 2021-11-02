using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Faculty.AspUI.Services.Interfaces;
using Faculty.AspUI.ViewModels.LoginRegister;

namespace Faculty.AspUI.Services
{
    /// <summary>
    /// Authentication service.
    /// </summary>
    public class AuthService : IAuthService
    {
        /// <summary>
        /// Http Client for sending request on authentication server.
        /// </summary>
        private readonly HttpClient _userClient;

        /// <summary>
        /// Constructor for init Http Client.
        /// </summary>
        /// <param name="httpClient"></param>
        public AuthService(HttpClient httpClient)
        {
            _userClient = httpClient;
        }

        /// <summary>
        /// Method for login user.
        /// </summary>
        /// <param name="loginUser">Model user for login.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        public async Task<HttpResponseMessage> Login(LoginUser loginUser)
        {
            var response = await _userClient.PostAsync("api/auth/login", new StringContent(JsonConvert.SerializeObject(loginUser), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return response;
        }

        /// <summary>
        /// Method for register user.
        /// </summary>
        /// <param name="registerUser">Model user for register.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        public async Task<HttpResponseMessage> Register(RegisterUser registerUser)
        {
            var response = await _userClient.PostAsync("api/auth/register", new StringContent(JsonConvert.SerializeObject(registerUser), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return response;
        }
    }
}
