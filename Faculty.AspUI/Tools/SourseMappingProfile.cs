using AutoMapper;
using Faculty.Common.Dto.Group;
using Faculty.Common.Dto.Curator;
using Faculty.Common.Dto.Faculty;
using Faculty.Common.Dto.Student;
using Faculty.AspUI.ViewModels.Group;
using Faculty.AspUI.ViewModels.Curator;
using Faculty.AspUI.ViewModels.Faculty;
using Faculty.AspUI.ViewModels.Student;
using Faculty.Common.Dto.Specialization;
using Faculty.AspUI.ViewModels.Specialization;

namespace Faculty.AspUI.Tools
{
    /// <summary>
    /// Mapping source.
    /// </summary>
    public class SourceMappingProfile : Profile
    {
        /// <summary>
        /// Constructor for set up mapping.
        /// </summary>
        public SourceMappingProfile()
        {
            CreateMap<CuratorDto, CuratorDisplayModify>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(x => x.Surname))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(dest => dest.Doublename, opt => opt.MapFrom(x => x.Doublename))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(x => x.Phone));
            CreateMap<CuratorDisplayModify, CuratorDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(x => x.Surname))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(dest => dest.Doublename, opt => opt.MapFrom(x => x.Doublename))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(x => x.Phone));
            CreateMap<CuratorAdd, CuratorDto>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(x => x.Surname))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(dest => dest.Doublename, opt => opt.MapFrom(x => x.Doublename))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(x => x.Phone));

            CreateMap<FacultyDisplayDto, FacultyDisplay>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.StartDateEducation, opt => opt.MapFrom(x => x.StartDateEducation))
                .ForMember(dest => dest.CountYearEducation, opt => opt.MapFrom(x => x.CountYearEducation))
                .ForMember(dest => dest.CuratorSurname, opt => opt.MapFrom(x => x.CuratorSurname))
                .ForMember(dest => dest.StudentSurname, opt => opt.MapFrom(x => x.StudentSurname))
                .ForMember(dest => dest.GroupName, opt => opt.MapFrom(x => x.GroupName));
            CreateMap<FacultyDto, FacultyModify>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.StartDateEducation, opt => opt.MapFrom(x => x.StartDateEducation))
                .ForMember(dest => dest.CountYearEducation, opt => opt.MapFrom(x => x.CountYearEducation))
                .ForMember(dest => dest.CuratorId, opt => opt.MapFrom(x => x.CuratorId))
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(x => x.StudentId))
                .ForMember(dest => dest.GroupId, opt => opt.MapFrom(x => x.GroupId));
            CreateMap<FacultyModify, FacultyDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.StartDateEducation, opt => opt.MapFrom(x => x.StartDateEducation))
                .ForMember(dest => dest.CountYearEducation, opt => opt.MapFrom(x => x.CountYearEducation))
                .ForMember(dest => dest.CuratorId, opt => opt.MapFrom(x => x.CuratorId))
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(x => x.StudentId))
                .ForMember(dest => dest.GroupId, opt => opt.MapFrom(x => x.GroupId));
            CreateMap<FacultyAdd, FacultyDto>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.StartDateEducation, opt => opt.MapFrom(x => x.StartDateEducation))
                .ForMember(dest => dest.CountYearEducation, opt => opt.MapFrom(x => x.CountYearEducation))
                .ForMember(dest => dest.CuratorId, opt => opt.MapFrom(x => x.CuratorId))
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(x => x.StudentId))
                .ForMember(dest => dest.GroupId, opt => opt.MapFrom(x => x.GroupId));

            CreateMap<GroupDisplayDto, GroupDisplay>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(dest => dest.SpecializationName, opt => opt.MapFrom(x => x.SpecializationName));
            CreateMap<GroupDto, GroupModify>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(dest => dest.SpecializationId, opt => opt.MapFrom(x => x.SpecializationId));
            CreateMap<GroupModify, GroupDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(dest => dest.SpecializationId, opt => opt.MapFrom(x => x.SpecializationId));
            CreateMap<GroupAdd, GroupDto>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(dest => dest.SpecializationId, opt => opt.MapFrom(x => x.SpecializationId));

            CreateMap<SpecializationDto, SpecializationDisplayModify>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name));
            CreateMap<SpecializationDisplayModify, SpecializationDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name));
            CreateMap<SpecializationAdd, SpecializationDto>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name));

            CreateMap<StudentDto, StudentDisplayModify>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(x => x.Surname))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(dest => dest.Doublename, opt => opt.MapFrom(x => x.Doublename));
            CreateMap<StudentDisplayModify, StudentDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(x => x.Surname))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(dest => dest.Doublename, opt => opt.MapFrom(x => x.Doublename));
            CreateMap<StudentAdd, StudentDto>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(x => x.Surname))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(dest => dest.Doublename, opt => opt.MapFrom(x => x.Doublename));
        }
    }
}
