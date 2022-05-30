using PlaceHolder.Exceptions;
using PlaceHolder.Integrations.ViaCEP.Model;

namespace PlaceHolder.Integrations.ViaCEP
{
    public class ViaCEPIntegration
    {
        private readonly ILogger<ViaCEPIntegration> _logger;

        public ViaCEPIntegration()
        {
        }

        public ViaCEPIntegration(ILogger<ViaCEPIntegration> logger)
        {
            _logger = logger;
        }

        private async Task<ViaCEPResponse> ValidateCEPCallViaCEP(string cep)
        {
            string Url = $"https://viacep.com.br/ws/{ cep }/json/";

            
            using(HttpResponseMessage response = await ApiHttpClient.ApiClient.GetAsync(Url))
            {
                if(response.IsSuccessStatusCode)
                {
                    ViaCEPResponse responseModel = await response.Content.ReadFromJsonAsync<ViaCEPResponse>();

                    return responseModel;
                } else
                {
                    throw new ApiInternalException(response.ReasonPhrase);
                }
            }
        }

        public ViaCEPResponse ValidateCEPWrap(string cep)
        {
            var result = ValidateCEPCallViaCEP(cep);

            return (result.Result.Cep != null) ? result.Result : throw new CepNotFoundException();
        }
    }
}
