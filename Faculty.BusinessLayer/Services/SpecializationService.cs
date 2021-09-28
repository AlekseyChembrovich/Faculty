using AutoMapper;
using System.Linq;
using Faculty.DataAccessLayer;
using System.Collections.Generic;
using Faculty.DataAccessLayer.Models;
using Faculty.BusinessLayer.Interfaces;
using Faculty.BusinessLayer.ModelsDto.SpecializationDto;

namespace Faculty.BusinessLayer.Services
{
    public class SpecializationService : ISpecializationOperations
    {
        private readonly IRepository<Specialization> _repositorySpecialization;

        public SpecializationService(IRepository<Specialization> repositorySpecialization)
        {
            _repositorySpecialization = repositorySpecialization;
        }

        public IEnumerable<DisplaySpecializationDto> GetList()
        {
            var models = _repositorySpecialization.GetAll().ToList();
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            return mapper.Map<IEnumerable<Specialization>, IEnumerable<DisplaySpecializationDto>>(models); ;
        }

        public void Create(CreateSpecializationDto modelDto)
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            _repositorySpecialization.Insert(mapper.Map<CreateSpecializationDto, Specialization>(modelDto));
        }

        public void Delete(int id)
        {
            var model = _repositorySpecialization.GetById(id);
            _repositorySpecialization.Delete(model);
        }

        public EditSpecializationDto GetModel(int id)
        {
            var model = _repositorySpecialization.GetById(id);
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            return mapper.Map<Specialization, EditSpecializationDto>(model);
        }

        public void Edit(EditSpecializationDto modelDto)
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            _repositorySpecialization.Update(mapper.Map<EditSpecializationDto, Specialization>(modelDto));
        }
    }
}
