using System.Collections.Generic;
using Faculty.BusinessLayer.Dto.Specialization;

namespace Faculty.BusinessLayer.Interfaces
{
    public interface ISpecializationService
    {
        void Create(SpecializationAddDto model);
        void Delete(int id);
        IEnumerable<SpecializationDisplayModifyDto> GetAll();
        SpecializationDisplayModifyDto GetById(int id);
        void Edit(SpecializationDisplayModifyDto model);
    }
}
