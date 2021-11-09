using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Faculty.Common.Dto.Faculty;

namespace Faculty.AspUI.Services.Interfaces
{
    /// <summary>
    /// Interface faculty service.
    /// </summary>
    public interface IFacultyService
    {
        /// <summary>
        /// Method for getting all faculty list.
        /// </summary>
        /// <returns>An instance of the Task class typed by IEnumerable interface of faculty for display.</returns>
        Task<IEnumerable<FacultyDisplayDto>> GetFaculties();

        /// <summary>
        /// Method for getting faculty by id.
        /// </summary>
        /// <param name="id">Faculty id.</param>
        /// <returns>An instance of the Task class typed by FacultyDto class.</returns>
        Task<FacultyDto> GetFaculty(int id);

        /// <summary>
        /// Method for creating faculty.
        /// </summary>
        /// <param name="facultyDto">Faculty data transfer object.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        Task<HttpResponseMessage> CreateFaculty(FacultyDto facultyDto);

        /// <summary>
        /// Method for deleting faculty.
        /// </summary>
        /// <param name="id">Faculty id.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        Task<HttpResponseMessage> DeleteFaculty(int id);

        /// <summary>
        /// Method for editing faculty.
        /// </summary>
        /// <param name="facultyDto">Faculty data transfer object.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        Task<HttpResponseMessage> EditFaculty(FacultyDto facultyDto);
    }
}
