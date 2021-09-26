using AutoMapper;
using System.Linq;
using Faculty.DataAccessLayer;
using System.Collections.Generic;
using Faculty.DataAccessLayer.Models;
using Faculty.BusinessLayer.ModelsDto;
using Faculty.BusinessLayer.Interfaces;

namespace Faculty.BusinessLayer.Services
{
    public class StudentService : IStudentOperations
    {
        private readonly IRepository<Student> _repositoryStudent;

        public StudentService(IRepository<Student> repositoryStudent)
        {
            _repositoryStudent = repositoryStudent;
        }

        public StudentDto GetModel(int id)
        {
            var model = _repositoryStudent.GetById(id);
            Mapper.Initialize(cfg => cfg.CreateMap<Student, StudentDto>());
            return Mapper.Map<Student, StudentDto>(model);
        }

        public List<StudentDto> GetList()
        {
            var models = _repositoryStudent.GetAll().ToList();
            Mapper.Initialize(cfg => cfg.CreateMap<Student, StudentDto>());
            return Mapper.Map<List<Student>, List<StudentDto>>(models); ;
        }

        public void Create(StudentDto modelDto)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<StudentDto, Student>());
            _repositoryStudent.Insert(Mapper.Map<StudentDto, Student>(modelDto));
        }

        public void Delete(int id)
        {
            var model = _repositoryStudent.GetById(id);
            _repositoryStudent.Delete(model);
        }

        public void Edit(StudentDto modelDto)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<StudentDto, Student>());
            _repositoryStudent.Update(Mapper.Map<StudentDto, Student>(modelDto));
        }
    }
}
