using System;
using System.Text;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Authorization;
using Faculty.AspUI.ViewModels.LoginRegister;

namespace Faculty.AspUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly AuthOptions _authOptions;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IStringLocalizer _stringLocalizer;

        public HomeController(IHttpClientFactory clientFactory, IStringLocalizer stringLocalizer, AuthOptions authOptions)
        {
            _clientFactory = clientFactory;
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
            var client = _clientFactory.CreateClient("usersHttpClient");
            var response = await client.PostAsync("LoginRegister/Login", new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode == false)
            {
                ModelState.AddModelError("", _stringLocalizer["CommonError"]);
                return View(user);
            }

            var result = await response.Content.ReadAsStringAsync();
            HttpContext.Response.Cookies.Append("access_token", result, new CookieOptions { HttpOnly = true, Secure = true, Expires = DateTime.Now.AddDays(_authOptions.Lifetime) });
            return RedirectToAction("Index", "Faculty");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Append("access_token", "", new CookieOptions { HttpOnly = true, Secure = true, Expires = DateTime.Now.AddDays(_authOptions.Lifetime) });
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
            var client = _clientFactory.CreateClient("usersHttpClient");
            var response = await client.PostAsync("LoginRegister/Register", new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json"));
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
    }
}
