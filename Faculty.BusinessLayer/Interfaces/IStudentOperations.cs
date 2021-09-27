using Faculty.BusinessLayer.ModelsDto;
using Faculty.BusinessLayer.Interfaces.BaseInterfaces;

namespace Faculty.BusinessLayer.Interfaces
{
    public interface IStudentOperations : IDisplay<DisplayStudentDto>, ICreate<CreateStudentDto>, IDelete, IEdit<EditStudentDto>
    {
    }
}
