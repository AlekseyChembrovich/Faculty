using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using Faculty.DataAccessLayer.Models;
using Faculty.BusinessLayer.Interfaces;
using Faculty.BusinessLayer.Dto.Curator;
using Faculty.DataAccessLayer.Repository;

namespace Faculty.BusinessLayer.Services
{
    /// <summary>
    /// Curator service.
    /// </summary>
    public class CuratorService : ICuratorService
    {
        /// <summary>
        /// Model repository.
        /// </summary>
        private readonly IRepository<Curator> _repositoryCurator;

        /// <summary>
        /// Auto mapper.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for init repository.
        /// </summary>
        /// <param name="repositoryCurator">Model repository.</param>
        /// <param name="mapper">Mapper.</param>
        public CuratorService(IRepository<Curator> repositoryCurator, IMapper mapper)
        {
            _repositoryCurator = repositoryCurator;
            _mapper = mapper;
        }

        /// <summary>
        /// Method for receive set of entity.
        /// </summary>
        /// <returns>Dto set.</returns>
        public IEnumerable<CuratorDisplayModifyDto> GetAll()
        {
            var models = _repositoryCurator.GetAll();
            models = models.ToList();
            return _mapper.Map<IEnumerable<Curator>, IEnumerable<CuratorDisplayModifyDto>>(models);
        }

        /// <summary>
        /// Method for creating a new entity.
        /// </summary>
        /// <param name="dto">Add Dto.</param>
        public void Create(CuratorAddDto dto)
        {
            _repositoryCurator.Insert(_mapper.Map<CuratorAddDto, Curator>(dto));
        }

        /// <summary>
        /// Method for deleting a exist entity.
        /// </summary>
        /// <param name="id">Id exist entity.</param>
        public void Delete(int id)
        {
            var model = _repositoryCurator.GetById(id);
            _repositoryCurator.Delete(model);
        }

        /// <summary>
        /// Method for receive dto.
        /// </summary>
        /// <param name="id">Id exist entity.</param>
        /// <returns>Modify Dto.</returns>
        public CuratorDisplayModifyDto GetById(int id)
        {
            var model = _repositoryCurator.GetById(id);
            return _mapper.Map<Curator, CuratorDisplayModifyDto>(model);
        }

        /// <summary>
        /// Method for changing a exist entity.
        /// </summary>
        /// <param name="dto">Modify Dto.</param>
        public void Edit(CuratorDisplayModifyDto dto)
        {
            _repositoryCurator.Update(_mapper.Map<CuratorDisplayModifyDto, Curator>(dto));
        }
    }
}
