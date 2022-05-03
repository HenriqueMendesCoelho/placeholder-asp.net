
using PlaceHolder.DTOs;
using PlaceHolder.Methods;

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

        /// <summary>
        /// Login generate JWT token
        /// </summary>
        [HttpPost("login")]
        public ActionResult<TokenDTO?> Login([FromBody] UserLoginDTO obj)
        {
            if(obj == null) return Unauthorized("Email or password incorrect");

            TokenDTO token = _authService.Login(obj);

            return (token != null) ? Ok(token) : Unauthorized("Email or password incorrect");
        }

        /// <summary>
        /// RefreshToken
        /// </summary>
        [HttpPost("refresh")]
        public ActionResult<TokenDTO?> RefreshToken(RefreshTokenDTO obj)
        {
            if (string.IsNullOrEmpty(obj.RefreshToken) || string.IsNullOrEmpty(obj.AccessToken)) 
                return BadRequest(new JsonReturnStandard()
                    .SingleReturnJsonError("Access token and refresh token can not be null or empty"));

            TokenDTO token = _authService.RefreshToken(obj);

            return (token != null) ? Ok(token) : Forbid("Invalid access token or refresh token");
        }
    }
}
