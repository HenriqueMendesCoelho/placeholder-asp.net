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


        //[Authorize(Roles ="admin")] - Deixar para apenas admin, porém o

        /// <summary>
        /// Search user by id passing in the path URL - ADM ONLY
        /// </summary>
        [HttpGet("v1/{id}")]
        public ActionResult<User?> SearchUser(long id)
        {
            //Getting user by jwt bearer token
            ClaimsPrincipal principal = _tokenService.GetPrincipal(HttpContext.Request.Headers["Authorization"].ToString().Substring(7));
            User user = _userService.FindByEmail(principal.Identity.Name);

            //Validation if user is admin
            if (user.profile != Profiles.ProfilesEnum.ADMIN) return Forbid();

            User search = _userService.FindByID(id);

            if (search == null) return NotFound("User not found");
            return Ok(user);
        }

        /// <summary>
        /// Search ALL user in base return a list of users
        /// </summary>
        [HttpGet("v1/list")]
        public ActionResult<List<User>> ListUsers()
        {
            return _userService.FindAll();
        }

        /// <summary>
        /// Create a user
        /// </summary>
        [HttpPost("v1")]
        [AllowAnonymous]
        public ActionResult<User?> CreateUser(UserDTO obj)
        {
            _userService.Create(obj);

            return Ok(obj);
        }

        /// <summary>
        /// Update a user
        /// </summary>
        [HttpPut("v1")]
        public ActionResult<User?> Update(User user)
        {
            if(user == null) return BadRequest("User can not be null");

            return _userService.Update(user);
        }

        /// <summary>
        /// DELETE user by e-mail - ADM ONLY
        /// </summary>
        [HttpDelete("v1/{email}")]
        public ActionResult DeleteUser (string email)
        {
            if(email == null) return BadRequest("Email can't be null");

            try
            {
                _userService.Delete(email);
            }
            catch (Exception)
            {

                return BadRequest("User not found");
            }

            return Ok("User deleted");
        }
    }
}
