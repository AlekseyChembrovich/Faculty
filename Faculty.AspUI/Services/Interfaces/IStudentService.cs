using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Faculty.Common.Dto.Student;

namespace Faculty.AspUI.Services.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDto>> GetStudents();

        Task<StudentDto> GetStudent(int id);

        Task<HttpResponseMessage> CreateStudent(StudentDto studentDto);

        Task<HttpResponseMessage> DeleteStudent(int id);

        Task<HttpResponseMessage> EditStudent(StudentDto studentDto);
    }
}
