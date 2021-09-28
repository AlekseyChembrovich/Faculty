using System.Collections.Generic;
using Faculty.BusinessLayer.ModelsDto;
using Faculty.BusinessLayer.ModelsDto.CuratorDto;
using Faculty.BusinessLayer.ModelsDto.GroupDto;
using Faculty.BusinessLayer.ModelsDto.StudentDto;

namespace Faculty.BusinessLayer.ModelsBusiness
{
    public class ModelElementFaculty
    {
        public List<DisplayStudentDto> Students;
        public List<DisplayCuratorDto> Curators;
        public List<DisplayGroupDto> Groups;

        public ModelElementFaculty(List<DisplayStudentDto> students, List<DisplayCuratorDto> curator, List<DisplayGroupDto> groups)
        {
            Students = students;
            Curators = curator;
            Groups = groups;
        }
    }
}
