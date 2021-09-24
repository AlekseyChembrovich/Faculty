using AutoMapper;
using System.Linq;
using Faculty.DataAccessLayer;
using System.Collections.Generic;
using Faculty.DataAccessLayer.Models;
using Faculty.BusinessLayer.Interfaces;
using Faculty.BusinessLayer.ModelsBusiness;
using Faculty.BusinessLayer.ModelsDTO;
using Faculty.DataAccessLayer.RepositoryEntityFramework;

namespace Faculty.BusinessLayer.Services
{
    public class GroupService : IGroupService
    {
        private readonly IRepositoryGroup _repositoryGroup;
        private readonly IRepository<Specialization> _repositorySpecialization;

        public GroupService(IRepositoryGroup repositoryGroup, IRepository<Specialization> repositorySpecialization)
        {
            _repositoryGroup = repositoryGroup;
            _repositorySpecialization = repositorySpecialization;
        }

        public GroupDTO GetModel(int id)
        {
            var model = _repositoryGroup.GetById(id);
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<Group, GroupDTO>());
            var mapper = new Mapper(configuration);
            var modelDTO = mapper.Map<GroupDTO>(model);
            return modelDTO;
        }

        public List<GroupDTO> GetList()
        {
            var models = _repositoryGroup.GetAllIncludeForeignKey().ToList();
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Group, GroupDTO>();
                cfg.CreateMap<Specialization, SpecializationDTO>();
            });
            var mapper = new Mapper(configuration);
            var modelsDTO = mapper.Map<List<GroupDTO>>(models);
            return modelsDTO;
        }

        public void Create(GroupDTO modelDTO)
        {
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<GroupDTO, Group>());
            var mapper = new Mapper(configuration);
            var model = mapper.Map<Group>(modelDTO);
            _repositoryGroup.Insert(model);
        }

        public void Delete(int id)
        {
            var model = _repositoryGroup.GetById(id);
            _repositoryGroup.Delete(model);
        }

        public void Edit(GroupDTO modelDTO)
        {
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<GroupDTO, Group>());
            var mapper = new Mapper(configuration);
            var model = mapper.Map<Group>(modelDTO);
            _repositoryGroup.Update(model);
        }

        public ViewModelGroup CreateViewModelGroup()
        {
            var models = _repositorySpecialization.GetAll().ToList();
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<Specialization, SpecializationDTO>());
            var mapper = new Mapper(configuration);
            var modelsDTO = mapper.Map<List<SpecializationDTO>>(models);
            return new ViewModelGroup(modelsDTO);
        }
    }
}
