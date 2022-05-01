using AutoMapper;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Faculty.Common.Dto.User;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Faculty.AspUI.ViewModels.User;
using Faculty.AspUI.Services.Interfaces;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Authorization;

namespace Faculty.AspUI.Controllers
{
    [Authorize(Policy = "Administrator")]
    public class UserController : Controller
    {
        private readonly IStringLocalizer<UserController> _stringLocalizer;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IStringLocalizer<UserController> stringLocalizer, IMapper mapper)
        {
            _stringLocalizer = stringLocalizer;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            IEnumerable<UserDisplay> usersDisplay = default;
            try
            {
                usersDisplay = _mapper.Map<IEnumerable<UserDto>, IEnumerable<UserDisplay>>(await _userService.GetUsers());
            }
            catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "Home");
            }
            catch (HttpRequestException)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(usersDisplay);
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
            catch (HttpRequestException)
            {
                return RedirectToAction("Error", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserAdd userAdd)
        {
            try
            {
                await FillViewBag();
                if (ModelState.IsValid == false) return View(userAdd);
                var userAddDto = _mapper.Map<UserAdd, UserAddDto>(userAdd);
                await _userService.CreateUser(userAddDto);
            }
            catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "Home");
            }
            catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.BadRequest)
            {
                ModelState.AddModelError("", _stringLocalizer["CommonError"]);
                return View(userAdd);
            }
            catch (HttpRequestException)
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
            catch (HttpRequestException)
            {
                return RedirectToAction("Error", "Home");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            UserModify userModify = null;
            try
            {
                await FillViewBag();
                userModify = _mapper.Map<UserDto, UserModify>(await _userService.GetUser(id));
            }
            catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "Home");
            }
            catch (HttpRequestException)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(userModify);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserModify userModify)
        {
            try
            {
                await FillViewBag();
                if (ModelState.IsValid == false) return View(userModify);
                var userDto = _mapper.Map<UserModify, UserDto>(userModify);
                await _userService.EditUser(userDto);
            }
            catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "Home");
            }
            catch (HttpRequestException e) when (e.StatusCode is HttpStatusCode.BadRequest)
            {
                ModelState.AddModelError("", _stringLocalizer["CommonError"]);
                return View(userModify);
            }
            catch (HttpRequestException)
            {
                return RedirectToAction("Error", "Home");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditPassword(string id)
        {
            var userModifyPassword = new UserModifyPassword { Id = id };
            return View(userModifyPassword);
        }

        [HttpPost]
        public async Task<IActionResult> EditPassword(UserModifyPassword userModifyPassword)
        {
            try
            {
                await FillViewBag();
                if (ModelState.IsValid == false) return View(userModifyPassword);
                var userModifyPasswordDto = _mapper.Map<UserModifyPassword, UserModifyPasswordDto>(userModifyPassword);
                await _userService.EditPasswordUser(userModifyPasswordDto);
            }
            catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "Home");
            }
            catch (HttpRequestException e) when (e.StatusCode is HttpStatusCode.BadRequest)
            {
                ModelState.AddModelError("", _stringLocalizer["CommonError"]);
                return View(userModifyPassword);
            }
            catch (HttpRequestException)
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
