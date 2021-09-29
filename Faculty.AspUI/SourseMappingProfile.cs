using AutoMapper;
using Faculty.AspUI.ViewModels.Group;
using Faculty.BusinessLayer.Dto.Group;
using Faculty.AspUI.ViewModels.Curator;
using Faculty.AspUI.ViewModels.Faculty;
using Faculty.AspUI.ViewModels.Student;
using Faculty.BusinessLayer.Dto.Student;
using Faculty.BusinessLayer.Dto.Curator;
using Faculty.BusinessLayer.Dto.Faculty;
using Faculty.AspUI.ViewModels.Specialization;
using Faculty.BusinessLayer.Dto.Specialization;

namespace Faculty.AspUI
{
    public class SourceMappingProfile : Profile
    {
        public SourceMappingProfile()
        {
            CreateMap<CuratorDisplayModifyDto, CuratorDisplayModify>();
            CreateMap<CuratorDisplayModify, CuratorDisplayModifyDto>();
            CreateMap<CuratorAdd, CuratorAddDto>();

            CreateMap<FacultyDisplayDto, FacultyDisplay>();
            CreateMap<FacultyModifyDto, FacultyModify>();
            CreateMap<FacultyModify, FacultyModifyDto>();
            CreateMap<FacultyAdd, FacultyAddDto>();

            CreateMap<GroupDisplayDto, GroupDisplay>();
            CreateMap<GroupModifyDto, GroupModify>();
            CreateMap<GroupModify, GroupModifyDto>();
            CreateMap<GroupAdd, GroupAddDto>();

            CreateMap<SpecializationDisplayModifyDto, SpecializationDisplayModify>();
            CreateMap<SpecializationDisplayModify, SpecializationDisplayModifyDto>();
            CreateMap<SpecializationAdd, SpecializationAddDto>();

            CreateMap<StudentDisplayModifyDto, StudentDisplayModify>();
            CreateMap<StudentDisplayModify, StudentDisplayModifyDto>();
            CreateMap<StudentAdd, StudentAddDto>();
        }
    }
}
