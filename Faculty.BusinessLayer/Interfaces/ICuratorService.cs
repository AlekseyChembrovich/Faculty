using System.Collections.Generic;
using Faculty.BusinessLayer.Dto.Curator;

namespace Faculty.BusinessLayer.Interfaces
{
    public interface ICuratorService
    {
        void Create(CuratorAddDto model);
        void Delete(int id);
        IEnumerable<CuratorDisplayModifyDto> GetAll();
        CuratorDisplayModifyDto GetById(int id);
        void Edit(CuratorDisplayModifyDto model);
    }
}
