using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Faculty.AspUI.ViewModels.User;

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
        Task<IEnumerable<UserDisplay>> GetUsers();

        /// <summary>
        /// Method for getting user by id.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns>An instance of the Task class typed by UserModify class.</returns>
        Task<UserModify> GetUser(string id);

        /// <summary>
        /// Method for creating user.
        /// </summary>
        /// <param name="userAdd">Model user for add.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        Task<HttpResponseMessage> CreateUser(UserAdd userAdd);

        /// <summary>
        /// Method for deleting user.
        /// </summary>
        /// <param name="id">Model user for delete.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        Task<HttpResponseMessage> DeleteUser(string id);

        /// <summary>
        /// Method for editing user.
        /// </summary>
        /// <param name="userModify">Model user for modify.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        Task<HttpResponseMessage> EditUser(UserModify userModify);

        /// <summary>
        /// Method for editing user password.
        /// </summary>
        /// <param name="userEditPass">Model user for modify password.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        Task<HttpResponseMessage> EditPasswordUser(UserModifyPassword userEditPass);

        /// <summary>
        /// Method for getting existing roles.
        /// </summary>
        /// <returns>An instance of the Task class typed by IEnumerable interface of strings.</returns>
        Task<IEnumerable<string>> GetRoles();
    }
}
