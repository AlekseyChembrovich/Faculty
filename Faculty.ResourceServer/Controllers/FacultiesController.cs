using AutoMapper;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Faculty.BusinessLayer.Interfaces;
using Faculty.BusinessLayer.Dto.Faculty;
using Microsoft.AspNetCore.Authorization;
using Faculty.ResourceServer.Models.Faculty;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Faculty.ResourceServer.Controllers
{
    [ApiController]
    [Route("api/faculties")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FacultiesController : Controller
    {
        private readonly IFacultyService _facultyService;
        private readonly IMapper _mapper;

        public FacultiesController(IFacultyService facultyService, IMapper mapper)
        {
            _facultyService = facultyService;
            _mapper = mapper;
        }

        // GET api/faculties
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<FacultyDisplay>> GetFaculties()
        {
            var facultiesDto = _facultyService.GetAll();
            if (facultiesDto == null)
            {
                return NotFound();
            }

            var listFacultiesDto = facultiesDto.ToList();
            if (!listFacultiesDto.Any())
            {
                return NotFound();
            }

            var listFaculties = _mapper.Map<List<FacultyDisplayDto>, List<FacultyDisplay>>(listFacultiesDto);
            return Ok(listFaculties);
        }

        // GET api/faculties/{id}
        [HttpGet("{id:int}")]
        public ActionResult<FacultyModify> GetFaculties(int id)
        {
            var facultyDto = _facultyService.GetById(id);
            if (facultyDto == null)
            {
                return NotFound();
            }

            var faculty = _mapper.Map<FacultyDto, FacultyModify>(facultyDto);
            return Ok(faculty);
        }

        // POST api/faculties
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrator")]
        public ActionResult<FacultyModify> Create(FacultyAdd facultyAdd)
        {
            var facultyDto = _mapper.Map<FacultyAdd, FacultyDto>(facultyAdd);
            facultyDto = _facultyService.Create(facultyDto);
            return CreatedAtAction(nameof(GetFaculties), new { id = facultyDto.Id }, facultyAdd);
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
        public ActionResult Edit(FacultyModify facultyModify)
        {
            var facultyDto = _facultyService.GetById(facultyModify.Id);
            if (facultyDto == null)
            {
                return NotFound();
            }

            var changedFacultyDto = _mapper.Map(facultyModify, facultyDto);
            _facultyService.Edit(changedFacultyDto);
            return NoContent();
        }
    }
}
