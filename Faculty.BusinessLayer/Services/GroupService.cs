using AutoMapper;
using System.Collections.Generic;
using Faculty.DataAccessLayer.Models;
using Faculty.BusinessLayer.Dto.Group;
using Faculty.BusinessLayer.Interfaces;
using Faculty.DataAccessLayer.RepositoryEntityFramework;

namespace Faculty.BusinessLayer.Services
{
    public class GroupService : IGroupService
    {
        private readonly IRepositoryGroup _repositoryGroup;

        public GroupService(IRepositoryGroup repositoryGroup)
        {
            _repositoryGroup = repositoryGroup;
        }

        public IEnumerable<GroupDisplayDto> GetAll()
        {
            var models = _repositoryGroup.GetAllIncludeForeignKey();
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            return mapper.Map<IEnumerable<Group>, IEnumerable<GroupDisplayDto>>(models);
        }

        public void Create(GroupAddDto modelDto)
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            _repositoryGroup.Insert(mapper.Map<GroupAddDto, Group>(modelDto));
        }

        public void Delete(int id)
        {
            var model = _repositoryGroup.GetById(id);
            _repositoryGroup.Delete(model);
        }

        public GroupModifyDto GetById(int id)
        {
            var model = _repositoryGroup.GetById(id);
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            return mapper.Map<Group, GroupModifyDto>(model);
        }

        public void Edit(GroupModifyDto modelDto)
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            _repositoryGroup.Update(mapper.Map<GroupModifyDto, Group>(modelDto));
        }
    }
}
