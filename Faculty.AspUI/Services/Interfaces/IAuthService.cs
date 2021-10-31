using System.Net.Http;
using System.Threading.Tasks;
using Faculty.AspUI.ViewModels.LoginRegister;

namespace Faculty.AspUI.Services.Interfaces
{
    public interface IAuthService
    {
        Task<HttpResponseMessage> Login(LoginUser loginUser);

        Task<HttpResponseMessage> Register(RegisterUser registerUser);
    }
}
