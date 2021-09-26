using System.Collections.Generic;
using Faculty.BusinessLayer.ModelsDto;

namespace Faculty.BusinessLayer.ModelsBusiness
{
    public class ModelElementFaculty
    {
        public List<StudentDto> Students;
        public List<CuratorDto> Curators;
        public List<GroupDto> Groups;

        public ModelElementFaculty(List<StudentDto> students, List<CuratorDto> curator, List<GroupDto> groups)
        {
            Students = students;
            Curators = curator;
            Groups = groups;
        }
    }
}
