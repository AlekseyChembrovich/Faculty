using System.Net.Http;
using System.Threading.Tasks;
using Faculty.Common.Dto.LoginRegister;

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
        /// <param name="authUserDto">Model user for login.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        Task<HttpResponseMessage> Login(AuthUserDto authUserDto);

        /// <summary>
        /// Method for register user.
        /// </summary>
        /// <param name="authUserDto">Model user for register.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        Task<HttpResponseMessage> Register(AuthUserDto authUserDto);
    }
}
