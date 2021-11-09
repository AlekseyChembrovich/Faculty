using AutoMapper;
using Faculty.Common.Dto.Group;
using Faculty.Common.Dto.Curator;
using Faculty.Common.Dto.Faculty;
using Faculty.Common.Dto.Student;
using Faculty.DataAccessLayer.Models;
using Faculty.Common.Dto.Specialization;

namespace Faculty.ResourceServer.Tools
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
            CreateMap<Curator, CuratorDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(x => x.Surname))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(dest => dest.Doublename, opt => opt.MapFrom(x => x.Doublename))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(x => x.Phone));
            CreateMap<CuratorDto, Curator>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
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
            CreateMap<DataAccessLayer.Models.Faculty, FacultyDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.StartDateEducation, opt => opt.MapFrom(x => x.StartDateEducation))
                .ForMember(dest => dest.CountYearEducation, opt => opt.MapFrom(x => x.CountYearEducation))
                .ForMember(dest => dest.CuratorId, opt => opt.MapFrom(x => x.CuratorId))
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(x => x.StudentId))
                .ForMember(dest => dest.GroupId, opt => opt.MapFrom(x => x.GroupId));
            CreateMap<FacultyDto, DataAccessLayer.Models.Faculty>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.StartDateEducation, opt => opt.MapFrom(x => x.StartDateEducation))
                .ForMember(dest => dest.CountYearEducation, opt => opt.MapFrom(x => x.CountYearEducation))
                .ForMember(dest => dest.CuratorId, opt => opt.MapFrom(x => x.CuratorId))
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(x => x.StudentId))
                .ForMember(dest => dest.GroupId, opt => opt.MapFrom(x => x.GroupId));

            CreateMap<Group, GroupDisplayDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(dest => dest.SpecializationName, opt => opt.MapFrom(x => x.Specialization.Name));
            CreateMap<Group, GroupDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(dest => dest.SpecializationId, opt => opt.MapFrom(x => x.SpecializationId));
            CreateMap<GroupDto, Group>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(dest => dest.SpecializationId, opt => opt.MapFrom(x => x.SpecializationId));

            CreateMap<Specialization, SpecializationDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name));
            CreateMap<SpecializationDto, Specialization>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name));

            CreateMap<Student, StudentDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(x => x.Surname))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(dest => dest.Doublename, opt => opt.MapFrom(x => x.Doublename));
            CreateMap<StudentDto, Student>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(x => x.Surname))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(dest => dest.Doublename, opt => opt.MapFrom(x => x.Doublename));
        }
    }
}
