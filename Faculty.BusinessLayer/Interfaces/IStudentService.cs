using System.Collections.Generic;
using Faculty.BusinessLayer.Dto.Student;

namespace Faculty.BusinessLayer.Interfaces
{
    public interface IStudentService
    {
        void Create(StudentAddDto model);
        void Delete(int id);
        IEnumerable<StudentDisplayModifyDto> GetAll();
        StudentDisplayModifyDto GetById(int id);
        void Edit(StudentDisplayModifyDto model);
    }
}
