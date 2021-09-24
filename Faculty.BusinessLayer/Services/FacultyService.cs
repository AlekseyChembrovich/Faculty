using AutoMapper;
using System.Linq;
using Faculty.DataAccessLayer;
using System.Collections.Generic;
using Faculty.DataAccessLayer.Models;
using Faculty.BusinessLayer.ModelsDTO;
using Faculty.BusinessLayer.Interfaces;
using Faculty.BusinessLayer.ModelsBusiness;
using Faculty.DataAccessLayer.RepositoryEntityFramework;

namespace Faculty.BusinessLayer.Services
{
    public class FacultyService : IFacultyService
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

        public FacultyDTO GetModel(int id)
        {
            var model = _repositoryFaculty.GetById(id);
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<DataAccessLayer.Models.Faculty, FacultyDTO>());
            var mapper = new Mapper(configuration);
            var modelDTO = mapper.Map<FacultyDTO>(model);
            return modelDTO;
        }

        public List<FacultyDTO> GetList()
        {
            var models = _repositoryFaculty.GetAllIncludeForeignKey().ToList();
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DataAccessLayer.Models.Faculty, FacultyDTO>();
                cfg.CreateMap<Student, StudentDTO>();
                cfg.CreateMap<Curator, CuratorDTO>();
                cfg.CreateMap<Group, GroupDTO>();
                cfg.CreateMap<Specialization, SpecializationDTO>();
            });
            var mapper = new Mapper(configuration);
            var modelsDTO = mapper.Map<List<FacultyDTO>>(models);
            return modelsDTO;
        }

        public void Create(FacultyDTO modelDTO)
        {
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<FacultyDTO, DataAccessLayer.Models.Faculty>());
            var mapper = new Mapper(configuration);
            var model = mapper.Map<DataAccessLayer.Models.Faculty>(modelDTO);
            _repositoryFaculty.Insert(model);
        }

        public void Delete(int id)
        {
            var model = _repositoryFaculty.GetById(id);
            _repositoryFaculty.Delete(model);
        }

        public void Edit(FacultyDTO modelDTO)
        {
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<FacultyDTO, DataAccessLayer.Models.Faculty>());
            var mapper = new Mapper(configuration);
            var model = mapper.Map<DataAccessLayer.Models.Faculty>(modelDTO);
            _repositoryFaculty.Update(model);
        }

        public ViewModelFaculty CreateViewModelFaculty()
        {
            var modelsStudent = _repositoryStudent.GetAll().ToList();
            var configurationStudent = new MapperConfiguration(cfg => cfg.CreateMap<Student, StudentDTO>());
            var mapperStudent = new Mapper(configurationStudent);
            var modelsStudentDTO = mapperStudent.Map<List<StudentDTO>>(modelsStudent);
            var modelsCurator = _repositoryCurator.GetAll().ToList();
            var configurationCurator = new MapperConfiguration(cfg => cfg.CreateMap<Curator, CuratorDTO>());
            var mapperCurator = new Mapper(configurationCurator);
            var modelsCuratorDTO = mapperCurator.Map<List<CuratorDTO>>(modelsCurator);
            var modelsGroup = _repositoryGroup.GetAll().ToList();
            var configurationGroup = new MapperConfiguration(cfg => cfg.CreateMap<Group, GroupDTO>());
            var mapperGroup = new Mapper(configurationGroup);
            var modelsGroupDTO = mapperGroup.Map<List<GroupDTO>>(modelsGroup);
            return new ViewModelFaculty(modelsStudentDTO, modelsCuratorDTO, modelsGroupDTO);
        }
    }
}
