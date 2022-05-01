using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Faculty.Common.Dto.LoginRegister;
using Microsoft.AspNetCore.Authorization;
using Faculty.AuthenticationServer.Services.Interfaces;

namespace Faculty.AuthenticationServer.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // POST api/auth/login
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(AuthUserDto authUserDto)
        {
            var (token, identityResult) = await _authService.GetJwtToken(authUserDto);
            if (!identityResult.Succeeded)
            {
                return BadRequest();
            }

            return Ok(new
            {
                jwtToken = token
            });
        }

        // POST api/auth/register
        [HttpPost("register")]
        public async Task<ActionResult> Register(AuthUserDto authUserDto)
        {
            var identityResult = await _authService.Register(authUserDto);
            if (!identityResult.Succeeded)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
