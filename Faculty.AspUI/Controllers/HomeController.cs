using System;
using AutoMapper;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Faculty.AspUI.Tools;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Faculty.Common.Dto.LoginRegister;
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
        private readonly IMapper _mapper;

        public HomeController(IAuthService authService, IStringLocalizer<HomeController> stringLocalizer, AuthOptions authOptions, IMapper mapper)
        {
            _authService = authService;
            _authOptions = authOptions;
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginUser loginUser)
        {
            if (ModelState.IsValid == false) return View(loginUser);
            HttpResponseMessage response = default;
            try
            {
                var authUserDto = _mapper.Map<LoginUser, AuthUserDto>(loginUser);
                response = await _authService.Login(authUserDto);
            }
            catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.BadRequest)
            {
                ModelState.AddModelError(string.Empty, _stringLocalizer["CommonError"]);
                return View(loginUser);
            }
            catch (HttpRequestException)
            {
                return RedirectToAction("Error", "Home");
            }

            var tokenJson = await response.Content.ReadAsStringAsync();
            var objectWithToken = JsonConvert.DeserializeObject<dynamic>(tokenJson);
            string token = objectWithToken.jwtToken.ToString();
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
        public async Task<IActionResult> Register(RegisterUser registerUser)
        {
            if (ModelState.IsValid == false) return View(registerUser);
            try
            {
                var authUserDto = _mapper.Map<RegisterUser, AuthUserDto>(registerUser);
                await _authService.Register(authUserDto);
            }
            catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.BadRequest)
            {
                ModelState.AddModelError(string.Empty, _stringLocalizer["CommonError"]);
                return View(registerUser);
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
