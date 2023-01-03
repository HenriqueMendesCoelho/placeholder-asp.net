
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
        [ProducesResponseType(200, Type = typeof(TokenDTO))]
        [ProducesResponseType(401)]
        public ActionResult<TokenDTO?> Login([FromBody] UserLoginDTO obj)
        {
            if(obj == null) return Unauthorized("Email or password incorrect");

            TokenDTO token = _authService.Login(obj);
            Console.WriteLine("Ola");

            return (token != null) ? Ok(token) : Unauthorized("Email or password incorrect");
        }

        /// <summary>
        /// RefreshToken
        /// </summary>
        [HttpPost("refresh")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public ActionResult<TokenDTO?> RefreshToken(RefreshTokenDTO obj)
        {
            if (string.IsNullOrEmpty(obj.RefreshToken) || string.IsNullOrEmpty(obj.AccessToken)) 
                return BadRequest(new JsonReturnStandard()
                    .SingleReturnJsonError("Access token and refresh token can not be null or empty"));

            TokenDTO token = _authService.RefreshToken(obj);

            return (token != null) ? Ok(token) : Unauthorized(new JsonReturnStandard()
                    .SingleReturnJsonError("Invalid access token or refresh token"));
            
        }
    }
}
