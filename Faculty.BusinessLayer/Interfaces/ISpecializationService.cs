using Faculty.BusinessLayer.ModelsDTO;
using Faculty.BusinessLayer.Interfaces.BaseInterfaces;

namespace Faculty.BusinessLayer.Interfaces
{
    public interface ISpecializationService : IDisplay<SpecializationDTO>, ICreate<SpecializationDTO>, IDelete<SpecializationDTO>, IEdit<SpecializationDTO>
    {
    }
}
