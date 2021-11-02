using AutoMapper;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Faculty.BusinessLayer.Interfaces;
using Faculty.BusinessLayer.Dto.Curator;
using Faculty.ResourceServer.Models.Curator;

namespace Faculty.ResourceServer.Controllers
{
    [ApiController]
    [Route("api/curators")]
    public class CuratorsController : Controller
    {
        private readonly ICuratorService _curatorService;
        private readonly IMapper _mapper;

        public CuratorsController(ICuratorService curatorService, IMapper mapper)
        {
            _curatorService = curatorService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CuratorDisplayModify>> GetCurators()
        {
            var curatorsDto = _curatorService.GetAll();
            var listCuratorsDto = curatorsDto.ToList();
            if (!listCuratorsDto.Any())
            {
                return NotFound();
            }

            var listCurators = _mapper.Map<List<CuratorDisplayModifyDto>, List<CuratorDisplayModify>>(listCuratorsDto);
            return Ok(listCurators);
        }

        [HttpGet("{id:int}")]
        public ActionResult<CuratorDisplayModify> GetCurators(int id)
        {
            var curatorDto = _curatorService.GetById(id);
            if (curatorDto == null)
            {
                return NotFound();
            }

            var curator = _mapper.Map<CuratorDisplayModifyDto, CuratorDisplayModify>(curatorDto);
            return Ok(curator);
        }

        [HttpPost]
        public ActionResult<CuratorDisplayModify> Create(CuratorAdd curatorAdd)
        {
            var curatorDto = _mapper.Map<CuratorAdd, CuratorAddDto>(curatorAdd);
            _curatorService.Create(curatorDto);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
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

        [HttpPut]
        public ActionResult Edit(CuratorDisplayModify curatorModify)
        {
            var curatorDto = _curatorService.GetById(curatorModify.Id);
            if (curatorDto == null)
            {
                return NotFound();
            }

            var changedCurator = _mapper.Map(curatorModify, curatorDto);
            _curatorService.Edit(changedCurator);
            return NoContent();
        }
    }
}
