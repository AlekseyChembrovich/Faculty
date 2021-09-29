using AutoMapper;
using System.Collections.Generic;
using Faculty.BusinessLayer.Interfaces;
using Faculty.BusinessLayer.Dto.Faculty;
using Faculty.DataAccessLayer.Repository.EntityFramework.Interfaces;

namespace Faculty.BusinessLayer.Services
{
    public class FacultyService : IFacultyService
    {
        private readonly IRepositoryFaculty _repositoryFaculty;

        public FacultyService(IRepositoryFaculty repositoryFaculty)
        {
            _repositoryFaculty = repositoryFaculty;
        }

        public IEnumerable<FacultyDisplayDto> GetAll()
        {
            var models = _repositoryFaculty.GetAllIncludeForeignKey();
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            return mapper.Map<IEnumerable<DataAccessLayer.Models.Faculty>, IEnumerable<FacultyDisplayDto>>(models);
        }

        public void Create(FacultyAddDto modelDto)
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            _repositoryFaculty.Insert(mapper.Map<FacultyAddDto, DataAccessLayer.Models.Faculty>(modelDto));
        }

        public void Delete(int id)
        {
            var model = _repositoryFaculty.GetById(id);
            _repositoryFaculty.Delete(model);
        }

        public FacultyModifyDto GetById(int id)
        {
            var model = _repositoryFaculty.GetById(id);
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            return mapper.Map<DataAccessLayer.Models.Faculty, FacultyModifyDto>(model);
        }

        public void Edit(FacultyModifyDto modelDto)
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            _repositoryFaculty.Update(mapper.Map<FacultyModifyDto, DataAccessLayer.Models.Faculty>(modelDto));
        }
    }
}
