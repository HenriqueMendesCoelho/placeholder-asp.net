using Backlog.Methods;
using Backlog.DTOs;

namespace Backlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext context;

        public UserController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet("{id}/v1")]
        public async Task<ActionResult<User>> SearchUser(long id)
        {
            var user = await context.Users.FindAsync(id);
            if (user == null)
                BadRequest(new JsonReturnStandard().SingleReturnJsonError("User not found"));
            return Ok(user);
        }

        [DisableCors]
        [Route("v1")]
        [HttpPost]
        public object CreateUser(CreateUserDTO obj)
        {
            User u = new User
            {
                Name = obj.Name,
                Email = obj.Email,
                Password = obj.Password,
                CpfOrCnpj = obj.CpfOrCnpj,
                BackupEmail = obj.BackupEmail,
                CreatedDate = DateTime.Now,
                BithDate = obj.BithDate,
                Age = obj.Age
            };
            try
            {
                context.Users.Add(u);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Conflict("Error");
            }

            return Ok(new JsonReturnStandard().SingleReturnJson("User created"));
        }
    }
}
