using System.Collections.Generic;
using Faculty.BusinessLayer.ModelsDto;

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
