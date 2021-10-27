using System;
using System.Threading.Tasks;
using Faculty.AspUI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Authorization;
using Faculty.AspUI.ViewModels.LoginRegister;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Faculty.AspUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly AuthOptions _authOptions;
        private readonly UserService _userService;
        private readonly IStringLocalizer _stringLocalizer;

        public HomeController(UserService userService, IStringLocalizer stringLocalizer, AuthOptions authOptions)
        {
            _userService = userService;
            _authOptions = authOptions;
            _stringLocalizer = stringLocalizer;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginUser user)
        {
            if (ModelState.IsValid == false) return View(user);
            var response = await _userService.GetLoginResponse(user);
            if (response.IsSuccessStatusCode == false)
            {
                ModelState.AddModelError("", _stringLocalizer["CommonError"]);
                return View(user);
            }

            var result = await response.Content.ReadAsStringAsync();
            HttpContext.Response.Cookies.Append("access_token", result, new CookieOptions { Expires = DateTime.Now.AddDays(_authOptions.Lifetime) });
            return RedirectToAction("Index", "Faculty");
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "administrator,employee")]
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Append("access_token", string.Empty, new CookieOptions { Expires = DateTime.Now.AddDays(_authOptions.Lifetime) });
            return RedirectToAction("Index", "Faculty");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterUser user)
        {
            if (ModelState.IsValid == false) return View(user);
            var response = await _userService.GetRegisterResponse(user);
            if (response.IsSuccessStatusCode) return RedirectToAction("Login");
            ModelState.AddModelError("", _stringLocalizer["CommonError"]);
            return View(user);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Localize(string culture, string returnUrl)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)), 
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
            return LocalRedirect(returnUrl);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Error()
        {
            return View();
        }
    }
}
