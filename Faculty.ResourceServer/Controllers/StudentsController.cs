using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Faculty.Common.Dto.Student;
using Faculty.BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Faculty.ResourceServer.Controllers
{
    [ApiController]
    [Route("api/students")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class StudentsController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // GET api/students
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<StudentDto>> GetStudents()
        {
            var studentsDto = _studentService.GetAll();
            if (studentsDto == null)
            {
                return NotFound();
            }

            if (!studentsDto.Any())
            {
                return NotFound();
            }

            return Ok(studentsDto);
        }

        // GET api/students/{id}
        [HttpGet("{id:int}")]
        public ActionResult<StudentDto> GetStudents(int id)
        {
            var curatorDto = _studentService.GetById(id);
            if (curatorDto == null)
            {
                return NotFound();
            }

            return Ok(curatorDto);
        }

        // POST api/students
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrator")]
        public ActionResult<StudentDto> Create(StudentDto studentDto)
        {
            studentDto = _studentService.Create(studentDto);
            return CreatedAtAction(nameof(GetStudents), new { id = studentDto.Id }, studentDto);
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
        public ActionResult Edit(StudentDto studentDto)
        {
            var studentDtoFound = _studentService.GetById(studentDto.Id);
            if (studentDtoFound == null)
            {
                return NotFound();
            }

            _studentService.Edit(studentDto);
            return NoContent();
        }
    }
}
