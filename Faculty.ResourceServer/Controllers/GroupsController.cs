using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Faculty.Common.Dto.Group;
using System.Collections.Generic;
using Faculty.BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Faculty.ResourceServer.Controllers
{
    [ApiController]
    [Route("api/groups")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GroupsController : Controller
    {
        private readonly IGroupService _groupService;

        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        // GET api/groups
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<GroupDto>> GetGroups()
        {
            var groupsDto = _groupService.GetAll();
            if (groupsDto == null)
            {
                return NotFound();
            }

            if (!groupsDto.Any())
            {
                return NotFound();
            }

            return Ok(groupsDto);
        }

        // GET api/groups/{id}
        [HttpGet("{id:int}")]
        public ActionResult<GroupDto> GetGroups(int id)
        {
            var groupDto = _groupService.GetById(id);
            if (groupDto == null)
            {
                return NotFound();
            }

            return Ok(groupDto);
        }

        // POST api/groups
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrator")]
        public ActionResult<GroupDto> Create(GroupDto groupDto)
        {
            groupDto = _groupService.Create(groupDto);
            return CreatedAtAction(nameof(GetGroups), new { id = groupDto.Id }, groupDto);
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
        public ActionResult Edit(GroupDto groupDto)
        {
            var groupDtoFound = _groupService.GetById(groupDto.Id);
            if (groupDtoFound == null)
            {
                return NotFound();
            }

            _groupService.Edit(groupDto);
            return NoContent();
        }
    }
}
