using Faculty.BusinessLayer.ModelsDto;
using Faculty.BusinessLayer.ModelsBusiness;
using Faculty.BusinessLayer.Interfaces.BaseInterfaces;

namespace Faculty.BusinessLayer.Interfaces
{
    public interface IFacultyOperations : IDisplay<FacultyDto>, ICreate<FacultyDto>, IDelete<FacultyDto>, IEdit<FacultyDto>
    {
        ModelElementFaculty CreateViewModelFaculty();
    }
}
