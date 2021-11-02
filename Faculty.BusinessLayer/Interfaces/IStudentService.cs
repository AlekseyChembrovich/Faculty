using System.Collections.Generic;
using Faculty.BusinessLayer.Dto.Student;

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
        StudentAddDto Create(StudentAddDto dto);

        /// <summary>
        /// Method for deleting a exist entity.
        /// </summary>
        /// <param name="id">Id exist entity.</param>
        void Delete(int id);

        /// <summary>
        /// Method for receive set of entity.
        /// </summary>
        /// <returns>Dto set.</returns>
        IEnumerable<StudentDisplayModifyDto> GetAll();

        /// <summary>
        /// Method for receive dto.
        /// </summary>
        /// <param name="id">Id exist entity.</param>
        /// <returns>Modify Dto.</returns>
        StudentDisplayModifyDto GetById(int id);

        /// <summary>
        /// Method for changing a exist entity.
        /// </summary>
        /// <param name="dto">Modify Dto.</param>
        void Edit(StudentDisplayModifyDto dto);
    }
}
