using Faculty.BusinessLayer.ModelsDTO;
using Faculty.BusinessLayer.ModelsBusiness;
using Faculty.BusinessLayer.Interfaces.BaseInterfaces;

namespace Faculty.BusinessLayer.Interfaces
{
    public interface IFacultyService : IDisplay<FacultyDTO>, ICreate<FacultyDTO>, IDelete<FacultyDTO>, IEdit<FacultyDTO>
    {
        ViewModelFaculty CreateViewModelFaculty();
    }
}
