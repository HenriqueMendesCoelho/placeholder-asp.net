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
    public class HistoricController : ControllerBase
    {
        private readonly ILogger<HistoricController> _logger;
        private IHistoricService _service;
        private ITicketService _ticketService;
        private ITokenService _tokenService;
        private IUserService _userService;

        public HistoricController(ILogger<HistoricController> logger, IHistoricService service, ITicketService ticketService, ITokenService tokenService, IUserService userService)
        {
            _logger = logger;
            _service = service;
            _ticketService = ticketService;
            _tokenService = tokenService;
            _userService = userService;
        }



        /// <summary>
        /// Create Ticket historic - ADM and EMPLOYEE only
        /// </summary>
        [HttpPost("v1/{ticketId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public ActionResult CreateTicketHistoric(long ticketId, [FromBody] HistoricDTO obj)
        {
            //Getting user by jwt bearer token
            ClaimsPrincipal principal = _tokenService.GetPrincipal(HttpContext.Request.Headers["Authorization"].ToString().Substring(7));
            User user = _userService.FindByEmail(principal.Identity.Name);

            //Validation if user is admin or employee
            if (user.profile != Profiles.ProfilesEnum.ADMIN && user.profile != Profiles.ProfilesEnum.EMPLOYEE) return Forbid();

            Ticket ticket = _ticketService.FindByID(ticketId);
            if(ticket == null) return NotFound(new JsonReturnStandard().SingleReturnJsonError("Ticket not found"));

            if (user.profile == Profiles.ProfilesEnum.EMPLOYEE && Convert.ToInt64(ticket.Employee.Split("#")[1]) != user.Id)
                return Forbid("Only responsible of the ticket or ADM can create a historic");

            Historic historic = new();
            historic.TicketId = ticketId;
            historic.CreationDate = DateTime.Now;
            historic.CreateBy = user.FullName.Split(" ")[0] + "#" + user.Id;
            historic.Text = obj.Text;

            try
            {
                _service.Create(historic);
                return Ok(new JsonReturnStandard().SingleReturnJson("Historic created"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Problem("An error ocurred contact administrator");
            }
        }
    }
}
