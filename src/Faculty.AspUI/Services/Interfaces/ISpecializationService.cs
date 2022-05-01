using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Faculty.Common.Dto.Specialization;

namespace Faculty.AspUI.Services.Interfaces
{
    /// <summary>
    /// Interface specialization service.
    /// </summary>
    public interface ISpecializationService
    {
        /// <summary>
        /// Method for getting all specialization list.
        /// </summary>
        /// <returns>An instance of the Task class typed by IEnumerable interface of specialization for display.</returns>
        Task<IEnumerable<SpecializationDto>> GetSpecializations();

        /// <summary>
        /// Method for getting specialization by id.
        /// </summary>
        /// <param name="id">Specialization id.</param>
        /// <returns>An instance of the Task class typed by SpecializationDto class.</returns>
        Task<SpecializationDto> GetSpecialization(int id);

        /// <summary>
        /// Method for creating specialization.
        /// </summary>
        /// <param name="specializationDto">Specialization data transfer object.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        Task<HttpResponseMessage> CreateSpecialization(SpecializationDto specializationDto);

        /// <summary>
        /// Method for deleting specialization.
        /// </summary>
        /// <param name="id">Specialization id.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        Task<HttpResponseMessage> DeleteSpecialization(int id);

        /// <summary>
        /// Method for editing specialization.
        /// </summary>
        /// <param name="specializationDto">Specialization data transfer object.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        Task<HttpResponseMessage> EditSpecialization(SpecializationDto specializationDto);
    }
}
