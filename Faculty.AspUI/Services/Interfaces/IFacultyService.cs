using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Faculty.Common.Dto.Faculty;

namespace Faculty.AspUI.Services.Interfaces
{
    public interface IFacultyService
    {
        Task<IEnumerable<FacultyDisplayDto>> GetFaculties();

        Task<FacultyDto> GetFaculty(int id);

        Task<HttpResponseMessage> CreateFaculty(FacultyDto facultyDto);

        Task<HttpResponseMessage> DeleteFaculty(int id);

        Task<HttpResponseMessage> EditFaculty(FacultyDto facultyDto);
    }
}
