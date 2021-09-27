using Faculty.BusinessLayer.ModelsDto;
using Faculty.BusinessLayer.ModelsBusiness;
using Faculty.BusinessLayer.Interfaces.BaseInterfaces;

namespace Faculty.BusinessLayer.Interfaces
{
    public interface IFacultyOperations : IDisplay<DisplayFacultyDto>, ICreate<CreateFacultyDto>, IDelete, IEdit<EditFacultyDto>
    {
        ModelElementFaculty CreateViewModelFaculty();
    }
}
