using Faculty.BusinessLayer.ModelsDto;
using Faculty.BusinessLayer.Interfaces.BaseInterfaces;

namespace Faculty.BusinessLayer.Interfaces
{
    public interface ISpecializationOperations : IDisplay<DisplaySpecializationDto>, ICreate<CreateSpecializationDto>, IDelete, IEdit<EditSpecializationDto>
    {
    }
}
