using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Faculty.AspUI.ViewModels.Faculty;

namespace Faculty.AspUI.Services.Interfaces
{
    public interface IFacultyService
    {
        Task<IEnumerable<FacultyDisplay>> GetFaculties();

        Task<FacultyModify> GetFaculty(int id);

        Task<HttpResponseMessage> CreateFaculty(FacultyAdd facultyAdd);

        Task<HttpResponseMessage> DeleteFaculty(int id);

        Task<HttpResponseMessage> EditFaculty(FacultyModify facultyModify);
    }
}
