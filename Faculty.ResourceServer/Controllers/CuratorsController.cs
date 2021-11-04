using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Faculty.Common.Dto.Curator;
using Faculty.BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Faculty.ResourceServer.Controllers
{
    [ApiController]
    [Route("api/curators")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuratorsController : Controller
    {
        private readonly ICuratorService _curatorService;

        public CuratorsController(ICuratorService curatorService)
        {
            _curatorService = curatorService;
        }

        // GET api/curators
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<CuratorDto>> GetCurators()
        {
            var curatorsDto = _curatorService.GetAll();
            if (curatorsDto == null)
            {
                return NotFound();
            }

            if (!curatorsDto.Any())
            {
                return NotFound();
            }

            return Ok(curatorsDto);
        }

        // GET api/curators/{id}
        [HttpGet("{id:int}")]
        public ActionResult<CuratorDto> GetCurators(int id)
        {
            var curatorDto = _curatorService.GetById(id);
            if (curatorDto == null)
            {
                return NotFound();
            }

            return Ok(curatorDto);
        }

        // POST api/curators
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrator")]
        public ActionResult<CuratorDto> Create(CuratorDto curatorDto)
        {
            curatorDto = _curatorService.Create(curatorDto);
            return CreatedAtAction(nameof(GetCurators), new { id = curatorDto.Id }, curatorDto);
        }

        // DELETE api/curators/{id}
        [HttpDelete("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrator")]
        public ActionResult Delete(int id)
        {
            var curatorDto = _curatorService.GetById(id);
            if (curatorDto == null)
            {
                return NotFound();
            }

            _curatorService.Delete(curatorDto.Id);
            return NoContent();
        }

        // PUT api/curators
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrator")]
        public ActionResult Edit(CuratorDto curatorDto)
        {
            var curatorDtoFound = _curatorService.GetById(curatorDto.Id);
            if (curatorDtoFound == null)
            {
                return NotFound();
            }

            _curatorService.Edit(curatorDto);
            return NoContent();
        }
    }
}
