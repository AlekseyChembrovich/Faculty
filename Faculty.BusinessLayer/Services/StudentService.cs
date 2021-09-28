using AutoMapper;
using System.Linq;
using Faculty.DataAccessLayer;
using System.Collections.Generic;
using Faculty.DataAccessLayer.Models;
using Faculty.BusinessLayer.Interfaces;
using Faculty.BusinessLayer.ModelsDto.StudentDto;

namespace Faculty.BusinessLayer.Services
{
    public class StudentService : IStudentOperations
    {
        private readonly IRepository<Student> _repositoryStudent;

        public StudentService(IRepository<Student> repositoryStudent)
        {
            _repositoryStudent = repositoryStudent;
        }

        public IEnumerable<DisplayStudentDto> GetList()
        {
            var models = _repositoryStudent.GetAll().ToList();
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            return mapper.Map<IEnumerable<Student>, IEnumerable<DisplayStudentDto>>(models);
        }

        public void Create(CreateStudentDto modelDto)
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            _repositoryStudent.Insert(mapper.Map<CreateStudentDto, Student>(modelDto));
        }

        public void Delete(int id)
        {
            var model = _repositoryStudent.GetById(id);
            _repositoryStudent.Delete(model);
        }

        public EditStudentDto GetModel(int id)
        {
            var model = _repositoryStudent.GetById(id);
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            return mapper.Map<Student, EditStudentDto>(model);
        }

        public void Edit(EditStudentDto modelDto)
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SourceMappingProfile()));
            var mapper = new Mapper(mapperConfiguration);
            _repositoryStudent.Update(mapper.Map<EditStudentDto, Student>(modelDto));
        }
    }
}
