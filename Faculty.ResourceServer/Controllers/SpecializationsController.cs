using AutoMapper;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Faculty.BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Faculty.BusinessLayer.Dto.Specialization;
using Faculty.ResourceServer.Models.Specialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Faculty.ResourceServer.Controllers
{
    [ApiController]
    [Route("api/specializations")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SpecializationsController : Controller
    {
        private readonly ISpecializationService _specializationService;
        private readonly IMapper _mapper;

        public SpecializationsController(ISpecializationService specializationService, IMapper mapper)
        {
            _specializationService = specializationService;
            _mapper = mapper;
        }

        // GET api/specializations
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<SpecializationDisplayModify>> GetSpecializations()
        {
            var specializationsDto = _specializationService.GetAll();
            var listSpecializationsDto = specializationsDto.ToList();
            if (!listSpecializationsDto.Any())
            {
                return NotFound();
            }

            var listSpecializations = _mapper.Map<List<SpecializationDto>, List<SpecializationDisplayModify>>(listSpecializationsDto);
            return Ok(listSpecializations);
        }

        // GET api/specializations/{id}
        [HttpGet("{id:int}")]
        public ActionResult<SpecializationDisplayModify> GetSpecializations(int id)
        {
            var specializationDto = _specializationService.GetById(id);
            if (specializationDto == null)
            {
                return NotFound();
            }

            var specialization = _mapper.Map<SpecializationDto, SpecializationDisplayModify>(specializationDto);
            return Ok(specialization);
        }

        // POST api/specializations
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrator")]
        public ActionResult<SpecializationDisplayModify> Create(SpecializationAdd specializationAdd)
        {
            var specializationDto = _mapper.Map<SpecializationAdd, SpecializationDto>(specializationAdd);
            specializationDto = _specializationService.Create(specializationDto);
            return CreatedAtAction(nameof(GetSpecializations), new { id = specializationDto.Id }, specializationAdd);
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
        public ActionResult Edit(SpecializationDisplayModify specializationModify)
        {
            var specializationDto = _specializationService.GetById(specializationModify.Id);
            if (specializationDto == null)
            {
                return NotFound();
            }

            var changedSpecializationDto = _mapper.Map(specializationModify, specializationDto);
            _specializationService.Edit(changedSpecializationDto);
            return NoContent();
        }
    }
}
