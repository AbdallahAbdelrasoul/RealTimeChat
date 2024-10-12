using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealTimeChat.Application.Users;
using RealTimeChat.Application.Users.DTOs;

namespace RealTimeChat.Presentation.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _userService;

        public UsersController(IUsersService userService)
        {
            _userService = userService;
        }

        [HttpPost("[Action]"), AllowAnonymous]
        public async Task<ActionResult<UserDTO>> Register(UserRegisterDTO request)
        {
            return Ok(await _userService.Register(request));
        }

        [HttpPost("[Action]"), AllowAnonymous]
        public async Task<ActionResult<UserLoginOutputDTO>> Login(UserLoginDTO request)
        {
            return Ok(await _userService.Login(request));
        }

        [HttpPost("[Action]"), AllowAnonymous]
        public async Task<ActionResult> VerifyEmail(UserValidateEmailDTO request)
        {
            await _userService.VerifyEmail(request);
            return Ok("Your Email Is Verified Successfully");
        }


    }
}
