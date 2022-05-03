namespace PlaceHolder.Integrations.ViaCEP.Model
{
    public class ViaCEPResponse
    {
        public string Cep { get; set; }

        public string Logradouro { get; set; }

        public string Bairro { get; set; }

        public string Localidade { get; set; }

        public string Uf { get; set; }

        public ViaCEPResponse() {   }

        public ViaCEPResponse(string cep, string logradouro, string bairro, string localidade, string uf)
        {
            Cep = cep;
            Logradouro = logradouro;
            Bairro = bairro;
            Localidade = localidade;
            Uf = uf;
        }
    }
}
