using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Faculty.AspUI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Localization;
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
            var principal = ValidateToken(result);
            if (principal is not null)
            {
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddDays(_authOptions.Lifetime)
                });

                HttpContext.Response.Cookies.Append("access_token", result, new CookieOptions
                {
                    Expires = DateTime.UtcNow.AddDays(_authOptions.Lifetime)
                });
                return RedirectToAction("Index", "Faculty");
            }

            ModelState.AddModelError("", _stringLocalizer["CommonError"]);
            return View(user);
        }

        private ClaimsPrincipal ValidateToken(string jwtToken)
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

            var principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out SecurityToken validatedToken);
            return principal;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "administrator,employee")]
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
