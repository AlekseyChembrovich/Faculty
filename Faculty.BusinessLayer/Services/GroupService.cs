using AutoMapper;
using System.Collections.Generic;
using Faculty.DataAccessLayer.Models;
using Faculty.BusinessLayer.Dto.Group;
using Faculty.BusinessLayer.Interfaces;
using Faculty.DataAccessLayer.Repository.EntityFramework.Interfaces;

namespace Faculty.BusinessLayer.Services
{
    /// <summary>
    /// Group service.
    /// </summary>
    public class GroupService : IGroupService
    {
        /// <summary>
        /// Model repository.
        /// </summary>
        private readonly IRepositoryGroup _repositoryGroup;

        /// <summary>
        /// Constructor for init repository.
        /// </summary>
        /// <param name="repositoryGroup">Model repository.</param>
        public GroupService(IRepositoryGroup repositoryGroup)
        {
            _repositoryGroup = repositoryGroup;
        }

        /// <summary>
        /// Method for receive set of entity.
        /// </summary>
        /// <returns>Dto set.</returns>
        public IEnumerable<GroupDisplayDto> GetAll()
        {
            var models = _repositoryGroup.GetAllIncludeForeignKey();
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            return mapper.Map<IEnumerable<Group>, IEnumerable<GroupDisplayDto>>(models);
        }

        /// <summary>
        /// Method for creating a new entity.
        /// </summary>
        /// <param name="dto">Add Dto.</param>
        public void Create(GroupAddDto dto)
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            _repositoryGroup.Insert(mapper.Map<GroupAddDto, Group>(dto));
        }

        /// <summary>
        /// Method for receive dto set.
        /// </summary>
        /// <param name="id">Id exist entity.</param>
        /// <returns>Modify Dto.</returns>
        public void Delete(int id)
        {
            var model = _repositoryGroup.GetById(id);
            _repositoryGroup.Delete(model);
        }

        /// <summary>
        /// Method for receive dto.
        /// </summary>
        /// <param name="id">Id exist entity.</param>
        /// <returns>Modify Dto.</returns>
        public GroupModifyDto GetById(int id)
        {
            var model = _repositoryGroup.GetById(id);
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            return mapper.Map<Group, GroupModifyDto>(model);
        }

        /// <summary>
        /// Method for changing a exist entity.
        /// </summary>
        /// <param name="dto">Modify Dto.</param>
        public void Edit(GroupModifyDto dto)
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            _repositoryGroup.Update(mapper.Map<GroupModifyDto, Group>(dto));
        }
    }
}
