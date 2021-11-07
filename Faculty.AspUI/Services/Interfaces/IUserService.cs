using System.Net.Http;
using System.Threading.Tasks;
using Faculty.Common.Dto.User;
using System.Collections.Generic;

namespace Faculty.AspUI.Services.Interfaces
{
    /// <summary>
    /// Interface user service.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Method for getting all user list.
        /// </summary>
        /// <returns>An instance of the Task class typed by IEnumerable interface of user for display.</returns>
        Task<IEnumerable<UserDto>> GetUsers();

        /// <summary>
        /// Method for getting user by id.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns>An instance of the Task class typed by UserModify class.</returns>
        Task<UserDto> GetUser(string id);

        /// <summary>
        /// Method for creating user.
        /// </summary>
        /// <param name="userAddDto">Model user for add.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        Task<HttpResponseMessage> CreateUser(UserAddDto userAddDto);

        /// <summary>
        /// Method for deleting user.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        Task<HttpResponseMessage> DeleteUser(string id);

        /// <summary>
        /// Method for editing user.
        /// </summary>
        /// <param name="userDto">Model user for modify.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        Task<HttpResponseMessage> EditUser(UserDto userDto);

        /// <summary>
        /// Method for editing user password.
        /// </summary>
        /// <param name="userModifyPasswordDto">Model user for modify password.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        Task<HttpResponseMessage> EditPasswordUser(UserModifyPasswordDto userModifyPasswordDto);

        /// <summary>
        /// Method for getting existing roles.
        /// </summary>
        /// <returns>An instance of the Task class typed by IEnumerable interface of strings.</returns>
        Task<IEnumerable<string>> GetRoles();
    }
}
