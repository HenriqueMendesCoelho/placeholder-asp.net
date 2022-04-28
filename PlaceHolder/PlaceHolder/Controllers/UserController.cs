using Microsoft.AspNetCore.Authorization;
using PlaceHolder.DTOs;
using PlaceHolder.Methods;
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
        /// Search user by id passing in the path URL - ADM Or EMPLOYEE ONLY
        /// </summary>
        [HttpGet("v1/{id}/adm")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<User?> SearchUser(long id)
        {
            //Getting user by jwt bearer token
            ClaimsPrincipal principal = _tokenService.GetPrincipal(HttpContext.Request.Headers["Authorization"].ToString().Substring(7));
            User user = _userService.FindByEmail(principal.Identity.Name);

            //Validation if user is admin or employee
            if (user.profile != Profiles.ProfilesEnum.ADMIN && user.profile != Profiles.ProfilesEnum.EMPLOYEE) return Forbid();

            User search = _userService.FindByID(id);
            if (search == null) return NotFound(new JsonReturnStandard().SingleReturnJsonError("User not found"));

            return Ok(search);
        }

        /// <summary>
        /// Search ALL user in base return a list of users - ADM Or EMPLOYEE ONLY
        /// </summary>
        [HttpGet("v1/list")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public ActionResult<List<User>> ListUsers()
        {
            //Getting user by jwt bearer token
            ClaimsPrincipal principal = _tokenService.GetPrincipal(HttpContext.Request.Headers["Authorization"].ToString().Substring(7));
            User user = _userService.FindByEmail(principal.Identity.Name);

            //Validation if user is admin or employee
            if (user.profile != Profiles.ProfilesEnum.ADMIN && user.profile != Profiles.ProfilesEnum.EMPLOYEE) return Forbid();

            return _userService.FindAll();
        }

        /// <summary>
        /// Create a user bearer token not required
        /// </summary>
        [HttpPost("v1")]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(500)]
        public ActionResult<User?> CreateUser(UserDTO obj)
        {
            _userService.Create(obj);

            return Ok(obj);
        }

        /// <summary>
        /// Update a user
        /// </summary>
        [HttpPut("v1")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public ActionResult<User?> Update(User user)
        {
            if(user == null) return BadRequest(new JsonReturnStandard().SingleReturnJsonError("User can not be null"));

            return _userService.Update(user);
        }

        /// <summary>
        /// DELETE user by e-mail - ADM ONLY
        /// </summary>
        [HttpDelete("v1/{email}/adm")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult DeleteUser (string email)
        {
            //Getting user by jwt bearer token
            ClaimsPrincipal principal = _tokenService.GetPrincipal(HttpContext.Request.Headers["Authorization"].ToString().Substring(7));
            User user = _userService.FindByEmail(principal.Identity.Name);

            //Validation if user is admin
            if (user.profile != Profiles.ProfilesEnum.ADMIN) return Forbid();

            if (email == null) return BadRequest(new JsonReturnStandard().SingleReturnJsonError("Email can't be null"));

            try
            {
                _userService.Delete(email);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return Problem("An error ocurred contact administrator");
            }

            return Ok(new JsonReturnStandard().SingleReturnJson("User deleted"));
        }

        /// <summary>
        /// Add or remove admin privilege - ADM ONLY
        /// </summary>
        [HttpPost("v1/{id}/adm")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult AddOrRemoveAdminPrivilege(long id)
        {
            //Getting user by jwt bearer token
            ClaimsPrincipal principal = _tokenService.GetPrincipal(HttpContext.Request.Headers["Authorization"].ToString().Substring(7));
            User userLoged = _userService.FindByEmail(principal.Identity.Name);

            //Validation if user is admin
            if (userLoged.profile != Profiles.ProfilesEnum.ADMIN) return Forbid();

            User user = _userService.FindByID(id);

            if(user == null) return NotFound(new JsonReturnStandard().SingleReturnJsonError("User not found"));

            if(user.profile == Profiles.ProfilesEnum.ADMIN)
            {
                userLoged.profile = Profiles.ProfilesEnum.USER;
                return Ok(new JsonReturnStandard().SingleReturnJson("User is no longer an admin"));
            }
            else
            {
                userLoged.profile = Profiles.ProfilesEnum.ADMIN;
                return Ok(new JsonReturnStandard().SingleReturnJson("User is now admin"));
            }
        }
    }
}
