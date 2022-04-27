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
    public class TicketController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private ITicketService _ticketService;
        private ITokenService _tokenService;
        private IUserService _userService;

        public TicketController(ILogger<UserController> logger, ITicketService ticketService, ITokenService tokenService, IUserService userService)
        {
            _logger = logger;
            _ticketService = ticketService;
            _tokenService = tokenService;
            _userService = userService;
        }

        /// <summary>
        /// Get ticket by id - ADM ONLY
        /// </summary>
        [HttpGet("v1/{id}/adm")]
        [ProducesResponseType(200, Type = typeof(Ticket))]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404, Type = typeof(Dictionary<string, object>))]
        [ProducesResponseType(500)]
        public ActionResult<Ticket> Get(long id)
        {
            //Getting user by jwt bearer token
            ClaimsPrincipal principal = _tokenService.GetPrincipal(HttpContext.Request.Headers["Authorization"].ToString().Substring(7));
            User user = _userService.FindByEmail(principal.Identity.Name);

            //Validation if user is admin
            if (user.profile != Profiles.ProfilesEnum.ADMIN) return Forbid();

            Ticket ticket = _ticketService.FindByID(id);

            if(ticket == null) return NotFound(new JsonReturnStandard().SingleReturnJsonError("Ticket not found"));

            return ticket;
        }

        /// <summary>
        /// Create a ticket user NON ADM
        /// </summary>
        [HttpPost("v1")]
        [ProducesResponseType(200, Type = typeof(Ticket))]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public ActionResult<Ticket> CreateTicketUserNonADM(TicketCreateByUserDTO obj)
        {
            //Getting user by jwt bearer token
            ClaimsPrincipal principal = _tokenService.GetPrincipal(HttpContext.Request.Headers["Authorization"].ToString().Substring(7));
            User user = _userService.FindByEmail(principal.Identity.Name);
            Ticket ticket;

            try
            {
                ticket = _ticketService.CreateTicketByUser(obj, user);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return Problem("An error ocurred contact administrator");
            }
            return (ticket != null) ? Ok(ticket) : Problem("An error ocurred contact administrator");
        }

        /// <summary>
        /// Employee loged takes responsibility for ticket - ADM Only
        /// </summary>
        [HttpPatch("v1/{id}/adm")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public ActionResult TakeTicket(long id)
        {
            //Getting user by jwt bearer token
            ClaimsPrincipal principal = _tokenService.GetPrincipal(HttpContext.Request.Headers["Authorization"].ToString().Substring(7));
            User user = _userService.FindByEmail(principal.Identity.Name);

            //Validation if user is admin
            if (user.profile != Profiles.ProfilesEnum.ADMIN) return Forbid();

            Ticket ticket = _ticketService.FindByID(id);

            if(ticket == null) return NotFound(new JsonReturnStandard().SingleReturnJsonError("Ticket not found"));
            if (!string.IsNullOrEmpty(ticket.Employee)) return Conflict(new JsonReturnStandard().SingleReturnJsonError("Ticket is already taken by another employee"));

            ticket.Employee = user.FullName.Split(" ")[0] + "#" + user.Id;

            try
            {
                _ticketService.Update(ticket);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return Problem("An error ocurred contact administrator");
            }

            return Ok(ticket);
        }

        /// <summary>
        /// Transfer ticker to another ADM - ADM Only
        /// </summary>
        /// <remarks>
        /// The body must be json with just **"email@email.com"** with the **quotes**, but it must be a **JSON** and not text
        /// Ex:
        /// "email@email.com"
        /// </remarks>
        [HttpPatch("v1/transfer/{id}/adm")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(406)]
        [ProducesResponseType(500)]
        public ActionResult TransferTicket(long id, [FromBody] string email)
        {
            //Getting user by jwt bearer token
            ClaimsPrincipal principal = _tokenService.GetPrincipal(HttpContext.Request.Headers["Authorization"].ToString().Substring(7));
            User user = _userService.FindByEmail(principal.Identity.Name);

            //Validation if user is admin
            if (user.profile != Profiles.ProfilesEnum.ADMIN) return Forbid();

            if (user.Email.Equals(email, StringComparison.OrdinalIgnoreCase)) return StatusCode(StatusCodes.Status406NotAcceptable, 
                new JsonReturnStandard().SingleReturnJsonError("The ticker cannot be transferred to itself"));

            User user_tranfer = _userService.FindByEmail(email);

            if (user_tranfer == null) return NotFound(new JsonReturnStandard().SingleReturnJsonError("User not found"));
            if (user_tranfer.profile != Profiles.ProfilesEnum.ADMIN) return BadRequest(new JsonReturnStandard().SingleReturnJsonError("User to transfer have to be an ADM"));

            Ticket ticket = _ticketService.FindByID(id);
            if(ticket == null) return NotFound(new JsonReturnStandard().SingleReturnJsonError("Ticket not found"));
            if (Convert.ToInt64(ticket.Employee.Split("#")[1]) != user.Id) return StatusCode(StatusCodes.Status406NotAcceptable, 
                new JsonReturnStandard().SingleReturnJsonError("The ticket can only be transferred by the responsible employee"));

            ticket.Employee = user_tranfer.FullName.Split(" ")[0] + "#" + user_tranfer.Id;
            Ticket ticketUpdated;

            try
            {
                ticketUpdated = _ticketService.Update(ticket);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return Problem("An error ocurred contact administrator");
            }

            return Ok(ticketUpdated);
        }
    }
}
