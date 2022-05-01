using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Faculty.Common.Dto.LoginRegister;

namespace Faculty.AuthenticationServer.Services.Interfaces
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
        Task<Tuple<string, IdentityResult>> GetJwtToken(AuthUserDto authUserDto);

        /// <summary>
        /// Method for register user.
        /// </summary>
        /// <param name="authUserDto">Model user for register.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        Task<IdentityResult> Register(AuthUserDto authUserDto);
    }
}
