using Faculty.BusinessLayer.ModelsDto;
using Faculty.BusinessLayer.ModelsBusiness;
using Faculty.BusinessLayer.Interfaces.BaseInterfaces;
using Faculty.BusinessLayer.ModelsDto.FacultyDto;

namespace Faculty.BusinessLayer.Interfaces
{
    public interface IFacultyOperations : IDisplay<DisplayFacultyDto>, ICreate<CreateFacultyDto>, IDelete, IEdit<EditFacultyDto>
    {
        ModelElementFaculty CreateViewModelFaculty();
    }
}
