using System.Collections.Generic;
using Faculty.BusinessLayer.ModelsDTO;

namespace Faculty.BusinessLayer.ModelsBusiness
{
    public class ViewModelFaculty
    {
        public List<StudentDTO> Students;
        public List<CuratorDTO> Curators;
        public List<GroupDTO> Groups;

        public ViewModelFaculty(List<StudentDTO> students, List<CuratorDTO> curator, List<GroupDTO> groups)
        {
            Students = students;
            Curators = curator;
            Groups = groups;
        }
    }
}
