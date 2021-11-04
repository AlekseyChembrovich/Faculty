using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Faculty.Common.Dto.Specialization;

namespace Faculty.AspUI.Services.Interfaces
{
    public interface ISpecializationService
    {
        Task<IEnumerable<SpecializationDto>> GetSpecializations();

        Task<SpecializationDto> GetSpecialization(int id);

        Task<HttpResponseMessage> CreateSpecialization(SpecializationDto specializationDto);

        Task<HttpResponseMessage> DeleteSpecialization(int id);

        Task<HttpResponseMessage> EditSpecialization(SpecializationDto specializationDto);
    }
}
