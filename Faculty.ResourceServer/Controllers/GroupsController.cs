using AutoMapper;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Faculty.BusinessLayer.Dto.Group;
using Faculty.BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Faculty.ResourceServer.Models.Group;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Faculty.ResourceServer.Controllers
{
    [ApiController]
    [Route("api/groups")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GroupsController : Controller
    {
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;

        public GroupsController(IGroupService groupService, IMapper mapper)
        {
            _groupService = groupService;
            _mapper = mapper;
        }

        // GET api/groups
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<GroupDisplay>> GetGroups()
        {
            var groupsDto = _groupService.GetAll();
            if (groupsDto == null)
            {
                return NotFound();
            }

            var listGroupsDto = groupsDto.ToList();
            if (!listGroupsDto.Any())
            {
                return NotFound();
            }

            var listGroups = _mapper.Map<List<GroupDisplayDto>, List<GroupDisplay>>(listGroupsDto);
            return Ok(listGroups);
        }

        // GET api/groups/{id}
        [HttpGet("{id:int}")]
        public ActionResult<GroupModify> GetGroups(int id)
        {
            var groupDto = _groupService.GetById(id);
            if (groupDto == null)
            {
                return NotFound();
            }

            var group = _mapper.Map<GroupDto, GroupModify>(groupDto);
            return Ok(group);
        }

        // POST api/groups
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrator")]
        public ActionResult<GroupModify> Create(GroupAdd groupAdd)
        {
            var groupDto = _mapper.Map<GroupAdd, GroupDto>(groupAdd);
            groupDto = _groupService.Create(groupDto);
            return CreatedAtAction(nameof(GetGroups), new { id = groupDto.Id }, groupAdd);
        }

        // DELETE api/groups/{id}
        [HttpDelete("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrator")]
        public ActionResult Delete(int id)
        {
            var groupDto = _groupService.GetById(id);
            if (groupDto == null)
            {
                return NotFound();
            }

            _groupService.Delete(groupDto.Id);
            return NoContent();
        }

        // PUT api/groups
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrator")]
        public ActionResult Edit(GroupModify groupModify)
        {
            var groupDto = _groupService.GetById(groupModify.Id);
            if (groupDto == null)
            {
                return NotFound();
            }

            var changedGroupDto = _mapper.Map(groupModify, groupDto);
            _groupService.Edit(changedGroupDto);
            return NoContent();
        }
    }
}
