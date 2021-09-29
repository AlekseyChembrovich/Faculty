using AutoMapper;
using System.Linq;
using System.Collections.Generic;
using Faculty.DataAccessLayer.Models;
using Faculty.BusinessLayer.Interfaces;
using Faculty.BusinessLayer.Dto.Student;
using Faculty.DataAccessLayer.Repository;

namespace Faculty.BusinessLayer.Services
{
    public class StudentService : IStudentService
    {
        private readonly IRepository<Student> _repositoryStudent;

        public StudentService(IRepository<Student> repositoryStudent)
        {
            _repositoryStudent = repositoryStudent;
        }

        public IEnumerable<StudentDisplayModifyDto> GetAll()
        {
            var models = _repositoryStudent.GetAll().ToList();
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            return mapper.Map<IEnumerable<Student>, IEnumerable<StudentDisplayModifyDto>>(models);
        }

        public void Create(StudentAddDto modelDto)
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            _repositoryStudent.Insert(mapper.Map<StudentAddDto, Student>(modelDto));
        }

        public void Delete(int id)
        {
            var model = _repositoryStudent.GetById(id);
            _repositoryStudent.Delete(model);
        }

        public StudentDisplayModifyDto GetById(int id)
        {
            var model = _repositoryStudent.GetById(id);
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            return mapper.Map<Student, StudentDisplayModifyDto>(model);
        }

        public void Edit(StudentDisplayModifyDto modelDto)
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            _repositoryStudent.Update(mapper.Map<StudentDisplayModifyDto, Student>(modelDto));
        }
    }
}
