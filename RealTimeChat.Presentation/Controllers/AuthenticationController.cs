using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealTimeChat.Application.AuthenticationService;
using RealTimeChat.Application.AuthenticationService.IO;

namespace RealTimeChat.Presentation.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("SignUp"), AllowAnonymous]
        public async Task<ActionResult<SignUpOutput>> SignUp(SignUpInput input)
        {
            return Ok(await _authenticationService.SignUp(input));
        }

    }
}
