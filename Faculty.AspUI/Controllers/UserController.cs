using System.Linq;
using Faculty.AspUI.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Faculty.AspUI.ViewModels.User;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Faculty.AspUI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "administrator")]
    public class UserController : Controller
    {
        private readonly IStringLocalizer _stringLocalizer;
        private readonly UserService _userService;

        public UserController(UserService userService, IStringLocalizer stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsers();
            return View(users?.ToList());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await FillViewBag();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserAdd user)
        {
            await FillViewBag();
            if (ModelState.IsValid == false) return View(user);
            var response = await _userService.CreateNewUser(user);
            if (response.IsSuccessStatusCode) return RedirectToAction("Index");
            ModelState.AddModelError("", _stringLocalizer["CommonError"]);
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            await _userService.DeleteExistUser(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            await FillViewBag();
            return View(await _userService.FindByIdUser(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserModify user)
        {
            await FillViewBag();
            if (ModelState.IsValid == false) return View(user);
            var response = await _userService.EditExistUser(user);
            if (response.IsSuccessStatusCode) return RedirectToAction("Index");
            ModelState.AddModelError("", _stringLocalizer["CommonError"]);
            return View(user);
        }

        [HttpGet]
        public IActionResult EditPassword(string id)
        {
            var model = new EditPassUser { Id = id };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditPassword(EditPassUser user)
        {
            await FillViewBag();
            if (ModelState.IsValid == false) return View(user);
            var response = await _userService.EditPasswordExistUser(user);
            if (response.IsSuccessStatusCode) return RedirectToAction("Index");
            ModelState.AddModelError("", _stringLocalizer["CommonError"]);
            return View(user);
        }

        public async Task FillViewBag()
        {
            ViewBag.Roles = await _userService.GetAllRoles();
        }
    }
}
