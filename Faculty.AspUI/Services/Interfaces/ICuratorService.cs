using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Faculty.AspUI.ViewModels.Curator;

namespace Faculty.AspUI.Services.Interfaces
{
    public interface ICuratorService
    {
        Task<IEnumerable<CuratorDisplayModify>> GetCurators();

        Task<CuratorDisplayModify> GetCurator(int id);

        Task<HttpResponseMessage> CreateCurator(CuratorAdd curatorAdd);

        Task<HttpResponseMessage> DeleteCurator(int id);

        Task<HttpResponseMessage> EditCurator(CuratorDisplayModify curatorModify);
    }
}
