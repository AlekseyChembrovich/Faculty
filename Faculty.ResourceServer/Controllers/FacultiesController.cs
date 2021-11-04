using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Faculty.Common.Dto.Faculty;
using Faculty.BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Faculty.ResourceServer.Controllers
{
    [ApiController]
    [Route("api/faculties")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FacultiesController : Controller
    {
        private readonly IFacultyService _facultyService;

        public FacultiesController(IFacultyService facultyService)
        {
            _facultyService = facultyService;
        }

        // GET api/faculties
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<FacultyDto>> GetFaculties()
        {
            var facultiesDto = _facultyService.GetAll();
            if (facultiesDto == null)
            {
                return NotFound();
            }

            if (!facultiesDto.Any())
            {
                return NotFound();
            }

            return Ok(facultiesDto);
        }

        // GET api/faculties/{id}
        [HttpGet("{id:int}")]
        public ActionResult<FacultyDto> GetFaculties(int id)
        {
            var facultyDto = _facultyService.GetById(id);
            if (facultyDto == null)
            {
                return NotFound();
            }

            return Ok(facultyDto);
        }

        // POST api/faculties
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrator")]
        public ActionResult<FacultyDto> Create(FacultyDto facultyDto)
        {
            facultyDto = _facultyService.Create(facultyDto);
            return CreatedAtAction(nameof(GetFaculties), new { id = facultyDto.Id }, facultyDto);
        }

        // DELETE api/faculties/{id}
        [HttpDelete("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrator")]
        public ActionResult Delete(int id)
        {
            var facultyDto = _facultyService.GetById(id);
            if (facultyDto == null)
            {
                return NotFound();
            }

            _facultyService.Delete(facultyDto.Id);
            return NoContent();
        }

        // PUT api/faculties
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrator")]
        public ActionResult Edit(FacultyDto facultyDto)
        {
            var facultyDtoFound = _facultyService.GetById(facultyDto.Id);
            if (facultyDtoFound == null)
            {
                return NotFound();
            }

            _facultyService.Edit(facultyDto);
            return NoContent();
        }
    }
}
