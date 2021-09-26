using AutoMapper;
using System.Linq;
using Faculty.DataAccessLayer;
using System.Collections.Generic;
using Faculty.DataAccessLayer.Models;
using Faculty.BusinessLayer.ModelsDto;
using Faculty.BusinessLayer.Interfaces;

namespace Faculty.BusinessLayer.Services
{
    public class CuratorService : ICuratorOperations
    {
        private readonly IRepository<Curator> _repositoryCurator;

        public CuratorService(IRepository<Curator> repositoryCurator)
        {
            _repositoryCurator = repositoryCurator;
        }

        public CuratorDto GetModel(int id)
        {
            var model = _repositoryCurator.GetById(id);
            Mapper.Initialize(cfg => cfg.CreateMap<Curator, CuratorDto>());
            return Mapper.Map<Curator, CuratorDto>(model);
        }

        public List<CuratorDto> GetList()
        {
            var models = _repositoryCurator.GetAll().ToList();
            Mapper.Initialize(cfg => cfg.CreateMap<Curator, CuratorDto>());
            return Mapper.Map<List<Curator>, List<CuratorDto>>(models); ;
        }

        public void Create(CuratorDto modelDto)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<CuratorDto, Curator>());
            _repositoryCurator.Insert(Mapper.Map<CuratorDto, Curator>(modelDto));
        }

        public void Delete(int id)
        {
            var model = _repositoryCurator.GetById(id);
            _repositoryCurator.Delete(model);
        }

        public void Edit(CuratorDto modelDto)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<CuratorDto, Curator>());
            _repositoryCurator.Update(Mapper.Map<CuratorDto, Curator>(modelDto));
        }
    }
}
