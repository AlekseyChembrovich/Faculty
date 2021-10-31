using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
    [Route("api/users")]
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

        // GET api/users
        [HttpGet]
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

        // GET api/users/{id}
        [HttpGet("{id}")]
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

        // POST api/users
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrator")]
        public async Task<ActionResult<UserDisplay>> Create(UserAdd model)
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

        // DELETE api/users
        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrator")]
        public async Task<ActionResult> Delete(string id)
        {
            var identity = await _userManager.FindByIdAsync(id);
            if (identity == null)
            {
                return NotFound();
            }

            await _userManager.DeleteAsync(identity);
            return NoContent();
        }

        // PUT api/users
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrator")]
        public async Task<ActionResult> Edit(UserModify userModify)
        {
            var identity = await _userManager.FindByIdAsync(userModify.Id);
            if (identity == null)
            {
                return NotFound();
            }

            identity.UserName = userModify.Login;
            identity.Birthday = userModify.Birthday;
            await UpdateUserRoles(identity, userModify);
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

        // PATCH api/users
        [HttpPatch]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrator")]
        public async Task<ActionResult> EditPassword(UserModifyPassword userModifyPassword)
        {
            var identity = await _userManager.FindByIdAsync(userModifyPassword.Id);
            if (identity == null)
            {
                return NotFound();
            }

            var resultValidate = await _passwordValidator.ValidateAsync(_userManager, identity, userModifyPassword.NewPassword);
            if (resultValidate.Succeeded == false)
            {
                return BadRequest();
            }

            identity.PasswordHash = _passwordHasher.HashPassword(identity, userModifyPassword.NewPassword);
            var resultUpdate = await _userManager.UpdateAsync(identity);
            if (resultUpdate.Succeeded == false)
            {
                return BadRequest();
            }

            return NoContent();
        }

        // GET api/users/roles
        [HttpGet("roles")]
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
