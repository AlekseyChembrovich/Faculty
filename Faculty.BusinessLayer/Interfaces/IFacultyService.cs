using System.Collections.Generic;
using Faculty.BusinessLayer.Dto.Faculty;

namespace Faculty.BusinessLayer.Interfaces
{
    public interface IFacultyService
    {
        void Create(FacultyAddDto model);
        void Delete(int id);
        IEnumerable<FacultyDisplayDto> GetAll();
        FacultyModifyDto GetById(int id);
        void Edit(FacultyModifyDto model);
    }
}
