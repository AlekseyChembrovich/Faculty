using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Faculty.AuthenticationServer.Models.LoginRegister;

namespace Faculty.AuthenticationServer.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class LoginRegisterController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly AuthOptions _authOptions;

        public LoginRegisterController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, AuthOptions authOptions)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authOptions = authOptions;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUser user)
        {
            var identity = await _userManager.FindByNameAsync(user.Login);
            if (identity == null) return BadRequest();
            var result = await _signInManager.CheckPasswordSignInAsync(identity, user.Password, false);
            if (result.Succeeded == false) return BadRequest();

            var claimsIdentity = GetIdentityClaims(identity);

            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: _authOptions.Issuer,
                audience: _authOptions.Audience,
                notBefore: now,
                claims: claimsIdentity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(_authOptions.Lifetime)),
                signingCredentials: new SigningCredentials(_authOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return Ok(token);
        }

        private ClaimsIdentity GetIdentityClaims(IdentityUser identity)
        {
            var role = _userManager.GetRolesAsync(identity).Result[0];
            var claims = new List<Claim>
            {
                new (ClaimsIdentity.DefaultNameClaimType, identity.UserName),
                new (ClaimsIdentity.DefaultRoleClaimType, role)
            };
            return new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUser user)
        {
            var identity = new IdentityUser { UserName = user.Login };
            var result = await _userManager.CreateAsync(identity, user.Password);
            if (result.Succeeded == false)
            {
                return BadRequest();
            }

            await _userManager.AddToRoleAsync(identity, "employee");
            return Ok();
        }
    }
}
