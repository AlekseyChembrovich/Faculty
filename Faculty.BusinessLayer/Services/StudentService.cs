using AutoMapper;
using System.Linq;
using Faculty.DataAccessLayer;
using System.Collections.Generic;
using Faculty.DataAccessLayer.Models;
using Faculty.BusinessLayer.Interfaces;
using Faculty.BusinessLayer.ModelsDTO;

namespace Faculty.BusinessLayer.Services
{
    public class StudentService : IStudentService
    {
        private readonly IRepository<Student> _repositoryStudent;

        public StudentService(IRepository<Student> repositoryStudent)
        {
            _repositoryStudent = repositoryStudent;
        }

        public StudentDTO GetModel(int id)
        {
            var model = _repositoryStudent.GetById(id);
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<Student, StudentDTO>());
            var mapper = new Mapper(configuration);
            var modelDTO = mapper.Map<StudentDTO>(model);
            return modelDTO;
        }

        public List<StudentDTO> GetList()
        {
            var models = _repositoryStudent.GetAll().ToList();
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<Student, StudentDTO>());
            var mapper = new Mapper(configuration);
            var modelsDTO = mapper.Map<List<StudentDTO>>(models);
            return modelsDTO;
        }

        public void Create(StudentDTO modelDTO)
        {
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<StudentDTO, Student>());
            var mapper = new Mapper(configuration);
            var model = mapper.Map<Student>(modelDTO);
            _repositoryStudent.Insert(model);
        }

        public void Delete(int id)
        {
            var model = _repositoryStudent.GetById(id);
            _repositoryStudent.Delete(model);
        }

        public void Edit(StudentDTO modelDTO)
        {
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<StudentDTO, Student>());
            var mapper = new Mapper(configuration);
            var model = mapper.Map<Student>(modelDTO);
            _repositoryStudent.Update(model);
        }
    }
}
