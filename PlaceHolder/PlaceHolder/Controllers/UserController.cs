using Microsoft.AspNetCore.Authorization;
using PlaceHolder.DTOs;
using PlaceHolder.Security;
using System.Security.Claims;

namespace PlaceHolder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private IUserService _userService;
        private ITokenService _tokenService;

        public UserController(ILogger<UserController> logger, IUserService userService, ITokenService tokenService)
        {
            _logger = logger;
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpGet("v1/{id}")]
        //[Authorize(Roles ="admin")] - Deixar para apenas admin, porém o
        public ActionResult<User?> SearchUser(long id)
        {
            //Getting user by jwt bearer token
            ClaimsPrincipal principal = _tokenService.GetPrincipal(HttpContext.Request.Headers["Authorization"].ToString().Substring(7));
            string identity = principal.Identity.Name;

            Console.WriteLine(identity);

            //Validate if user is admin
            var user = _userService.FindByEmail(identity);
            if (user.Role != "admin") return Forbid();

            if(user == null) return NotFound("User not found");
            return Ok(user);
        }

        [HttpPost("v1")]
        [AllowAnonymous]
        public ActionResult<User?> CreateUser(UserDTO obj)
        {
            _userService.Create(obj);

            return Ok(obj);
        }

        [HttpPut("v1")]
        public ActionResult<User?> Update(User user)
        {
            if(user == null) return BadRequest("User can not be null");

            return _userService.Update(user);
        }
    }
}
