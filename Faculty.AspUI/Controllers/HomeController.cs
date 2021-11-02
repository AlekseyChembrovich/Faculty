using System;
using System.Net;
using System.Net.Http;
using Faculty.AspUI.Tools;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Localization;
using Faculty.AspUI.Services.Interfaces;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Faculty.AspUI.ViewModels.LoginRegister;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Faculty.AspUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly AuthOptions _authOptions;
        private readonly IAuthService _authService;
        private readonly IStringLocalizer<HomeController> _stringLocalizer;

        public HomeController(IAuthService authService, IStringLocalizer<HomeController> stringLocalizer, AuthOptions authOptions)
        {
            _authService = authService;
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
            HttpResponseMessage response = default;
            try
            {
                response = await _authService.Login(user);
            }
            catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.BadRequest)
            {
                ModelState.AddModelError(string.Empty, _stringLocalizer["CommonError"]);
                return View(user);
            }
            catch (HttpRequestException)
            {
                return RedirectToAction("Error", "Home");
            }

            var token = await response.Content.ReadAsStringAsync();
            var principal = ValidateToken(token);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddDays(_authOptions.Lifetime)
            });

            HttpContext.Response.Cookies.Append("access_token", token, new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(_authOptions.Lifetime)
            });

            return RedirectToAction("Index", "Faculty");
        }

        private ClaimsPrincipal ValidateToken(string token)
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _authOptions?.Issuer,
                ValidateAudience = true,
                ValidAudience = _authOptions?.Audience,
                IssuerSigningKey = _authOptions?.GetSymmetricSecurityKey(),
                ValidateIssuerSigningKey = true
            };

            var principal = new JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out SecurityToken validatedToken);
            return principal;
        }

        [HttpGet]
        [Authorize(Policy = "Common")]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Response.Cookies.Delete("access_token");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
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
            try
            {
                await _authService.Register(user);
            }
            catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.BadRequest)
            {
                ModelState.AddModelError(string.Empty, _stringLocalizer["CommonError"]);
                return View(user);
            }
            catch (HttpRequestException)
            {
                return RedirectToAction("Error", "Home");
            }

            return RedirectToAction("Login");
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Localize(string culture, string returnUrl)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)), new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
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
