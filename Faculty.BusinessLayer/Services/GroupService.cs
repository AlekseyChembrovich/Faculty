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
    public class GroupService : IGroupOperations
    {
        private readonly IRepositoryGroup _repositoryGroup;
        private readonly IRepository<Specialization> _repositorySpecialization;

        public GroupService(IRepositoryGroup repositoryGroup, IRepository<Specialization> repositorySpecialization)
        {
            _repositoryGroup = repositoryGroup;
            _repositorySpecialization = repositorySpecialization;
        }

        public GroupDto GetModel(int id)
        {
            var model = _repositoryGroup.GetById(id);
            Mapper.Initialize(cfg => cfg.CreateMap<Group, GroupDto>());
            return Mapper.Map<Group, GroupDto>(model);
        }

        public List<GroupDto> GetList()
        {
            var models = _repositoryGroup.GetAllIncludeForeignKey().ToList();
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Group, GroupDto>()
                    .ForMember("SpecializationName", opt => opt.MapFrom(src => src.Specialization.Name));
            });
            return Mapper.Map<List<Group>, List<GroupDto>>(models);
        }

        public void Create(GroupDto modelDto)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<GroupDto, Group>());
            _repositoryGroup.Insert(Mapper.Map<GroupDto, Group>(modelDto));
        }

        public void Delete(int id)
        {
            var model = _repositoryGroup.GetById(id);
            _repositoryGroup.Delete(model);
        }

        public void Edit(GroupDto modelDto)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<GroupDto, Group>());
            _repositoryGroup.Update(Mapper.Map<GroupDto, Group>(modelDto));
        }

        public ModelElementGroup CreateViewModelGroup()
        {
            var models = _repositorySpecialization.GetAll().ToList();

            Mapper.Initialize(cfg => cfg.CreateMap<Specialization, SpecializationDto>());
            var modelsDto = Mapper.Map<List<Specialization>, List<SpecializationDto>>(models);
            return new ModelElementGroup(modelsDto);
        }
    }
}
