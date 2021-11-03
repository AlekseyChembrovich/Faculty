using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Faculty.AspUI.ViewModels.Group;

namespace Faculty.AspUI.Services.Interfaces
{
    public interface IGroupService
    {
        Task<IEnumerable<GroupDisplay>> GetGroups();

        Task<GroupModify> GetGroup(int id);

        Task<HttpResponseMessage> CreateGroup(GroupAdd groupAdd);

        Task<HttpResponseMessage> DeleteGroup(int id);

        Task<HttpResponseMessage> EditGroup(GroupModify groupModify);
    }
}
