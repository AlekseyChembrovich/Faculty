using Faculty.BusinessLayer.ModelsDto;
using Faculty.BusinessLayer.ModelsBusiness;
using Faculty.BusinessLayer.Interfaces.BaseInterfaces;

namespace Faculty.BusinessLayer.Interfaces
{
    public interface IGroupOperations : IDisplay<DisplayGroupDto>, ICreate<CreateGroupDto>, IDelete, IEdit<EditGroupDto>
    {
        ModelElementGroup CreateViewModelGroup();
    }
}
