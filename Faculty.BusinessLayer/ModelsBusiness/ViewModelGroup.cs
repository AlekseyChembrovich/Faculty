using System.Collections.Generic;
using Faculty.BusinessLayer.ModelsDTO;

namespace Faculty.BusinessLayer.ModelsBusiness
{
    public class ViewModelGroup
    {
        public List<SpecializationDTO> Specializations;

        public ViewModelGroup(List<SpecializationDTO> specializations)
        {
            Specializations = specializations;
        }
    }
}
