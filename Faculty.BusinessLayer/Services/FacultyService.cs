using AutoMapper;
using System.Linq;
using Faculty.DataAccessLayer;
using System.Collections.Generic;
using Faculty.DataAccessLayer.Models;
using Faculty.BusinessLayer.Interfaces;
using Faculty.BusinessLayer.ModelsBusiness;
using Faculty.BusinessLayer.ModelsDto.GroupDto;
using Faculty.BusinessLayer.ModelsDto.StudentDto;
using Faculty.BusinessLayer.ModelsDto.CuratorDto;
using Faculty.BusinessLayer.ModelsDto.FacultyDto;
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

        public IEnumerable<DisplayFacultyDto> GetList()
        {
            var models = _repositoryFaculty.GetAllIncludeForeignKey().ToList();
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            return mapper.Map<IEnumerable<DataAccessLayer.Models.Faculty>, IEnumerable<DisplayFacultyDto>>(models);
        }

        public void Create(CreateFacultyDto modelDto)
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            _repositoryFaculty.Insert(mapper.Map<CreateFacultyDto, DataAccessLayer.Models.Faculty>(modelDto));
        }

        public void Delete(int id)
        {
            var model = _repositoryFaculty.GetById(id);
            _repositoryFaculty.Delete(model);
        }

        public EditFacultyDto GetModel(int id)
        {
            var model = _repositoryFaculty.GetById(id);
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            return mapper.Map<DataAccessLayer.Models.Faculty, EditFacultyDto>(model);
        }

        public void Edit(EditFacultyDto modelDto)
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            _repositoryFaculty.Update(mapper.Map<EditFacultyDto, DataAccessLayer.Models.Faculty>(modelDto));
        }

        public ModelElementFaculty CreateViewModelFaculty()
        {
            var modelsStudent = _repositoryStudent.GetAll().ToList();
            var mapperConfigurationStudent = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapperStudent = new Mapper(mapperConfigurationStudent);
            var modelsStudentDto = mapperStudent.Map<List<Student>, List<DisplayStudentDto>>(modelsStudent);
            var modelsCurator = _repositoryCurator.GetAll().ToList();
            var mapperConfigurationCurator = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapperCurator = new Mapper(mapperConfigurationCurator);
            var modelsCuratorDto = mapperCurator.Map<List<Curator>, List<DisplayCuratorDto>>(modelsCurator);
            var modelsGroup = _repositoryGroup.GetAll().ToList();
            var mapperConfigurationGroup = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapperGroup = new Mapper(mapperConfigurationGroup);
            var modelsGroupDto = mapperGroup.Map<List<Group>, List<DisplayGroupDto>>(modelsGroup);
            return new ModelElementFaculty(modelsStudentDto, modelsCuratorDto, modelsGroupDto);
        }
    }
}
