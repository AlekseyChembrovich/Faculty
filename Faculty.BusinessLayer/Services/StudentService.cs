using AutoMapper;
using System.Linq;
using System.Collections.Generic;
using Faculty.DataAccessLayer.Models;
using Faculty.BusinessLayer.Interfaces;
using Faculty.BusinessLayer.Dto.Student;
using Faculty.DataAccessLayer.Repository;

namespace Faculty.BusinessLayer.Services
{
    /// <summary>
    /// Student service.
    /// </summary>
    public class StudentService : IStudentService
    {
        /// <summary>
        /// Model repository.
        /// </summary>
        private readonly IRepository<Student> _repositoryStudent;

        /// <summary>
        /// Constructor for init repository.
        /// </summary>
        /// <param name="repositoryStudent">Model repository.</param>
        public StudentService(IRepository<Student> repositoryStudent)
        {
            _repositoryStudent = repositoryStudent;
        }

        /// <summary>
        /// Method for receive set of entity.
        /// </summary>
        /// <returns>Dto set.</returns>
        public IEnumerable<StudentDisplayModifyDto> GetAll()
        {
            var models = _repositoryStudent.GetAll().ToList();
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            return mapper.Map<IEnumerable<Student>, IEnumerable<StudentDisplayModifyDto>>(models);
        }

        /// <summary>
        /// Method for creating a new entity.
        /// </summary>
        /// <param name="dto">Add Dto.</param>
        public void Create(StudentAddDto dto)
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            _repositoryStudent.Insert(mapper.Map<StudentAddDto, Student>(dto));
        }

        /// <summary>
        /// Method for receive dto set.
        /// </summary>
        /// <param name="id">Id exist entity.</param>
        /// <returns>Modify Dto.</returns>
        public void Delete(int id)
        {
            var model = _repositoryStudent.GetById(id);
            _repositoryStudent.Delete(model);
        }

        /// <summary>
        /// Method for receive dto.
        /// </summary>
        /// <param name="id">Id exist entity.</param>
        /// <returns>Modify Dto.</returns>
        public StudentDisplayModifyDto GetById(int id)
        {
            var model = _repositoryStudent.GetById(id);
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            return mapper.Map<Student, StudentDisplayModifyDto>(model);
        }

        /// <summary>
        /// Method for changing a exist entity.
        /// </summary>
        /// <param name="dto">Modify Dto.</param>
        public void Edit(StudentDisplayModifyDto dto)
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            _repositoryStudent.Update(mapper.Map<StudentDisplayModifyDto, Student>(dto));
        }
    }
}
