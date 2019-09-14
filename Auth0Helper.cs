using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace sypht_csharp_client
{
    class Auth0Helper
    {
        private static String AUTH0_URL = "https://login.sypht.com";
        
        private String clientId = null;
        private String clientSecret = null;

        private HttpClient httpClient = new HttpClient();

        public Auth0Helper()
        {
            var syphtApiKey = Environment.GetEnvironmentVariable("SYPHT_API_KEY");
            if (syphtApiKey != null)
            {
                clientId = syphtApiKey.Split(":")[0];
                clientSecret = syphtApiKey.Split(":")[1];
            }
        }

        public async Task<string> login()
        {
            httpClient.BaseAddress = new Uri(AUTH0_URL);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            
            var content = new StringContent("{" +
            "\"client_id\":\"" + clientId + "\"," +
            "\"client_secret\":\"" + clientSecret + "\"," +
            "\"audience\":\"https://api.sypht.com\"," +
            "\"grant_type\":\"client_credentials\"" +	
            "}", Encoding.UTF8, "application/json");
            var result = await httpClient.PostAsync("/oauth/token", content);
            return await result.Content.ReadAsStringAsync();
        }
    }
}