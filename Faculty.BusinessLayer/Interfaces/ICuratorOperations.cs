using Faculty.BusinessLayer.ModelsDto;
using Faculty.BusinessLayer.Interfaces.BaseInterfaces;

namespace Faculty.BusinessLayer.Interfaces
{
    public interface ICuratorOperations : IDisplay<DisplayCuratorDto>, ICreate<CreateCuratorDto>, IDelete, IEdit<EditCuratorDto>
    {
    }
}
