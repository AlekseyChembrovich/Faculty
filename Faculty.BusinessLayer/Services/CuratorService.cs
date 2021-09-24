using AutoMapper;
using System.Linq;
using Faculty.DataAccessLayer;
using System.Collections.Generic;
using Faculty.DataAccessLayer.Models;
using Faculty.BusinessLayer.Interfaces;
using Faculty.BusinessLayer.ModelsDTO;

namespace Faculty.BusinessLayer.Services
{
    public class CuratorService : ICuratorService
    {
        private readonly IRepository<Curator> _repositoryCurator;

        public CuratorService(IRepository<Curator> repositoryCurator)
        {
            _repositoryCurator = repositoryCurator;
        }

        public CuratorDTO GetModel(int id)
        {
            var model = _repositoryCurator.GetById(id);
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<Curator, CuratorDTO>());
            var mapper = new Mapper(configuration);
            var modelDTO = mapper.Map<CuratorDTO>(model);
            return modelDTO;
        }

        public List<CuratorDTO> GetList()
        {
            var models = _repositoryCurator.GetAll().ToList();
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<Curator, CuratorDTO>());
            var mapper = new Mapper(configuration);
            var modelsDTO = mapper.Map<List<CuratorDTO>>(models);
            return modelsDTO;
        }

        public void Create(CuratorDTO modelDTO)
        {
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<CuratorDTO, Curator>());
            var mapper = new Mapper(configuration);
            var model = mapper.Map<Curator>(modelDTO);
            _repositoryCurator.Insert(model);
        }

        public void Delete(int id)
        {
            var model = _repositoryCurator.GetById(id);
            _repositoryCurator.Delete(model);
        }

        public void Edit(CuratorDTO modelDTO)
        {
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<CuratorDTO, Curator>());
            var mapper = new Mapper(configuration);
            var model = mapper.Map<Curator>(modelDTO);
            _repositoryCurator.Update(model);
        }
    }
}
