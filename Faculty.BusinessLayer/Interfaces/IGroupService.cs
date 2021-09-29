using System.Collections.Generic;
using Faculty.BusinessLayer.Dto.Group;

namespace Faculty.BusinessLayer.Interfaces
{
    public interface IGroupService
    {
        void Create(GroupAddDto model);
        void Delete(int id);
        IEnumerable<GroupDisplayDto> GetAll();
        GroupModifyDto GetById(int id);
        void Edit(GroupModifyDto model);
    }
}
