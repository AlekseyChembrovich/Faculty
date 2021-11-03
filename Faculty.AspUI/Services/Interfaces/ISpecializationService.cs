using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Faculty.AspUI.ViewModels.Specialization;

namespace Faculty.AspUI.Services.Interfaces
{
    public interface ISpecializationService
    {
        Task<IEnumerable<SpecializationDisplayModify>> GetSpecializations();

        Task<SpecializationDisplayModify> GetSpecialization(int id);

        Task<HttpResponseMessage> CreateSpecialization(SpecializationAdd specializationAdd);

        Task<HttpResponseMessage> DeleteSpecialization(int id);

        Task<HttpResponseMessage> EditSpecialization(SpecializationDisplayModify specializationModify);
    }
}
