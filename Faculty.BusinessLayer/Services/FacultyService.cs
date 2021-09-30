using AutoMapper;
using System.Collections.Generic;
using Faculty.BusinessLayer.Interfaces;
using Faculty.BusinessLayer.Dto.Faculty;
using Faculty.DataAccessLayer.Repository.EntityFramework.Interfaces;

namespace Faculty.BusinessLayer.Services
{
    /// <summary>
    /// Faculty service.
    /// </summary>
    public class FacultyService : IFacultyService
    {
        /// <summary>
        /// Model repository.
        /// </summary>
        private readonly IRepositoryFaculty _repositoryFaculty;

        /// <summary>
        /// Constructor for init repository.
        /// </summary>
        /// <param name="repositoryFaculty">Model repository.</param>
        public FacultyService(IRepositoryFaculty repositoryFaculty)
        {
            _repositoryFaculty = repositoryFaculty;
        }

        /// <summary>
        /// Method for receive set of entity.
        /// </summary>
        /// <returns>Dto set.</returns>
        public IEnumerable<FacultyDisplayDto> GetAll()
        {
            var models = _repositoryFaculty.GetAllIncludeForeignKey();
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            return mapper.Map<IEnumerable<DataAccessLayer.Models.Faculty>, IEnumerable<FacultyDisplayDto>>(models);
        }

        /// <summary>
        /// Method for creating a new entity.
        /// </summary>
        /// <param name="dto">Add Dto.</param>
        public void Create(FacultyAddDto dto)
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            _repositoryFaculty.Insert(mapper.Map<FacultyAddDto, DataAccessLayer.Models.Faculty>(dto));
        }

        /// <summary>
        /// Method for receive dto set.
        /// </summary>
        /// <param name="id">Id exist entity.</param>
        /// <returns>Modify Dto.</returns>
        public void Delete(int id)
        {
            var model = _repositoryFaculty.GetById(id);
            _repositoryFaculty.Delete(model);
        }

        /// <summary>
        /// Method for receive dto.
        /// </summary>
        /// <param name="id">Id exist entity.</param>
        /// <returns>Modify Dto.</returns>
        public FacultyModifyDto GetById(int id)
        {
            var model = _repositoryFaculty.GetById(id);
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            return mapper.Map<DataAccessLayer.Models.Faculty, FacultyModifyDto>(model);
        }

        /// <summary>
        /// Method for changing a exist entity.
        /// </summary>
        /// <param name="dto">Modify Dto.</param>
        public void Edit(FacultyModifyDto dto)
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            _repositoryFaculty.Update(mapper.Map<FacultyModifyDto, DataAccessLayer.Models.Faculty>(dto));
        }
    }
}
