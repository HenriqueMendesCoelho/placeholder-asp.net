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


        //[Authorize(Roles ="admin")] - Deixar para apenas admin, porém não irei utilizar devido a limitações na arquitetura original do token JWT

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
        /// <remarks>
        /// Password regex > (?=.*[0,9])(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%]).{8,40} ||
        /// Email regex > none
        /// </remarks>
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
        /// Set privilege for a User - ADM ONLY
        /// </summary>
        /// <remarks>
        /// Posible values: **ADMIN**, **EMPLOYEE**, **USER**
        /// </remarks>
        [HttpPatch("v1/{id}/adm")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public ActionResult AddOrRemoveAdminPrivilege(long id, [FromBody] string privilege)
        {
            //Getting user by jwt bearer token
            ClaimsPrincipal principal = _tokenService.GetPrincipal(HttpContext.Request.Headers["Authorization"].ToString().Substring(7));
            User userLoged = _userService.FindByEmail(principal.Identity.Name);

            //Validation if user is admin
            if (userLoged.profile != Profiles.ProfilesEnum.ADMIN) return Forbid();

            User user = _userService.FindByID(id);

            if(user == null) return NotFound(new JsonReturnStandard().SingleReturnJsonError("User not found"));
            if (privilege != "ADMIN" && privilege != "EMPLOYEE" && privilege != "USER") return BadRequest(new JsonReturnStandard().SingleReturnJsonError("Role informed not exists"));

            if(privilege.Equals("ADMIN"))
            {
                if(user.profile == Profiles.ProfilesEnum.ADMIN) return Conflict("User already is an admin");
                user.profile = Profiles.ProfilesEnum.ADMIN;
                _userService.Update(user);

                return Ok(new JsonReturnStandard().SingleReturnJson("User is now an admin"));
            } else if(privilege == "EMPLOYEE")
            {
                if (user.profile == Profiles.ProfilesEnum.EMPLOYEE) return Conflict("User already is an admin");
                user.profile = Profiles.ProfilesEnum.EMPLOYEE;
                _userService.Update(user);

                return Ok(new JsonReturnStandard().SingleReturnJson("User is now an employee"));
            } else
            {
                if (user.profile == Profiles.ProfilesEnum.USER) return Conflict("User already is an USER");
                user.profile = Profiles.ProfilesEnum.USER;
                _userService.Update(user);

                return Ok(new JsonReturnStandard().SingleReturnJson("User is now an employee"));
            }
        }

        /// <summary>
        /// Return user by token
        /// </summary>
        [HttpGet("v1")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public User? SearchUserLogged()
        {
            ClaimsPrincipal principal = _tokenService.GetPrincipal(HttpContext.Request.Headers["Authorization"].ToString().Substring(7));
            return _userService.FindByEmail(principal.Identity.Name);
        }

        /// <summary>
        /// Change user password
        /// </summary>
        /// <remarks>
        /// Password regex > (?=.*[0,9])(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%]).{8,40}
        /// </remarks>
        [HttpPatch("v1/change")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public ActionResult ChangeUserPassword(ChangePasswordUserDTO obj)
        {
            //Getting user by jwt bearer token
            ClaimsPrincipal principal = _tokenService.GetPrincipal(HttpContext.Request.Headers["Authorization"].ToString().Substring(7));
            User userLoged = _userService.FindByEmail(principal.Identity.Name);

            if(!_userService.ValidateCredencials(userLoged.Email, obj.CurrentPassword)) 
                return BadRequest(new JsonReturnStandard().SingleReturnJsonError("Incorrect password"));

            if (!obj.NewPassword.Equals(obj.ConfirmNewPassword)) 
                return BadRequest(new JsonReturnStandard()
                    .SingleReturnJsonError("Confirmed password and new password must be the same"));

            userLoged.Password = _userService.EncryptPassword(obj.NewPassword);

            try
            {
                _userService.Update(userLoged);
                return Ok(new JsonReturnStandard().SingleReturnJson("Password changed"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Problem("An error ocurred contact administrator");
            }
        }
    }
}
