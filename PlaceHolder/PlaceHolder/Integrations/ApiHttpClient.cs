namespace PlaceHolder.Integrations
{
    public class ApiHttpClient
    {
        public static HttpClient ApiClient { get; set; }

        public static void InicializeClient()
        {
            ApiClient = new HttpClient();
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
