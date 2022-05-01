using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Faculty.BusinessLayer.Interfaces;
using Faculty.Common.Dto.Specialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Faculty.ResourceServer.Controllers
{
    [ApiController]
    [Route("api/specializations")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SpecializationsController : Controller
    {
        private readonly ISpecializationService _specializationService;

        public SpecializationsController(ISpecializationService specializationService)
        {
            _specializationService = specializationService;
        }

        // GET api/specializations
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<SpecializationDto>> GetSpecializations()
        {
            var specializationsDto = _specializationService.GetAll();
            if (specializationsDto == null)
            {
                return NotFound();
            }

            if (!specializationsDto.Any())
            {
                return NotFound();
            }

            return Ok(specializationsDto);
        }

        // GET api/specializations/{id}
        [HttpGet("{id:int}")]
        public ActionResult<SpecializationDto> GetSpecializations(int id)
        {
            var specializationDto = _specializationService.GetById(id);
            if (specializationDto == null)
            {
                return NotFound();
            }

            return Ok(specializationDto);
        }

        // POST api/specializations
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrator")]
        public ActionResult<SpecializationDto> Create(SpecializationDto specializationDto)
        {
            specializationDto = _specializationService.Create(specializationDto);
            return CreatedAtAction(nameof(GetSpecializations), new { id = specializationDto.Id }, specializationDto);
        }

        // DELETE api/specializations/{id}
        [HttpDelete("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrator")]
        public ActionResult Delete(int id)
        {
            var specializationDto = _specializationService.GetById(id);
            if (specializationDto == null)
            {
                return NotFound();
            }

            _specializationService.Delete(specializationDto.Id);
            return NoContent();
        }

        // PUT api/specializations
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrator")]
        public ActionResult Edit(SpecializationDto specializationDto)
        {
            var specializationDtoFound = _specializationService.GetById(specializationDto.Id);
            if (specializationDtoFound == null)
            {
                return NotFound();
            }

            _specializationService.Edit(specializationDto);
            return NoContent();
        }
    }
}
