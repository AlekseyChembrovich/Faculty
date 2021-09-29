using AutoMapper;
using Faculty.DataAccessLayer.Models;
using Faculty.BusinessLayer.Dto.Group;
using Faculty.BusinessLayer.Dto.Student;
using Faculty.BusinessLayer.Dto.Curator;
using Faculty.BusinessLayer.Dto.Faculty;
using Faculty.BusinessLayer.Dto.Specialization;

namespace Faculty.BusinessLayer
{
    public class SourceMappingProfile : Profile
    {
        public SourceMappingProfile()
        {
            CreateMap<Curator, CuratorDisplayModifyDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(x => x.Surname))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(dest => dest.Doublename, opt => opt.MapFrom(x => x.Doublename))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(x => x.Phone));
            CreateMap<CuratorDisplayModifyDto, Curator>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(x => x.Surname))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(dest => dest.Doublename, opt => opt.MapFrom(x => x.Doublename))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(x => x.Phone));
            CreateMap<CuratorAddDto, Curator>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(x => x.Surname))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(dest => dest.Doublename, opt => opt.MapFrom(x => x.Doublename))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(x => x.Phone));

            CreateMap<DataAccessLayer.Models.Faculty, FacultyDisplayDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.StartDateEducation, opt => opt.MapFrom(x => x.StartDateEducation))
                .ForMember(dest => dest.CountYearEducation, opt => opt.MapFrom(x => x.CountYearEducation))
                .ForMember(dest => dest.CuratorSurname, opt => opt.MapFrom(x => x.Curator.Surname))
                .ForMember(dest => dest.StudentSurname, opt => opt.MapFrom(x => x.Student.Surname))
                .ForMember(dest => dest.GroupName, opt => opt.MapFrom(x => x.Group.Name));
            CreateMap<DataAccessLayer.Models.Faculty, FacultyModifyDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.StartDateEducation, opt => opt.MapFrom(x => x.StartDateEducation))
                .ForMember(dest => dest.CountYearEducation, opt => opt.MapFrom(x => x.CountYearEducation))
                .ForMember(dest => dest.CuratorId, opt => opt.MapFrom(x => x.CuratorId))
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(x => x.StudentId))
                .ForMember(dest => dest.GroupId, opt => opt.MapFrom(x => x.GroupId));
            CreateMap<FacultyModifyDto, DataAccessLayer.Models.Faculty>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.StartDateEducation, opt => opt.MapFrom(x => x.StartDateEducation))
                .ForMember(dest => dest.CountYearEducation, opt => opt.MapFrom(x => x.CountYearEducation))
                .ForMember(dest => dest.CuratorId, opt => opt.MapFrom(x => x.CuratorId))
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(x => x.StudentId))
                .ForMember(dest => dest.GroupId, opt => opt.MapFrom(x => x.GroupId));
            CreateMap<FacultyAddDto, DataAccessLayer.Models.Faculty>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.StartDateEducation, opt => opt.MapFrom(x => x.StartDateEducation))
                .ForMember(dest => dest.CountYearEducation, opt => opt.MapFrom(x => x.CountYearEducation))
                .ForMember(dest => dest.CuratorId, opt => opt.MapFrom(x => x.CuratorId))
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(x => x.StudentId))
                .ForMember(dest => dest.GroupId, opt => opt.MapFrom(x => x.GroupId));

            CreateMap<Group, GroupDisplayDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(dest => dest.SpecializationName, opt => opt.MapFrom(x => x.Specialization.Name));
            CreateMap<Group, GroupModifyDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(dest => dest.SpecializationId, opt => opt.MapFrom(x => x.SpecializationId));
            CreateMap<GroupModifyDto, Group>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(dest => dest.SpecializationId, opt => opt.MapFrom(x => x.SpecializationId));
            CreateMap<GroupAddDto, Group>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(dest => dest.SpecializationId, opt => opt.MapFrom(x => x.SpecializationId));

            CreateMap<Specialization, SpecializationDisplayModifyDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name));
            CreateMap<SpecializationDisplayModifyDto, Specialization>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name));
            CreateMap<SpecializationAddDto, Specialization>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name));

            CreateMap<Student, StudentDisplayModifyDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(x => x.Surname))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(dest => dest.Doublename, opt => opt.MapFrom(x => x.Doublename));
            CreateMap<StudentDisplayModifyDto, Student>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(x => x.Surname))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(dest => dest.Doublename, opt => opt.MapFrom(x => x.Doublename));
            CreateMap<StudentAddDto, Student>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(x => x.Surname))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(dest => dest.Doublename, opt => opt.MapFrom(x => x.Doublename));
        }
    }
}
