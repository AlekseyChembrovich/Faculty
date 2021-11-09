using System;
using System.Threading.Tasks;
using Faculty.Common.Dto.User;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Faculty.AuthenticationServer.Services.Interfaces;

namespace Faculty.AuthenticationServer.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET api/users
        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> GetUsers()
        {
            var usersDto = _userService.GetUsers(out var identityResult);
            if (!identityResult.Succeeded)
            {
                return NotFound();
            }

            return Ok(usersDto);
        }

        // GET api/users/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUsers(string id)
        {
            var (userDto, identityResult) = await _userService.GetUser(id);
            if (identityResult == null)
            {
                return NotFound();
            }

            return Ok(userDto);
        }

        // POST api/users
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrator")]
        public async Task<ActionResult<UserDto>> Create(UserAddDto userAddDto)
        {
            var (userDto, identityResult) = await _userService.CreateUser(userAddDto);
            if (!identityResult.Succeeded)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetUsers), new { id = userDto.Id }, userAddDto);
        }

        // DELETE api/users/{id}
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrator")]
        public async Task<ActionResult> Delete(string id)
        {
            var identityResult = await _userService.DeleteUser(id);
            if (!identityResult.Succeeded)
            {
                return NotFound();
            }

            return NoContent();
        }

        // PUT api/users
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrator")]
        public async Task<ActionResult> Edit(UserDto userDto)
        {
            var identityResult = await _userService.EditUser(userDto);
            if (!identityResult.Succeeded)
            {
                return BadRequest();
            }

            return NoContent();
        }

        // PATCH api/users
        [HttpPatch]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrator")]
        public async Task<ActionResult> EditPassword(UserModifyPasswordDto userModifyPasswordDto)
        {
            var identityResult = await _userService.EditPasswordUser(userModifyPasswordDto);
            if (!identityResult.Succeeded)
            {
                return BadRequest();
            }

            return NoContent();
        }

        // GET api/users/roles
        [HttpGet("roles")]
        public ActionResult<IEnumerable<string>> GetRoles()
        {
            var namesRole = _userService.GetRoles(out var identityResult);
            if (!identityResult.Succeeded)
            {
                return NotFound();
            }

            return Ok(namesRole);
        }
    }
}
