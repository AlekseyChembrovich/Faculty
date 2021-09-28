using AutoMapper;
using System.Linq;
using Faculty.DataAccessLayer;
using System.Collections.Generic;
using Faculty.DataAccessLayer.Models;
using Faculty.BusinessLayer.Interfaces;
using Faculty.BusinessLayer.ModelsBusiness;
using Faculty.BusinessLayer.ModelsDto.GroupDto;
using Faculty.BusinessLayer.ModelsDto.SpecializationDto;
using Faculty.DataAccessLayer.RepositoryEntityFramework;

namespace Faculty.BusinessLayer.Services
{
    public class GroupService : IGroupOperations
    {
        private readonly IRepositoryGroup _repositoryGroup;
        private readonly IRepository<Specialization> _repositorySpecialization;

        public GroupService(IRepositoryGroup repositoryGroup, IRepository<Specialization> repositorySpecialization)
        {
            _repositoryGroup = repositoryGroup;
            _repositorySpecialization = repositorySpecialization;
        }

        public IEnumerable<DisplayGroupDto> GetList()
        {
            var models = _repositoryGroup.GetAllIncludeForeignKey();
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            return mapper.Map<IEnumerable<Group>, IEnumerable<DisplayGroupDto>>(models);
        }

        public void Create(CreateGroupDto modelDto)
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            _repositoryGroup.Insert(mapper.Map<CreateGroupDto, Group>(modelDto));
        }

        public void Delete(int id)
        {
            var model = _repositoryGroup.GetById(id);
            _repositoryGroup.Delete(model);
        }

        public EditGroupDto GetModel(int id)
        {
            var model = _repositoryGroup.GetById(id);
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            return mapper.Map<Group, EditGroupDto>(model);
        }

        public void Edit(EditGroupDto modelDto)
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            _repositoryGroup.Update(mapper.Map<EditGroupDto, Group>(modelDto));
        }

        public ModelElementGroup CreateViewModelGroup()
        {
            var models = _repositorySpecialization.GetAll().ToList();
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            var modelsDto = mapper.Map<List<Specialization>, List<DisplaySpecializationDto>>(models);
            return new ModelElementGroup(modelsDto);
        }
    }
}
