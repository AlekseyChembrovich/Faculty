using AutoMapper;
using Faculty.DataAccessLayer;
using System.Collections.Generic;
using Faculty.DataAccessLayer.Models;
using Faculty.BusinessLayer.Interfaces;
using Faculty.BusinessLayer.ModelsDto.CuratorDto;

namespace Faculty.BusinessLayer.Services
{
    public class CuratorService : ICuratorOperations
    {
        private readonly IRepository<Curator> _repositoryCurator;

        public CuratorService(IRepository<Curator> repositoryCurator)
        {
            _repositoryCurator = repositoryCurator;
        }

        public IEnumerable<DisplayCuratorDto> GetList()
        {
            var models = _repositoryCurator.GetAll();
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            return mapper.Map<IEnumerable<Curator>, IEnumerable<DisplayCuratorDto>>(models);
        }

        public void Create(CreateCuratorDto modelDto)
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            _repositoryCurator.Insert(mapper.Map<CreateCuratorDto, Curator>(modelDto));
        }

        public void Delete(int id)
        {
            var model = _repositoryCurator.GetById(id);
            _repositoryCurator.Delete(model);
        }

        public EditCuratorDto GetModel(int id)
        {
            var model = _repositoryCurator.GetById(id);
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            return mapper.Map<Curator, EditCuratorDto>(model);
        }

        public void Edit(EditCuratorDto modelDto)
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            _repositoryCurator.Update(mapper.Map<EditCuratorDto, Curator>(modelDto));
        }
    }
}
