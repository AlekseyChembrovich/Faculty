using AutoMapper;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Faculty.BusinessLayer.Interfaces;
using Faculty.BusinessLayer.Dto.Curator;
using Microsoft.AspNetCore.Authorization;
using Faculty.ResourceServer.Models.Curator;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Faculty.ResourceServer.Controllers
{
    [ApiController]
    [Route("api/curators")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuratorsController : Controller
    {
        private readonly ICuratorService _curatorService;
        private readonly IMapper _mapper;

        public CuratorsController(ICuratorService curatorService, IMapper mapper)
        {
            _curatorService = curatorService;
            _mapper = mapper;
        }

        // GET api/curators
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<CuratorDisplayModify>> GetCurators()
        {
            var curatorsDto = _curatorService.GetAll();
            var listCuratorsDto = curatorsDto.ToList();
            if (!listCuratorsDto.Any())
            {
                return NotFound();
            }

            var listCurators = _mapper.Map<List<CuratorDto>, List<CuratorDisplayModify>>(listCuratorsDto);
            return Ok(listCurators);
        }

        // GET api/curators/{id}
        [HttpGet("{id:int}")]
        public ActionResult<CuratorDisplayModify> GetCurators(int id)
        {
            var curatorDto = _curatorService.GetById(id);
            if (curatorDto == null)
            {
                return NotFound();
            }

            var curator = _mapper.Map<CuratorDto, CuratorDisplayModify>(curatorDto);
            return Ok(curator);
        }

        // POST api/curators
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrator")]
        public ActionResult<CuratorDisplayModify> Create(CuratorAdd curatorAdd)
        {
            var curatorDto = _mapper.Map<CuratorAdd, CuratorDto>(curatorAdd);
            curatorDto = _curatorService.Create(curatorDto);
            return CreatedAtAction(nameof(GetCurators), new { id = curatorDto.Id }, curatorAdd);
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
        public ActionResult Edit(CuratorDisplayModify curatorModify)
        {
            var curatorDto = _curatorService.GetById(curatorModify.Id);
            if (curatorDto == null)
            {
                return NotFound();
            }

            var changedCuratorDto = _mapper.Map(curatorModify, curatorDto);
            _curatorService.Edit(changedCuratorDto);
            return NoContent();
        }
    }
}
