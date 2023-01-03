using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlaceHolder.Exceptions;
using PlaceHolder.Integrations.ViaCEP;
using PlaceHolder.Integrations.ViaCEP.Model;
using PlaceHolder.Methods;

namespace PlaceHolder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class CepController : ControllerBase
    {
        private readonly ILogger<CepController> _logger;

        public CepController(ILogger<CepController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Search CEP
        /// </summary>
        /// <remarks>
        /// Mandatory token
        /// </remarks>
        [HttpGet("v1/{cep}")]
        [ProducesResponseType(200, Type = typeof(ViaCEPResponse))]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult GetCep(string cep)
        {

            try
            {
                ViaCEPIntegration viaCEPIntegration = new();
                ViaCEPResponse viaCEP = viaCEPIntegration.ValidateCEPWrap(cep);

                Dictionary<string, string> response = new Dictionary<string, string>();
                response.Add("Cep", viaCEP.Cep);
                response.Add("Street", viaCEP.Logradouro);
                response.Add("District", viaCEP.Bairro);
                response.Add("City", viaCEP.Localidade);
                response.Add("State", viaCEP.Uf);
                return Ok(response);
            }
            catch (CepNotFoundException e)
            {
                _logger.LogError(e.ToString());
                return NotFound(new JsonReturnStandard().SingleReturnJsonError(e.Message));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return Problem("An error ocurred contact administrator");
            }
        }
    }
}
