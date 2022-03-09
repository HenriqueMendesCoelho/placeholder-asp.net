namespace Backlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        [HttpGet("v1/{id}")]
        public Ticket Get(long id)
        {
            return new Ticket();
        }
    }
}
