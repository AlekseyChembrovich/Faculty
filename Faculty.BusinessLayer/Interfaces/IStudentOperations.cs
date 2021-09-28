using Faculty.BusinessLayer.ModelsDto;
using Faculty.BusinessLayer.Interfaces.BaseInterfaces;
using Faculty.BusinessLayer.ModelsDto.StudentDto;

namespace Faculty.BusinessLayer.Interfaces
{
    public interface IStudentOperations : IDisplay<DisplayStudentDto>, ICreate<CreateStudentDto>, IDelete, IEdit<EditStudentDto>
    {
    }
}
