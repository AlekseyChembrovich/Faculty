using System.Net.Http;
using System.Threading.Tasks;
using Faculty.AspUI.ViewModels.LoginRegister;

namespace Faculty.AspUI.Services.Interfaces
{
    /// <summary>
    /// Interface authentication service.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Method for login user.
        /// </summary>
        /// <param name="loginUser">Model user for login.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        Task<HttpResponseMessage> Login(LoginUser loginUser);

        /// <summary>
        /// Method for register user.
        /// </summary>
        /// <param name="registerUser">Model user for register.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        Task<HttpResponseMessage> Register(RegisterUser registerUser);
    }
}
