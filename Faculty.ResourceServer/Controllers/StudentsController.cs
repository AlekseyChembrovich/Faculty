using AutoMapper;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Faculty.BusinessLayer.Interfaces;
using Faculty.BusinessLayer.Dto.Student;
using Faculty.ResourceServer.Models.Student;

namespace Faculty.ResourceServer.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;

        public StudentsController(IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<StudentDisplayModify>> GetStudents()
        {
            var studentsDto = _studentService.GetAll();
            var listStudentsDto = studentsDto.ToList();
            if (!listStudentsDto.Any())
            {
                return NotFound();
            }

            var listCurators = _mapper.Map<List<StudentDisplayModifyDto>, List<StudentDisplayModify>>(listStudentsDto);
            return Ok(listCurators);
        }

        [HttpGet("{id:int}")]
        public ActionResult<StudentDisplayModify> GetStudents(int id)
        {
            var curatorDto = _studentService.GetById(id);
            if (curatorDto == null)
            {
                return NotFound();
            }

            var curator = _mapper.Map<StudentDisplayModifyDto, StudentDisplayModify>(curatorDto);
            return Ok(curator);
        }

        [HttpPost]
        public ActionResult<StudentDisplayModify> Create()
        {
            return View();
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var studentDto = _studentService.GetById(id);
            if (studentDto == null)
            {
                return NotFound();
            }

            _studentService.Delete(studentDto.Id);
            return NoContent();
        }

        [HttpPut]
        public ActionResult Edit(StudentDisplayModify studentModify)
        {
            var studentDto = _studentService.GetById(studentModify.Id);
            if (studentDto == null)
            {
                return NotFound();
            }

            var changedStudentDto = _mapper.Map(studentModify, studentDto);
            _studentService.Edit(changedStudentDto);
            return NoContent();
        }
    }
}
