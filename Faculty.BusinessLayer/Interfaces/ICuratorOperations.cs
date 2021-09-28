using Faculty.BusinessLayer.ModelsDto;
using Faculty.BusinessLayer.Interfaces.BaseInterfaces;
using Faculty.BusinessLayer.ModelsDto.CuratorDto;

namespace Faculty.BusinessLayer.Interfaces
{
    public interface ICuratorOperations : IDisplay<DisplayCuratorDto>, ICreate<CreateCuratorDto>, IDelete, IEdit<EditCuratorDto>
    {
    }
}
