using AutoMapper;
using System.Linq;
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

        public List<DisplayCuratorDto> GetList()
        {
            var models = _repositoryCurator.GetAll().ToList();
            Mapper.Initialize(cfg => cfg.CreateMap<Curator, DisplayCuratorDto>());
            return Mapper.Map<List<Curator>, List<DisplayCuratorDto>>(models);
        }

        public void Create(CreateCuratorDto modelDto)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<CreateCuratorDto, Curator>());
            _repositoryCurator.Insert(Mapper.Map<CreateCuratorDto, Curator>(modelDto));
        }

        public void Delete(int id)
        {
            var model = _repositoryCurator.GetById(id);
            _repositoryCurator.Delete(model);
        }

        public EditCuratorDto GetModel(int id)
        {
            var model = _repositoryCurator.GetById(id);
            Mapper.Initialize(cfg => cfg.CreateMap<Curator, EditCuratorDto>());
            return Mapper.Map<Curator, EditCuratorDto>(model);
        }

        public void Edit(EditCuratorDto modelDto)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<EditCuratorDto, Curator>());
            _repositoryCurator.Update(Mapper.Map<EditCuratorDto, Curator>(modelDto));
        }
    }
}
