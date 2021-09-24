using AutoMapper;
using System.Linq;
using Faculty.DataAccessLayer;
using System.Collections.Generic;
using Faculty.DataAccessLayer.Models;
using Faculty.BusinessLayer.Interfaces;
using Faculty.BusinessLayer.ModelsDTO;

namespace Faculty.BusinessLayer.Services
{
    public class SpecializationService : ISpecializationService
    {
        private readonly IRepository<Specialization> _repositorySpecialization;

        public SpecializationService(IRepository<Specialization> repositorySpecialization)
        {
            _repositorySpecialization = repositorySpecialization;
        }

        public SpecializationDTO GetModel(int id)
        {
            var model = _repositorySpecialization.GetById(id);
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<Specialization, SpecializationDTO>());
            var mapper = new Mapper(configuration);
            var modelDTO = mapper.Map<SpecializationDTO>(model);
            return modelDTO;
        }

        public List<SpecializationDTO> GetList()
        {
            var models = _repositorySpecialization.GetAll().ToList();
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<Specialization, SpecializationDTO>());
            var mapper = new Mapper(configuration);
            var modelsDTO = mapper.Map<List<SpecializationDTO>>(models);
            return modelsDTO;
        }

        public void Create(SpecializationDTO modelDTO)
        {
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<SpecializationDTO, Specialization>());
            var mapper = new Mapper(configuration);
            var model = mapper.Map<Specialization>(modelDTO);
            _repositorySpecialization.Insert(model);
        }

        public void Delete(int id)
        {
            var model = _repositorySpecialization.GetById(id);
            _repositorySpecialization.Delete(model);
        }

        public void Edit(SpecializationDTO modelDTO)
        {
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<SpecializationDTO, Specialization>());
            var mapper = new Mapper(configuration);
            var model = mapper.Map<Specialization>(modelDTO);
            _repositorySpecialization.Update(model);
        }
    }
}
