using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Faculty.AspUI.ViewModels.User;

namespace Faculty.AspUI.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDisplay>> GetUsers();

        Task<UserModify> GetUser(string id);

        Task<HttpResponseMessage> CreateUser(UserAdd userAdd);

        Task<HttpResponseMessage> DeleteUser(string id);

        Task<HttpResponseMessage> EditUser(UserModify userModify);

        Task<HttpResponseMessage> EditPasswordUser(UserModifyPassword userEditPass);

        Task<IEnumerable<string>> GetRoles();
    }
}
