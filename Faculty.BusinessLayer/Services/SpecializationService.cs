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

        public List<DisplaySpecializationDto> GetList()
        {
            var models = _repositorySpecialization.GetAll().ToList();
            Mapper.Initialize(cfg => cfg.CreateMap<Specialization, DisplaySpecializationDto>());
            return Mapper.Map<List<Specialization>, List<DisplaySpecializationDto>>(models); ;
        }

        public void Create(CreateSpecializationDto modelDto)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<CreateSpecializationDto, Specialization>());
            _repositorySpecialization.Insert(Mapper.Map<CreateSpecializationDto, Specialization>(modelDto));
        }

        public void Delete(int id)
        {
            var model = _repositorySpecialization.GetById(id);
            _repositorySpecialization.Delete(model);
        }

        public EditSpecializationDto GetModel(int id)
        {
            var model = _repositorySpecialization.GetById(id);
            Mapper.Initialize(cfg => cfg.CreateMap<Specialization, EditSpecializationDto>());
            return Mapper.Map<Specialization, EditSpecializationDto>(model);
        }

        public void Edit(EditSpecializationDto modelDto)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<EditSpecializationDto, Specialization>());
            _repositorySpecialization.Update(Mapper.Map<EditSpecializationDto, Specialization>(modelDto));
        }
    }
}
