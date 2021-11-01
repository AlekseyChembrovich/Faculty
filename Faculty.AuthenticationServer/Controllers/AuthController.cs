using System;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Faculty.AuthenticationServer.Tools;
using Faculty.AuthenticationServer.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Faculty.AuthenticationServer.Models.LoginRegister;

namespace Faculty.AuthenticationServer.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly UserManager<CustomUser> _userManager;
        private readonly SignInManager<CustomUser> _signInManager;
        private readonly AuthOptions _authOptions;

        public AuthController(UserManager<CustomUser> userManager, SignInManager<CustomUser> signInManager, AuthOptions authOptions)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authOptions = authOptions;
        }

        // POST api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUser user)
        {
            var identity = await _userManager.FindByNameAsync(user.Login);
            if (identity == null)
            {
                return BadRequest();
            }

            var result = await _signInManager.CheckPasswordSignInAsync(identity, user.Password, false);
            if (result.Succeeded == false)
            {
                return BadRequest();
            }

            var claimsIdentity = GetIdentityClaims(identity);
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: _authOptions.Issuer,
                audience: _authOptions.Audience,
                notBefore: now,
                claims: claimsIdentity.Claims,
                expires: now.Add(TimeSpan.FromDays(_authOptions.Lifetime)),
                signingCredentials: new SigningCredentials(_authOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return Ok(token);
        }

        private ClaimsIdentity GetIdentityClaims(CustomUser identity)
        {
            var roles = _userManager.GetRolesAsync(identity).Result;
            var claims = new List<Claim>
            {
                new (ClaimsIdentity.DefaultNameClaimType, identity.UserName)
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));
            return new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme, ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }

        // POST api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUser user)
        {
            var identity = new CustomUser { UserName = user.Login };
            var result = await _userManager.CreateAsync(identity, user.Password);
            if (result.Succeeded == false)
            {
                return BadRequest();
            }

            await _userManager.AddToRoleAsync(identity, "employee");
            return NoContent();
        }
    }
}
