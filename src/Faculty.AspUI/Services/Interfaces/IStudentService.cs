using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Faculty.Common.Dto.Student;

namespace Faculty.AspUI.Services.Interfaces
{
    /// <summary>
    /// Interface student service.
    /// </summary>
    public interface IStudentService
    {
        /// <summary>
        /// Method for getting all student list.
        /// </summary>
        /// <returns>An instance of the Task class typed by IEnumerable interface of student for display.</returns>
        Task<IEnumerable<StudentDto>> GetStudents();

        /// <summary>
        /// Method for getting student by id.
        /// </summary>
        /// <param name="id">Student id.</param>
        /// <returns>An instance of the Task class typed by StudentDto class.</returns>
        Task<StudentDto> GetStudent(int id);

        /// <summary>
        /// Method for creating student.
        /// </summary>
        /// <param name="studentDto">Student data transfer object.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        Task<HttpResponseMessage> CreateStudent(StudentDto studentDto);

        /// <summary>
        /// Method for deleting student.
        /// </summary>
        /// <param name="id">Student id.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        Task<HttpResponseMessage> DeleteStudent(int id);

        /// <summary>
        /// Method for editing student.
        /// </summary>
        /// <param name="studentDto">Student data transfer object.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        Task<HttpResponseMessage> EditStudent(StudentDto studentDto);
    }
}
