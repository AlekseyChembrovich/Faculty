using System.Collections.Generic;
using Faculty.BusinessLayer.ModelsDto;

namespace Faculty.BusinessLayer.ModelsBusiness
{
    public class ModelElementGroup
    {
        public List<SpecializationDto> Specializations;

        public ModelElementGroup(List<SpecializationDto> specializations)
        {
            Specializations = specializations;
        }
    }
}
