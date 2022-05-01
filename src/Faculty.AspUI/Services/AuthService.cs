using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Faculty.Common.Dto.LoginRegister;
using Faculty.AspUI.Services.Interfaces;

namespace Faculty.AspUI.Services
{
    /// <summary>
    /// Authentication service.
    /// </summary>
    public class AuthService : BaseHttpService, IAuthService
    {
        /// <summary>
        /// Constructor for init Http Client.
        /// </summary>
        /// <param name="httpClient">Http client.</param>
        public AuthService(HttpClient httpClient) : base(httpClient)
        {

        }

        /// <summary>
        /// Method for login user.
        /// </summary>
        /// <param name="authUserDto">Model user for login.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        public async Task<HttpResponseMessage> Login(AuthUserDto authUserDto)
        {
            var response = await HttpClient.PostAsync("api/auth/login",
                new StringContent(JsonConvert.SerializeObject(authUserDto), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return response;
        }

        /// <summary>
        /// Method for register user.
        /// </summary>
        /// <param name="authUserDto">Model user for register.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        public async Task<HttpResponseMessage> Register(AuthUserDto authUserDto)
        {
            var response = await HttpClient.PostAsync("api/auth/register",
                new StringContent(JsonConvert.SerializeObject(authUserDto), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return response;
        }
    }
}
