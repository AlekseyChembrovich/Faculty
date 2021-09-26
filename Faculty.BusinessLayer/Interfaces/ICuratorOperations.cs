using Faculty.BusinessLayer.ModelsDto;
using Faculty.BusinessLayer.Interfaces.BaseInterfaces;

namespace Faculty.BusinessLayer.Interfaces
{
    public interface ICuratorOperations : IDisplay<CuratorDto>, ICreate<CuratorDto>, IDelete<CuratorDto>, IEdit<CuratorDto>
    {
    }
}
