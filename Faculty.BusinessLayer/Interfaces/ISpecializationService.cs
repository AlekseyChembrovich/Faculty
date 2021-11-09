using System.Collections.Generic;
using Faculty.Common.Dto.Specialization;

namespace Faculty.BusinessLayer.Interfaces
{
    /// <summary>
    /// Interface service.
    /// </summary>
    public interface ISpecializationService
    {
        /// <summary>
        /// Method for creating a new entity.
        /// </summary>
        /// <param name="dto">Add Dto.</param>
        SpecializationDto Create(SpecializationDto dto);

        /// <summary>
        /// Method for deleting a exist entity.
        /// </summary>
        /// <param name="id">Id exist entity.</param>
        void Delete(int id);

        /// <summary>
        /// Method for receive set of entity.
        /// </summary>
        /// <returns>Dto set.</returns>
        IEnumerable<SpecializationDto> GetAll();

        /// <summary>
        /// Method for receive dto.
        /// </summary>
        /// <param name="id">Id exist entity.</param>
        /// <returns>Modify Dto.</returns>
        SpecializationDto GetById(int id);

        /// <summary>
        /// Method for changing a exist entity.
        /// </summary>
        /// <param name="dto">Modify Dto.</param>
        void Edit(SpecializationDto dto);
    }
}
