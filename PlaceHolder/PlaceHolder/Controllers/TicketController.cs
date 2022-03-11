namespace PlaceHolder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private ITicketService _ticketService;

        public TicketController(ILogger<UserController> logger, ITicketService ticketService)
        {
            _logger = logger;
            _ticketService = ticketService;
        }

        [HttpGet("v1/{id}")]
        public Ticket Get(long id)
        {
            return new Ticket();
        }
    }
}
