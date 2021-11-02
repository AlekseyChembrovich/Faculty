using AutoMapper;
using System.Collections.Generic;
using Faculty.BusinessLayer.Interfaces;
using Faculty.BusinessLayer.Dto.Faculty;
using Faculty.DataAccessLayer.Repository.EntityFramework.Interfaces;

namespace Faculty.BusinessLayer.Services
{
    /// <summary>
    /// Faculty service.
    /// </summary>
    public class FacultyService : IFacultyService
    {
        /// <summary>
        /// Model repository.
        /// </summary>
        private readonly IRepositoryFaculty _repositoryFaculty;

        /// <summary>
        /// Auto mapper.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for init repository.
        /// </summary>
        /// <param name="repositoryFaculty">Model repository.</param>
        /// <param name="mapper">Mapper.</param>
        public FacultyService(IRepositoryFaculty repositoryFaculty, IMapper mapper)
        {
            _repositoryFaculty = repositoryFaculty;
            _mapper = mapper;
        }

        /// <summary>
        /// Method for receive set of entity.
        /// </summary>
        /// <returns>Dto set.</returns>
        public IEnumerable<FacultyDisplayDto> GetAll()
        {
            var models = _repositoryFaculty.GetAllIncludeForeignKey();
            return _mapper.Map<IEnumerable<DataAccessLayer.Models.Faculty>, IEnumerable<FacultyDisplayDto>>(models);
        }

        /// <summary>
        /// Method for creating a new entity.
        /// </summary>
        /// <param name="dto">Add Dto.</param>
        public FacultyDto Create(FacultyDto dto)
        {
            var faculty = _repositoryFaculty.Insert(_mapper.Map<FacultyDto, DataAccessLayer.Models.Faculty>(dto));
            var facultyDto = _mapper.Map<DataAccessLayer.Models.Faculty, FacultyDto>(faculty);
            return facultyDto;
        }

        /// <summary>
        /// Method for receive dto set.
        /// </summary>
        /// <param name="id">Id exist entity.</param>
        /// <returns>Modify Dto.</returns>
        public void Delete(int id)
        {
            var model = _repositoryFaculty.GetById(id);
            _repositoryFaculty.Delete(model);
        }

        /// <summary>
        /// Method for receive dto.
        /// </summary>
        /// <param name="id">Id exist entity.</param>
        /// <returns>Modify Dto.</returns>
        public FacultyDto GetById(int id)
        {
            var model = _repositoryFaculty.GetById(id);
            return _mapper.Map<DataAccessLayer.Models.Faculty, FacultyDto>(model);
        }

        /// <summary>
        /// Method for changing a exist entity.
        /// </summary>
        /// <param name="dto">Modify Dto.</param>
        public void Edit(FacultyDto dto)
        {
            _repositoryFaculty.Update(_mapper.Map<FacultyDto, DataAccessLayer.Models.Faculty>(dto));
        }
    }
}
