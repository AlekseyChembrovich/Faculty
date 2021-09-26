using AutoMapper;
using System.Linq;
using Faculty.DataAccessLayer;
using System.Collections.Generic;
using Faculty.DataAccessLayer.Models;
using Faculty.BusinessLayer.ModelsDto;
using Faculty.BusinessLayer.Interfaces;
using Faculty.BusinessLayer.ModelsBusiness;
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

        public FacultyDto GetModel(int id)
        {
            var model = _repositoryFaculty.GetById(id);
            Mapper.Initialize(cfg => cfg.CreateMap<DataAccessLayer.Models.Faculty, FacultyDto>());
            return Mapper.Map<DataAccessLayer.Models.Faculty, FacultyDto>(model);
        }

        public List<FacultyDto> GetList()
        {
            var models = _repositoryFaculty.GetAllIncludeForeignKey().ToList();
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DataAccessLayer.Models.Faculty, FacultyDto>()
                    .ForMember("CuratorSurname", opt => opt.MapFrom(src => src.Curator.Surname))
                    .ForMember("StudentSurname", opt => opt.MapFrom(src => src.Student.Surname))
                    .ForMember("GroupName", opt => opt.MapFrom(src => src.Group.Name));
            });
            return Mapper.Map<List<DataAccessLayer.Models.Faculty>, List<FacultyDto>>(models);
        }

        public void Create(FacultyDto modelDto)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<FacultyDto, DataAccessLayer.Models.Faculty>());
            _repositoryFaculty.Insert(Mapper.Map<FacultyDto, DataAccessLayer.Models.Faculty>(modelDto));
        }

        public void Delete(int id)
        {
            var model = _repositoryFaculty.GetById(id);
            _repositoryFaculty.Delete(model);
        }

        public void Edit(FacultyDto modelDto)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<FacultyDto, DataAccessLayer.Models.Faculty>());
            _repositoryFaculty.Update(Mapper.Map<FacultyDto, DataAccessLayer.Models.Faculty>(modelDto));
        }

        public ModelElementFaculty CreateViewModelFaculty()
        {
            var modelsStudent = _repositoryStudent.GetAll().ToList();
            Mapper.Initialize(cfg => cfg.CreateMap<Student, StudentDto>());
            var modelsStudentDto = Mapper.Map<List<Student>, List<StudentDto>>(modelsStudent);
            var modelsCurator = _repositoryCurator.GetAll().ToList();
            Mapper.Initialize(cfg => cfg.CreateMap<Curator, CuratorDto>());
            var modelsCuratorDto = Mapper.Map<List<Curator>, List<CuratorDto>>(modelsCurator);
            var modelsGroup = _repositoryGroup.GetAll().ToList();
            Mapper.Initialize(cfg => cfg.CreateMap<Group, GroupDto>());
            var modelsGroupDto = Mapper.Map<List<Group>, List<GroupDto>>(modelsGroup);
            return new ModelElementFaculty(modelsStudentDto, modelsCuratorDto, modelsGroupDto);
        }
    }
}
