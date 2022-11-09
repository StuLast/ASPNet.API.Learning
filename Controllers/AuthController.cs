
using Microsoft.AspNetCore.Mvc;

using dotnet.rpg.Services.AuthRepository;
using dotnet.rpg.Dtos.User;

namespace dotnet.rpg.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
        _authRepository = authRepository;
        }
    
        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<Int16>>> Register(UserRegisterDto request) 
        {
            var response = await _authRepository.Register(
                new User { UserName = request.Username},
                request.Password
            );

            if(!response.Success){
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPostAttribute("Login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLoginDto request)
        {
            var response = await _authRepository.Login(
                request.Username, 
                request.Password
            );

            if(!response.Success){
                return BadRequest(response);
            }

            return Ok(response);
        }

    }
}