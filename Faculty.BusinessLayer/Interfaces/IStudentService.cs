using Faculty.BusinessLayer.ModelsDTO;
using Faculty.BusinessLayer.Interfaces.BaseInterfaces;

namespace Faculty.BusinessLayer.Interfaces
{
    public interface IStudentService : IDisplay<StudentDTO>, ICreate<StudentDTO>, IDelete<StudentDTO>, IEdit<StudentDTO>
    {
    }
}
