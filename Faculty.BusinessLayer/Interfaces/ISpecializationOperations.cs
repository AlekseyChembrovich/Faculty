using Faculty.BusinessLayer.ModelsDto;
using Faculty.BusinessLayer.Interfaces.BaseInterfaces;
using Faculty.BusinessLayer.ModelsDto.SpecializationDto;

namespace Faculty.BusinessLayer.Interfaces
{
    public interface ISpecializationOperations : IDisplay<DisplaySpecializationDto>, ICreate<CreateSpecializationDto>, IDelete, IEdit<EditSpecializationDto>
    {
    }
}
