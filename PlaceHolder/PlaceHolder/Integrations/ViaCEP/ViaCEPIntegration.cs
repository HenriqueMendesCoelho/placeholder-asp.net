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

        public async Task<ViaCEPResponse?> ValidationCEP(string cep)
        {
            string Url = $"https://viacep.com.br/ws/{ cep }/json/";

            try
            {
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
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }

            return new ViaCEPResponse();
        }
    }
}
