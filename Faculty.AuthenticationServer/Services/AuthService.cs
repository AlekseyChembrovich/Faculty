using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Faculty.Common.Dto.LoginRegister;
using Faculty.AuthenticationServer.Tools;
using Faculty.AuthenticationServer.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Faculty.AuthenticationServer.Services.Interfaces;

namespace Faculty.AuthenticationServer.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<CustomUser> _userManager;
        private readonly SignInManager<CustomUser> _signInManager;
        private readonly AuthOptions _authOptions;

        public AuthService(UserManager<CustomUser> userManager, SignInManager<CustomUser> signInManager, AuthOptions authOptions)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authOptions = authOptions;
        }

        public async Task<Tuple<string, IdentityResult>> GetJwtToken(AuthUserDto authUserDto)
        {
            var customUser = await _userManager.FindByNameAsync(authUserDto.Login);
            if (customUser == null)
            {
                return new Tuple<string, IdentityResult>(default, IdentityResult.Failed());
            }

            var result = await _signInManager.CheckPasswordSignInAsync(customUser, authUserDto.Password, false);
            if (!result.Succeeded)
            {
                return new Tuple<string, IdentityResult>(default, IdentityResult.Failed());
            }

            var claimsIdentity = GetIdentityClaims(customUser);
            var token = GenerateJwtToken(claimsIdentity);
            return new Tuple<string, IdentityResult>(token, IdentityResult.Success);
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

        private string GenerateJwtToken(ClaimsIdentity claimsIdentity)
        {
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: _authOptions.Issuer,
                audience: _authOptions.Audience,
                notBefore: now,
                claims: claimsIdentity.Claims,
                expires: now.Add(TimeSpan.FromDays(_authOptions.Lifetime)),
                signingCredentials: new SigningCredentials(_authOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return token;
        }

        public async Task<IdentityResult> Register(AuthUserDto authUser)
        {
            var identity = new CustomUser { UserName = authUser.Login, Birthday = DateTime.Now.Date };
            var result = await _userManager.CreateAsync(identity, authUser.Password);
            if (!result.Succeeded)
            {
                return IdentityResult.Failed();
            }

            const string defaultRole = "employee";
            await _userManager.AddToRoleAsync(identity, defaultRole);
            return IdentityResult.Success;
        }
    }
}
