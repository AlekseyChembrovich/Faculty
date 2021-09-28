using AutoMapper;
using System.Linq;
using Faculty.DataAccessLayer;
using System.Collections.Generic;
using Faculty.DataAccessLayer.Models;
using Faculty.BusinessLayer.ModelsDto;
using Faculty.BusinessLayer.Interfaces;
using Faculty.BusinessLayer.ModelsBusiness;
using Faculty.BusinessLayer.ModelsDto.CuratorDto;
using Faculty.BusinessLayer.ModelsDto.FacultyDto;
using Faculty.BusinessLayer.ModelsDto.GroupDto;
using Faculty.BusinessLayer.ModelsDto.StudentDto;
using Faculty.DataAccessLayer.RepositoryEntityFramework;

namespace Faculty.BusinessLayer.Services
{
    public class FacultyService : IFacultyOperations
    {
        private readonly IRepositoryFaculty _repositoryFaculty;
        private readonly IRepositoryGroup _repositoryGroup;
        private readonly IRepository<Student> _repositoryStudent;
        private readonly IRepository<Curator> _repositoryCurator;

        public FacultyService(IRepositoryFaculty repositoryFaculty, IRepositoryGroup repositoryGroup, IRepository<Student> repositoryStudent, IRepository<Curator> repositoryCurator)
        {
            _repositoryFaculty = repositoryFaculty;
            _repositoryGroup = repositoryGroup;
            _repositoryStudent = repositoryStudent;
            _repositoryCurator = repositoryCurator;
        }

        public List<DisplayFacultyDto> GetList()
        {
            var models = _repositoryFaculty.GetAllIncludeForeignKey().ToList();
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DataAccessLayer.Models.Faculty, DisplayFacultyDto>()
                    .ForMember("CuratorSurname", opt => opt.MapFrom(src => src.Curator.Surname))
                    .ForMember("StudentSurname", opt => opt.MapFrom(src => src.Student.Surname))
                    .ForMember("GroupName", opt => opt.MapFrom(src => src.Group.Name));
            });
            return Mapper.Map<List<DataAccessLayer.Models.Faculty>, List<DisplayFacultyDto>>(models);
        }

        public void Create(CreateFacultyDto modelDto)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<CreateFacultyDto, DataAccessLayer.Models.Faculty>());
            _repositoryFaculty.Insert(Mapper.Map<CreateFacultyDto, DataAccessLayer.Models.Faculty>(modelDto));
        }

        public void Delete(int id)
        {
            var model = _repositoryFaculty.GetById(id);
            _repositoryFaculty.Delete(model);
        }

        public EditFacultyDto GetModel(int id)
        {
            var model = _repositoryFaculty.GetById(id);
            Mapper.Initialize(cfg => cfg.CreateMap<DataAccessLayer.Models.Faculty, EditFacultyDto>());
            return Mapper.Map<DataAccessLayer.Models.Faculty, EditFacultyDto>(model);
        }

        public void Edit(EditFacultyDto modelDto)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<EditFacultyDto, DataAccessLayer.Models.Faculty>());
            _repositoryFaculty.Update(Mapper.Map<EditFacultyDto, DataAccessLayer.Models.Faculty>(modelDto));
        }

        public ModelElementFaculty CreateViewModelFaculty()
        {
            var modelsStudent = _repositoryStudent.GetAll().ToList();
            Mapper.Initialize(cfg => cfg.CreateMap<Student, DisplayStudentDto>());
            var modelsStudentDto = Mapper.Map<List<Student>, List<DisplayStudentDto>>(modelsStudent);
            var modelsCurator = _repositoryCurator.GetAll().ToList();
            Mapper.Initialize(cfg => cfg.CreateMap<Curator, DisplayCuratorDto>());
            var modelsCuratorDto = Mapper.Map<List<Curator>, List<DisplayCuratorDto>>(modelsCurator);
            var modelsGroup = _repositoryGroup.GetAll().ToList();
            Mapper.Initialize(cfg => cfg.CreateMap<Group, DisplayGroupDto>());
            var modelsGroupDto = Mapper.Map<List<Group>, List<DisplayGroupDto>>(modelsGroup);
            return new ModelElementFaculty(modelsStudentDto, modelsCuratorDto, modelsGroupDto);
        }
    }
}
