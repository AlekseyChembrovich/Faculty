using AutoMapper;
using System.Linq;
using Faculty.DataAccessLayer;
using System.Collections.Generic;
using Faculty.DataAccessLayer.Models;
using Faculty.BusinessLayer.ModelsDto;
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

        public List<DisplayGroupDto> GetList()
        {
            var models = _repositoryGroup.GetAllIncludeForeignKey().ToList();
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Group, DisplayGroupDto>()
                    .ForMember("SpecializationName", opt => opt.MapFrom(src => src.Specialization.Name));
            });
            return Mapper.Map<List<Group>, List<DisplayGroupDto>>(models);
        }

        public void Create(CreateGroupDto modelDto)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<CreateGroupDto, Group>());
            _repositoryGroup.Insert(Mapper.Map<CreateGroupDto, Group>(modelDto));
        }

        public void Delete(int id)
        {
            var model = _repositoryGroup.GetById(id);
            _repositoryGroup.Delete(model);
        }

        public EditGroupDto GetModel(int id)
        {
            var model = _repositoryGroup.GetById(id);
            Mapper.Initialize(cfg => cfg.CreateMap<Group, EditGroupDto>());
            return Mapper.Map<Group, EditGroupDto>(model);
        }

        public void Edit(EditGroupDto modelDto)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<EditGroupDto, Group>());
            _repositoryGroup.Update(Mapper.Map<EditGroupDto, Group>(modelDto));
        }

        public ModelElementGroup CreateViewModelGroup()
        {
            var models = _repositorySpecialization.GetAll().ToList();
            Mapper.Initialize(cfg => cfg.CreateMap<Specialization, DisplaySpecializationDto>());
            var modelsDto = Mapper.Map<List<Specialization>, List<DisplaySpecializationDto>>(models);
            return new ModelElementGroup(modelsDto);
        }
    }
}
