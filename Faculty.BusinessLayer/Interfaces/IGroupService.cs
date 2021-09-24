using Faculty.BusinessLayer.ModelsDTO;
using Faculty.BusinessLayer.ModelsBusiness;
using Faculty.BusinessLayer.Interfaces.BaseInterfaces;

namespace Faculty.BusinessLayer.Interfaces
{
    public interface IGroupService : IDisplay<GroupDTO>, ICreate<GroupDTO>, IDelete<GroupDTO>, IEdit<GroupDTO>
    {
        ViewModelGroup CreateViewModelGroup();
    }
}
