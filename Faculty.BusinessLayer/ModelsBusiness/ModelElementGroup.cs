using System.Collections.Generic;
using Faculty.BusinessLayer.ModelsDto;

namespace Faculty.BusinessLayer.ModelsBusiness
{
    public class ModelElementGroup
    {
        public List<DisplaySpecializationDto> Specializations;

        public ModelElementGroup(List<DisplaySpecializationDto> specializations)
        {
            Specializations = specializations;
        }
    }
}
