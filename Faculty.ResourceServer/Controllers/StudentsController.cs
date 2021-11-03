using AutoMapper;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Faculty.BusinessLayer.Interfaces;
using Faculty.BusinessLayer.Dto.Student;
using Microsoft.AspNetCore.Authorization;
using Faculty.ResourceServer.Models.Student;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Faculty.ResourceServer.Controllers
{
    [ApiController]
    [Route("api/students")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class StudentsController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;

        public StudentsController(IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }

        // GET api/students
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<StudentDisplayModify>> GetStudents()
        {
            var studentsDto = _studentService.GetAll();
            var listStudentsDto = studentsDto.ToList();
            if (!listStudentsDto.Any())
            {
                return NotFound();
            }

            var listStudents = _mapper.Map<List<StudentDto>, List<StudentDisplayModify>>(listStudentsDto);
            return Ok(listStudents);
        }

        // GET api/students/{id}
        [HttpGet("{id:int}")]
        public ActionResult<StudentDisplayModify> GetStudents(int id)
        {
            var curatorDto = _studentService.GetById(id);
            if (curatorDto == null)
            {
                return NotFound();
            }

            var student = _mapper.Map<StudentDto, StudentDisplayModify>(curatorDto);
            return Ok(student);
        }

        // POST api/students
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrator")]
        public ActionResult<StudentDisplayModify> Create(StudentAdd studentAdd)
        {
            var studentDto = _mapper.Map<StudentAdd, StudentDto>(studentAdd);
            studentDto = _studentService.Create(studentDto);
            return CreatedAtAction(nameof(GetStudents), new { id = studentDto.Id }, studentAdd);
        }

        // DELETE api/students/{id}
        [HttpDelete("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrator")]
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

        // PUT api/students
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrator")]
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
