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
    /// <summary>
    /// Authentication service.
    /// </summary>
    public class AuthService : IAuthService
    {
        /// <summary>
        /// User manager for executing operation on identity user.
        /// </summary>
        private readonly UserManager<CustomUser> _userManager;
        /// <summary>
        /// Sign in manager for checking user password.
        /// </summary>
        private readonly SignInManager<CustomUser> _signInManager;
        /// <summary>
        /// Auth options.
        /// </summary>
        private readonly AuthOptions _authOptions;

        /// <summary>
        /// Constructor for init identity means.
        /// </summary>
        /// <param name="userManager">User manager.</param>
        /// <param name="signInManager">Sing in manager.</param>
        /// <param name="authOptions">Auth options.</param>
        public AuthService(UserManager<CustomUser> userManager, SignInManager<CustomUser> signInManager, AuthOptions authOptions)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authOptions = authOptions;
        }

        /// <summary>
        /// Method for login user.
        /// </summary>
        /// <param name="authUserDto">Model user for login.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
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

        /// <summary>
        /// Method generated claims identity.
        /// </summary>
        /// <param name="customUser">Custom identity user.</param>
        /// <returns>An instance of the ClaimsIdentity class.</returns>
        private ClaimsIdentity GetIdentityClaims(CustomUser customUser)
        {
            var roles = _userManager.GetRolesAsync(customUser).Result;
            var claims = new List<Claim>
            {
                new (ClaimsIdentity.DefaultNameClaimType, customUser.UserName)
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));
            return new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme, ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }

        /// <summary>
        /// Method generated jwt token.
        /// </summary>
        /// <param name="claimsIdentity">Claims identity.</param>
        /// <returns>Jwt token.</returns>
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

        /// <summary>
        /// Method for register user.
        /// </summary>
        /// <param name="authUserDto">Model user for register.</param>
        /// <returns>An instance of the Task class typed by HttpResponseMessage class.</returns>
        public async Task<IdentityResult> Register(AuthUserDto authUserDto)
        {
            var identity = new CustomUser { UserName = authUserDto.Login, Birthday = DateTime.Now.Date };
            var result = await _userManager.CreateAsync(identity, authUserDto.Password);
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
