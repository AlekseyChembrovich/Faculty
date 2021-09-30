using AutoMapper;
using System.Collections.Generic;
using Faculty.DataAccessLayer.Models;
using Faculty.BusinessLayer.Interfaces;
using Faculty.BusinessLayer.Dto.Curator;
using Faculty.DataAccessLayer.Repository;

namespace Faculty.BusinessLayer.Services
{
    /// <summary>
    /// Curator service.
    /// </summary>
    public class CuratorService : ICuratorService
    {
        /// <summary>
        /// Model repository.
        /// </summary>
        private readonly IRepository<Curator> _repositoryCurator;

        /// <summary>
        /// Constructor for init repository.
        /// </summary>
        /// <param name="repositoryCurator">Model repository.</param>
        public CuratorService(IRepository<Curator> repositoryCurator)
        {
            _repositoryCurator = repositoryCurator;
        }

        /// <summary>
        /// Method for receive set of entity.
        /// </summary>
        /// <returns>Dto set.</returns>
        public IEnumerable<CuratorDisplayModifyDto> GetAll()
        {
            var models = _repositoryCurator.GetAll();
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            return mapper.Map<IEnumerable<Curator>, IEnumerable<CuratorDisplayModifyDto>>(models);
        }

        /// <summary>
        /// Method for creating a new entity.
        /// </summary>
        /// <param name="dto">Add Dto.</param>
        public void Create(CuratorAddDto dto)
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            _repositoryCurator.Insert(mapper.Map<CuratorAddDto, Curator>(dto));
        }

        /// <summary>
        /// Method for deleting a exist entity.
        /// </summary>
        /// <param name="id">Id exist entity.</param>
        public void Delete(int id)
        {
            var model = _repositoryCurator.GetById(id);
            _repositoryCurator.Delete(model);
        }

        /// <summary>
        /// Method for receive dto.
        /// </summary>
        /// <param name="id">Id exist entity.</param>
        /// <returns>Modify Dto.</returns>
        public CuratorDisplayModifyDto GetById(int id)
        {
            var model = _repositoryCurator.GetById(id);
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            return mapper.Map<Curator, CuratorDisplayModifyDto>(model);
        }

        /// <summary>
        /// Method for changing a exist entity.
        /// </summary>
        /// <param name="dto">Modify Dto.</param>
        public void Edit(CuratorDisplayModifyDto dto)
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            _repositoryCurator.Update(mapper.Map<CuratorDisplayModifyDto, Curator>(dto));
        }
    }
}
