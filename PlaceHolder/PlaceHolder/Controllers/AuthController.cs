
using PlaceHolder.DTOs;

namespace PlaceHolder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult login([FromBody] UserLoginDTO obj)
        {
            if(obj == null) return Unauthorized("Email or password incorrect");

            var token = _authService.ValidateCredencials(obj);

            return (token != null) ? Ok(token) : Unauthorized("Email or password incorrect");
        }
    }
}
