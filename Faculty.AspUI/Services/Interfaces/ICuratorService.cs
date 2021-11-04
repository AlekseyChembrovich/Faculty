using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Faculty.Common.Dto.Curator;

namespace Faculty.AspUI.Services.Interfaces
{
    public interface ICuratorService
    {
        Task<IEnumerable<CuratorDto>> GetCurators();

        Task<CuratorDto> GetCurator(int id);

        Task<HttpResponseMessage> CreateCurator(CuratorDto curatorDto);

        Task<HttpResponseMessage> DeleteCurator(int id);

        Task<HttpResponseMessage> EditCurator(CuratorDto curatorDto);
    }
}
