using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationService.Models;
using AuthenticationService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AuthenticationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly AuthServiceConfig _authServiceConfig;
        private readonly IAuthService _authService;

        public AuthController(IOptionsSnapshot<AuthServiceConfig> authServiceConfig
            , IAuthService authService) {
            this._authServiceConfig = authServiceConfig.Value;
            this._authService = authService;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel) {
            var userdetails = await _authService.Login(loginModel);
            if (userdetails == null)
            {
                return NotFound();
            }
            else {
                return Ok(userdetails);
            }
        }

        [HttpPost]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] UserDetails userDetails) {
            var modifiedUserDetails = await _authService.RefreshToken(userDetails);
            if (modifiedUserDetails == null)
            {
                return NotFound();
            }
            else {
                return Ok(modifiedUserDetails);
            }
        }

    }
}