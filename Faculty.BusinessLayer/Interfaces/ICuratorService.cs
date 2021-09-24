using Faculty.BusinessLayer.ModelsDTO;
using Faculty.BusinessLayer.Interfaces.BaseInterfaces;

namespace Faculty.BusinessLayer.Interfaces
{
    public interface ICuratorService : IDisplay<CuratorDTO>, ICreate<CuratorDTO>, IDelete<CuratorDTO>, IEdit<CuratorDTO>
    {
    }
}
