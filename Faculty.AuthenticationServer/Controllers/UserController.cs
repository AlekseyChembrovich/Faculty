using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Faculty.AuthenticationServer.Models;
using Faculty.AuthenticationServer.Models.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Faculty.AuthenticationServer.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "administrator")]
    public class UserController : Controller
    {
        private readonly UserManager<CustomUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IPasswordValidator<CustomUser> _passwordValidator;
        private readonly IPasswordHasher<CustomUser> _passwordHasher;

        public UserController(UserManager<CustomUser> userManager, RoleManager<IdentityRole> roleManager, IPasswordValidator<CustomUser> passwordValidator, IPasswordHasher<CustomUser> passwordHasher, AuthOptions authOptions)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _passwordValidator = passwordValidator;
            _passwordHasher = passwordHasher;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userManager.Users.ToList();
            var usersDisplay = users.Select(user => new UserDisplay
            {
                Id = user.Id,
                Login = user.UserName,
                Roles = _userManager.GetRolesAsync(user).Result,
                Birthday = user.Birthday
            }).ToList();

            return Json(usersDisplay);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
            var identity = await _userManager.FindByIdAsync(id);
            var model = new UserDisplay
            {
                Id = identity.Id,
                Login = identity.UserName,
                Roles = _userManager.GetRolesAsync(identity).Result,
                Birthday = identity.Birthday
            };

            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserAdd model)
        {
            var identity = new CustomUser { UserName = model.Login, Birthday = model.Birthday };
            var result = await _userManager.CreateAsync(identity, model.Password);
            if (result.Succeeded == false) return BadRequest();
            await _userManager.AddToRolesAsync(identity, model.Roles);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var identity = await _userManager.FindByIdAsync(id);
            var result = await _userManager.DeleteAsync(identity);
            if (result.Succeeded == false) return BadRequest();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserModify model)
        {
            var identity = await _userManager.FindByIdAsync(model.Id);
            identity.UserName = model.Login;
            identity.Birthday = model.Birthday;
            var roles = await _userManager.GetRolesAsync(identity);
            var resultRemove = await _userManager.RemoveFromRolesAsync(identity, roles);
            if (resultRemove.Succeeded == false) return BadRequest();
            var resultAdd = await _userManager.AddToRolesAsync(identity, model.Roles);
            if (resultAdd.Succeeded == false) return BadRequest();
            var resultUpdate = await _userManager.UpdateAsync(identity);
            if (resultUpdate.Succeeded == false) return BadRequest();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> EditPassword(EditPassUser model)
        {
            var identity = await _userManager.FindByIdAsync(model.Id);
            var resultValidate = await _passwordValidator.ValidateAsync(_userManager, identity, model.NewPassword);
            if (resultValidate.Succeeded == false) return BadRequest();
            identity.PasswordHash = _passwordHasher.HashPassword(identity, model.NewPassword);
            var resultUpdate = await _userManager.UpdateAsync(identity);
            if (resultUpdate.Succeeded == false) return BadRequest();
            return Ok();
        }

        [HttpGet]
        public IActionResult GetRoles()
        {
            var namesRole = _roleManager.Roles.ToList().Select(x => x.Name);
            return Json(namesRole);
        }
    }
}
