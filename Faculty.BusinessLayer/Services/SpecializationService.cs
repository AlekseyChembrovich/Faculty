using AutoMapper;
using System.Linq;
using Faculty.DataAccessLayer;
using System.Collections.Generic;
using Faculty.DataAccessLayer.Models;
using Faculty.BusinessLayer.ModelsDto;
using Faculty.BusinessLayer.Interfaces;

namespace Faculty.BusinessLayer.Services
{
    public class SpecializationService : ISpecializationOperations
    {
        private readonly IRepository<Specialization> _repositorySpecialization;

        public SpecializationService(IRepository<Specialization> repositorySpecialization)
        {
            _repositorySpecialization = repositorySpecialization;
        }

        public SpecializationDto GetModel(int id)
        {
            var model = _repositorySpecialization.GetById(id);
            Mapper.Initialize(cfg => cfg.CreateMap<Specialization, SpecializationDto>());
            return Mapper.Map<Specialization, SpecializationDto>(model);
        }

        public List<SpecializationDto> GetList()
        {
            var models = _repositorySpecialization.GetAll().ToList();
            Mapper.Initialize(cfg => cfg.CreateMap<Specialization, SpecializationDto>());
            return Mapper.Map<List<Specialization>, List<SpecializationDto>>(models); ;
        }

        public void Create(SpecializationDto modelDto)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<SpecializationDto, Specialization>());
            _repositorySpecialization.Insert(Mapper.Map<SpecializationDto, Specialization>(modelDto));
        }

        public void Delete(int id)
        {
            var model = _repositorySpecialization.GetById(id);
            _repositorySpecialization.Delete(model);
        }

        public void Edit(SpecializationDto modelDto)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<SpecializationDto, Specialization>());
            _repositorySpecialization.Update(Mapper.Map<SpecializationDto, Specialization>(modelDto));
        }
    }
}
