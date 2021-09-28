using AutoMapper;
using Faculty.DataAccessLayer.Models;
using Faculty.BusinessLayer.ModelsDto.GroupDto;
using Faculty.BusinessLayer.ModelsDto.StudentDto;
using Faculty.BusinessLayer.ModelsDto.CuratorDto;
using Faculty.BusinessLayer.ModelsDto.FacultyDto;
using Faculty.BusinessLayer.ModelsDto.SpecializationDto;

namespace Faculty.BusinessLayer
{
    public class SourceMappingProfile : Profile
    {
        public SourceMappingProfile()
        {
            CreateMap<Curator, DisplayCuratorDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(x => x.Surname))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(dest => dest.Doublename, opt => opt.MapFrom(x => x.Doublename))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(x => x.Phone));
            CreateMap<CreateCuratorDto, Curator>();
            CreateMap<EditCuratorDto, Curator>();
            CreateMap<Curator, EditCuratorDto>();

            CreateMap<DataAccessLayer.Models.Faculty, DisplayFacultyDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.StartDateEducation, opt => opt.MapFrom(x => x.StartDateEducation))
                .ForMember(dest => dest.CountYearEducation, opt => opt.MapFrom(x => x.CountYearEducation))
                .ForMember(dest => dest.CuratorSurname, opt => opt.MapFrom(src => src.Curator.Surname))
                .ForMember(dest => dest.StudentSurname, opt => opt.MapFrom(src => src.Student.Surname))
                .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.Group.Name));
            CreateMap<CreateFacultyDto, DataAccessLayer.Models.Faculty>();
            CreateMap<EditFacultyDto, DataAccessLayer.Models.Faculty>();
            CreateMap<DataAccessLayer.Models.Faculty, EditFacultyDto>();

            CreateMap<Group, DisplayGroupDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(dest => dest.SpecializationName, opt => opt.MapFrom(src => src.Specialization.Name));
            CreateMap<CreateGroupDto, Group>();
            CreateMap<EditGroupDto, Group>();
            CreateMap<Group, EditGroupDto>();

            CreateMap<Specialization, DisplaySpecializationDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name));
            CreateMap<CreateSpecializationDto, Specialization>();
            CreateMap<EditSpecializationDto, Specialization>();
            CreateMap<Specialization, EditSpecializationDto>();

            CreateMap<Student, DisplayStudentDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(x => x.Surname))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(dest => dest.Doublename, opt => opt.MapFrom(x => x.Doublename));
            CreateMap<CreateStudentDto, Student>();
            CreateMap<EditStudentDto, Student>();
            CreateMap<Student, EditStudentDto>();
        }
    }
}
