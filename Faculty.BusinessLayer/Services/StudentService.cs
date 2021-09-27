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

        public List<DisplayStudentDto> GetList()
        {
            var models = _repositoryStudent.GetAll().ToList();
            Mapper.Initialize(cfg => cfg.CreateMap<Student, DisplayStudentDto>());
            return Mapper.Map<List<Student>, List<DisplayStudentDto>>(models); ;
        }

        public void Create(CreateStudentDto modelDto)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<CreateStudentDto, Student>());
            _repositoryStudent.Insert(Mapper.Map<CreateStudentDto, Student>(modelDto));
        }

        public void Delete(int id)
        {
            var model = _repositoryStudent.GetById(id);
            _repositoryStudent.Delete(model);
        }

        public EditStudentDto GetModel(int id)
        {
            var model = _repositoryStudent.GetById(id);
            Mapper.Initialize(cfg => cfg.CreateMap<Student, EditStudentDto>());
            return Mapper.Map<Student, EditStudentDto>(model);
        }

        public void Edit(EditStudentDto modelDto)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<EditStudentDto, Student>());
            _repositoryStudent.Update(Mapper.Map<EditStudentDto, Student>(modelDto));
        }
    }
}
