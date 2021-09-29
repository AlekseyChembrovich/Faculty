using AutoMapper;
using System.Linq;
using System.Collections.Generic;
using Faculty.DataAccessLayer.Models;
using Faculty.BusinessLayer.Interfaces;
using Faculty.DataAccessLayer.Repository;
using Faculty.BusinessLayer.Dto.Specialization;

namespace Faculty.BusinessLayer.Services
{
    public class SpecializationService : ISpecializationService
    {
        private readonly IRepository<Specialization> _repositorySpecialization;

        public SpecializationService(IRepository<Specialization> repositorySpecialization)
        {
            _repositorySpecialization = repositorySpecialization;
        }

        public IEnumerable<SpecializationDisplayModifyDto> GetAll()
        {
            var models = _repositorySpecialization.GetAll().ToList();
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            return mapper.Map<IEnumerable<Specialization>, IEnumerable<SpecializationDisplayModifyDto>>(models); ;
        }

        public void Create(SpecializationAddDto modelDto)
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            _repositorySpecialization.Insert(mapper.Map<SpecializationAddDto, Specialization>(modelDto));
        }

        public void Delete(int id)
        {
            var model = _repositorySpecialization.GetById(id);
            _repositorySpecialization.Delete(model);
        }

        public SpecializationDisplayModifyDto GetById(int id)
        {
            var model = _repositorySpecialization.GetById(id);
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            return mapper.Map<Specialization, SpecializationDisplayModifyDto>(model);
        }

        public void Edit(SpecializationDisplayModifyDto modelDto)
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            _repositorySpecialization.Update(mapper.Map<SpecializationDisplayModifyDto, Specialization>(modelDto));
        }
    }
}
