using System.Collections.Generic;
using Faculty.Common.Dto.Student;

namespace Faculty.BusinessLayer.Interfaces
{
    /// <summary>
    /// Interface service.
    /// </summary>
    public interface IStudentService
    {
        /// <summary>
        /// Method for creating a new entity.
        /// </summary>
        /// <param name="dto">Add Dto.</param>
        StudentDto Create(StudentDto dto);

        /// <summary>
        /// Method for deleting a exist entity.
        /// </summary>
        /// <param name="id">Id exist entity.</param>
        void Delete(int id);

        /// <summary>
        /// Method for receive set of entity.
        /// </summary>
        /// <returns>Dto set.</returns>
        IEnumerable<StudentDto> GetAll();

        /// <summary>
        /// Method for receive dto.
        /// </summary>
        /// <param name="id">Id exist entity.</param>
        /// <returns>Modify Dto.</returns>
        StudentDto GetById(int id);

        /// <summary>
        /// Method for changing a exist entity.
        /// </summary>
        /// <param name="dto">Modify Dto.</param>
        void Edit(StudentDto dto);
    }
}
