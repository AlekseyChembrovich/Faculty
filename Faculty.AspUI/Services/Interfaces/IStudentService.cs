using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Faculty.AspUI.ViewModels.Student;

namespace Faculty.AspUI.Services.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDisplayModify>> GetStudents();

        Task<StudentDisplayModify> GetStudent(int id);

        Task<HttpResponseMessage> CreateStudent(StudentAdd studentAdd);

        Task<HttpResponseMessage> DeleteStudent(int id);

        Task<HttpResponseMessage> EditStudent(StudentDisplayModify studentModify);
    }
}
