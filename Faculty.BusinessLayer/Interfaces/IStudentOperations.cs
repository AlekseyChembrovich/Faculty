using Faculty.BusinessLayer.ModelsDto;
using Faculty.BusinessLayer.Interfaces.BaseInterfaces;

namespace Faculty.BusinessLayer.Interfaces
{
    public interface IStudentOperations : IDisplay<StudentDto>, ICreate<StudentDto>, IDelete<StudentDto>, IEdit<StudentDto>
    {
    }
}
