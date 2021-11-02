using AutoMapper;
using Faculty.AspUI.ViewModels.Group;
using Faculty.DataAccessLayer.Models;
using Faculty.BusinessLayer.Dto.Group;
using Faculty.AspUI.ViewModels.Curator;
using Faculty.AspUI.ViewModels.Faculty;
using Faculty.AspUI.ViewModels.Student;
using Faculty.BusinessLayer.Dto.Curator;
using Faculty.BusinessLayer.Dto.Faculty;
using Faculty.BusinessLayer.Dto.Student;
using Faculty.AspUI.ViewModels.Specialization;
using Faculty.BusinessLayer.Dto.Specialization;

namespace Faculty.AspUI.Tools
{
    public class SourceMappingProfile : Profile
    {
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
