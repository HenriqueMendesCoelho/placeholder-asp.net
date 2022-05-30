using Microsoft.AspNetCore.Authorization;
using PlaceHolder.DTOs;
using PlaceHolder.Exceptions;
using PlaceHolder.Integrations.ViaCEP;
using PlaceHolder.Integrations.ViaCEP.Model;
using PlaceHolder.Methods;
using PlaceHolder.Security;
using System.Security.Claims;

namespace PlaceHolder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class UserAddressController : ControllerBase
    {
        private readonly ILogger<UserAddressController> _logger;
        private IUserAddressService _service;
        private ITokenService _tokenService;
        private IUserService _userService;

        public UserAddressController(ILogger<UserAddressController> logger, IUserAddressService service, ITokenService tokenService, IUserService userService)
        {
            _logger = logger;
            _service = service;
            _tokenService = tokenService;
            _userService = userService;
        }

        /// <summary>
        /// Create an UserAddress
        /// </summary>
        /// <remarks>
        /// State, City, District, Street is **NOT** mandatory, it will only be used if in the integration with viaCEP it comes blank.
        /// </remarks>
        [HttpPost("v1")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult CreateUserAddress(AddressDTO obj)
        {
            //Getting user by jwt bearer token
            ClaimsPrincipal principal = _tokenService.GetPrincipal(HttpContext.Request.Headers["Authorization"].ToString().Substring(7));
            User userLoged = _userService.FindByEmail(principal.Identity.Name);

            if (userLoged == null) return NotFound(new JsonReturnStandard().SingleReturnJsonError("User not found"));
            if (userLoged.Address != null) return BadRequest(new JsonReturnStandard().SingleReturnJsonError("User already contains a Address"));

            ViaCEPResponse response;
            try
            {
                ViaCEPIntegration viaCEP = new();
                response = viaCEP.ValidateCEPWrap(Convert.ToString(obj.Cep));
            }
            catch (CepNotFoundException e)
            {
                return NotFound(new JsonReturnStandard().SingleReturnJsonError(e.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Problem("An error ocurred contact administrator");
            }

            UserAddress userAddress = _service.ExtractAddresFromDTO(obj, userLoged.Id, response);

            try
            {
                _service.Create(userAddress);
                return Ok(userAddress);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Problem("An error ocurred contact administrator");
            }
        }

        /// <summary>
        /// Update an UserAddress
        /// </summary>
        /// <remarks>
        /// State, City, District, Street is **NOT** mandatory, it will only be used if in the integration with viaCEP it comes blank
        /// </remarks>
        [HttpPut("v1")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult UpdateUserAddress(AddressDTO obj)
        {
            //Getting user by jwt bearer token
            ClaimsPrincipal principal = _tokenService.GetPrincipal(HttpContext.Request.Headers["Authorization"].ToString().Substring(7));
            User userLoged = _userService.FindByEmail(principal.Identity.Name);

            UserAddress userAddress = _service.FindByID(userLoged.Id);

            if (userAddress == null) return NotFound(new JsonReturnStandard().SingleReturnJsonError("Address of the user logged not found"));

            ViaCEPIntegration viaCEP = new();
            ViaCEPResponse response;
            
            try
            {
                response = viaCEP.ValidateCEPWrap(Convert.ToString(obj.Cep));
            }
            catch (CepNotFoundException e)
            {
                _logger.LogError(e.ToString());
                return NotFound(new JsonReturnStandard().SingleReturnJsonError(e.Message));
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return Problem("An error ocurred contact administrator");
            }

            UserAddress userAddressNew = _service.ExtractAddresFromDTO(obj, userLoged.Id, response);

            try
            {
                _service.Update(userAddressNew);
                return Ok(userAddressNew);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Problem("An error ocurred contact administrator");
            }
        }

        /// <summary>
        /// Delete an UserAddress - ADM only
        /// </summary>
        [HttpDelete("v1/{id}/adm")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult DeleteUserAddress(long id)
        {
            //Getting user by jwt bearer token
            ClaimsPrincipal principal = _tokenService.GetPrincipal(HttpContext.Request.Headers["Authorization"].ToString().Substring(7));
            User userLoged = _userService.FindByEmail(principal.Identity.Name);

            //Validation if user is admin
            if (userLoged.profile != Profiles.ProfilesEnum.ADMIN) return Forbid("Forbidden");

            UserAddress userAddress = _service.FindByID(id);
            if(userAddress == null) return NotFound(new JsonReturnStandard().SingleReturnJsonError("Address not found"));

            try
            {
                _service.Delete(userAddress.Id);
                return Ok(new JsonReturnStandard().SingleReturnJson("Address deleted"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Problem("An error ocurred contact administrator");
            }
        }
    }
}
