using System.Net;
using System.Net.Http;
using Faculty.AspUI.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Faculty.AspUI.ViewModels.User;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Authorization;

namespace Faculty.AspUI.Controllers
{
    [Authorize(Policy = "Administrator")]
    public class UserController : Controller
    {
        private readonly IStringLocalizer<UserController> _stringLocalizer;
        private readonly UserService _userService;

        public UserController(UserService userService, IStringLocalizer<UserController> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<UserDisplay> users = default;
            try
            {
                users = await _userService.GetUsers();
            }
            catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "Home");
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }

            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                await FillViewBag();
            }
            catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "Home");
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserAdd user)
        {
            try
            {
                await FillViewBag();
                if (ModelState.IsValid == false) return View(user);
                await _userService.CreateUser(user);
            }
            catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "Home");
            }
            catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.BadRequest)
            {
                ModelState.AddModelError("", _stringLocalizer["CommonError"]);
                return View(user);
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _userService.DeleteUser(id);
            }
            catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "Home");
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            UserModify model = null;
            try
            {
                await FillViewBag();
                model = await _userService.GetUser(id);
            }
            catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "Home");
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserModify user)
        {
            try
            {
                await FillViewBag();
                if (ModelState.IsValid == false) return View(user);
                await _userService.EditUser(user);
            }
            catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "Home");
            }
            catch (HttpRequestException e) when (e.StatusCode is HttpStatusCode.NotFound)
            {
                ModelState.AddModelError("", _stringLocalizer["CommonError"]);
                return View(user);
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditPassword(string id)
        {
            var model = new UserEditPass { Id = id };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditPassword(UserEditPass user)
        {
            try
            {
                await FillViewBag();
                if (ModelState.IsValid == false) return View(user);
                await _userService.EditPasswordUser(user);
            }
            catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "Home");
            }
            catch (HttpRequestException e) when (e.StatusCode is HttpStatusCode.NotFound)
            {
                ModelState.AddModelError("", _stringLocalizer["CommonError"]);
                return View(user);
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }

            return RedirectToAction("Index");
        }

        public async Task FillViewBag()
        {
            ViewBag.Roles = await _userService.GetRoles();
        }
    }
}
