using Faculty.BusinessLayer.ModelsDto;
using Faculty.BusinessLayer.ModelsBusiness;
using Faculty.BusinessLayer.Interfaces.BaseInterfaces;

namespace Faculty.BusinessLayer.Interfaces
{
    public interface IGroupOperations : IDisplay<GroupDto>, ICreate<GroupDto>, IDelete<GroupDto>, IEdit<GroupDto>
    {
        ModelElementGroup CreateViewModelGroup();
    }
}
