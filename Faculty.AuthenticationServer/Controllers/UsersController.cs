using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Faculty.AuthenticationServer.Tools;
using Faculty.AuthenticationServer.Models;
using Faculty.AuthenticationServer.Models.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Faculty.AuthenticationServer.Controllers
{
    [ApiController]
    [Route("users")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController : Controller
    {
        private readonly UserManager<CustomUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IPasswordValidator<CustomUser> _passwordValidator;
        private readonly IPasswordHasher<CustomUser> _passwordHasher;

        public UsersController(UserManager<CustomUser> userManager, RoleManager<IdentityRole> roleManager, IPasswordValidator<CustomUser> passwordValidator, IPasswordHasher<CustomUser> passwordHasher, AuthOptions authOptions)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _passwordValidator = passwordValidator;
            _passwordHasher = passwordHasher;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserDisplay>))]
        public ActionResult<IEnumerable<UserDisplay>> GetUsers()
        {
            var users = _userManager.Users.ToList();
            if (!users.Any())
            {
                return NotFound();
            }

            var usersDisplay = users.Select(user => new UserDisplay
            {
                Id = user.Id,
                Login = user.UserName,
                Roles = _userManager.GetRolesAsync(user).Result,
                Birthday = user.Birthday
            }).ToList();

            return Ok(usersDisplay);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDisplay))]
        public async Task<ActionResult<UserDisplay>> GetUsers(string id)
        {
            var identity = await _userManager.FindByIdAsync(id);
            if (identity == null)
            {
                return NotFound();
            }

            var model = new UserDisplay
            {
                Id = identity.Id,
                Login = identity.UserName,
                Roles = _userManager.GetRolesAsync(identity).Result,
                Birthday = identity.Birthday
            };

            return Ok(model);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrator")]
        public async Task<ActionResult> CreateUser(UserAdd model)
        {
            var identity = new CustomUser { UserName = model.Login, Birthday = model.Birthday };
            var result = await _userManager.CreateAsync(identity, model.Password);
            if (result.Succeeded == false)
            {
                return BadRequest();
            }

            await _userManager.AddToRolesAsync(identity, model.Roles);
            return CreatedAtAction(nameof(GetUsers), new { id = identity.Id }, model);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrator")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var identity = await _userManager.FindByIdAsync(id);
            if (identity == null)
            {
                return NotFound();
            }

            await _userManager.DeleteAsync(identity);
            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrator")]
        public async Task<IActionResult> Edit(UserModify model)
        {
            var identity = await _userManager.FindByIdAsync(model.Id);
            if (identity == null)
            {
                return NotFound();
            }

            identity.UserName = model.Login;
            identity.Birthday = model.Birthday;
            await UpdateUserRoles(identity, model);
            var resultUpdate = await _userManager.UpdateAsync(identity);
            if (resultUpdate.Succeeded == false)
            {
                return BadRequest();
            }

            return NoContent();
        }

        private async Task UpdateUserRoles(CustomUser customUser, UserModify modifyUser)
        {
            var roles = await _userManager.GetRolesAsync(customUser);
            await _userManager.RemoveFromRolesAsync(customUser, roles);
            await _userManager.AddToRolesAsync(customUser, modifyUser.Roles);
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrator")]
        public async Task<IActionResult> EditPassword(UserEditPass model)
        {
            var identity = await _userManager.FindByIdAsync(model.Id);
            if (identity == null)
            {
                return NotFound();
            }

            var resultValidate = await _passwordValidator.ValidateAsync(_userManager, identity, model.NewPassword);
            if (resultValidate.Succeeded == false)
            {
                return BadRequest();
            }

            identity.PasswordHash = _passwordHasher.HashPassword(identity, model.NewPassword);
            var resultUpdate = await _userManager.UpdateAsync(identity);
            if (resultUpdate.Succeeded == false)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpGet("roles")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<string>))]
        public ActionResult<IEnumerable<string>> GetRoles()
        {
            var namesRole = _roleManager.Roles.ToList().Select(x => x.Name);
            if (!namesRole.Any())
            {
                return NotFound();
            }

            return Ok(namesRole);
        }
    }
}
