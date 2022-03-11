using PlaceHolder.DTOs;

namespace PlaceHolder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet("v1/{id}")]
        public ActionResult<User?> SearchUser(long id)
        {
            var user = _userService.FindByID(id);

            if(user == null) return NotFound("User not found");
            return Ok(user);
        }

        [HttpPost("v1")]
        public ActionResult<User?> CreateUser(UserDTO obj)
        {
            return Ok(_userService.Create(obj));
        }

        [HttpPut("v1")]
        public ActionResult<User?> Update(User user)
        {
            if(user == null) return BadRequest("User can not be null");

            return _userService.Update(user);
        }
    }
}
