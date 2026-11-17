using Lms_project.Core.Entities;
using Lms_project.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Lms_project.Core.DTOs.Auth;

namespace Lms_project.API_2_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController(IAuthService _service) : ControllerBase
    {
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto dto)
        {
            var response = await _service.LoginAsync(dto);
            return Ok(response);



        }
        [HttpPost("SignUp")]
        [AllowAnonymous]
        public async Task<ActionResult> RegisterAsStd(RegisterStdDto dto)
        {
            var response = await _service.RegisterAsStdAsync(dto);
            return Ok(new { Token = response.Token, expiresAt = response.Item2 });



        }
    }
}
