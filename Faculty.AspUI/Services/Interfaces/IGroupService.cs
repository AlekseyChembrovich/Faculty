using System.Net.Http;
using System.Threading.Tasks;
using Faculty.Common.Dto.Group;
using System.Collections.Generic;

namespace Faculty.AspUI.Services.Interfaces
{
    public interface IGroupService
    {
        Task<IEnumerable<GroupDisplayDto>> GetGroups();

        Task<GroupDto> GetGroup(int id);

        Task<HttpResponseMessage> CreateGroup(GroupDto groupDto);

        Task<HttpResponseMessage> DeleteGroup(int id);

        Task<HttpResponseMessage> EditGroup(GroupDto groupDto);
    }
}
