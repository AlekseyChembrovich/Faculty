using Faculty.BusinessLayer.ModelsDto;
using Faculty.BusinessLayer.Interfaces.BaseInterfaces;

namespace Faculty.BusinessLayer.Interfaces
{
    public interface ISpecializationOperations : IDisplay<SpecializationDto>, ICreate<SpecializationDto>, IDelete<SpecializationDto>, IEdit<SpecializationDto>
    {
    }
}
