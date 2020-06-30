using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Sypht
{
    class OAuthHelper
    {
        private const string SYPHT_AUTH_ENDPOINT = "https://auth.sypht.com/oauth2/token";

        private String clientId = null;
        private String clientSecret = null;

        private HttpClient httpClient = new HttpClient();

        public OAuthHelper()
        {
            var syphtApiKey = Environment.GetEnvironmentVariable("SYPHT_API_KEY");
            if (syphtApiKey != null)
            {
                clientId = syphtApiKey.Split(":")[0];
                clientSecret = syphtApiKey.Split(":")[1];
            }
        }

        private string AuthenticationEndpoint {
            get {
                string value = Environment.GetEnvironmentVariable("SYPHT_AUTH_ENDPOINT");
                if (string.IsNullOrEmpty(value))
                {
                    return SYPHT_AUTH_ENDPOINT;
                }
                return value;
            }
        }

        private string base64Encode(string value)
        {
            byte[] data = System.Text.ASCIIEncoding.ASCII.GetBytes(value);
            return System.Convert.ToBase64String(data);
        }

        private async Task<string> authAuth0()
        {
            httpClient.BaseAddress = new Uri("https://login.sypht.com");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            
            var content = new StringContent("{" +
                "\"client_id\":\"" + clientId + "\"," +
                "\"client_secret\":\"" + clientSecret + "\"," +
                "\"audience\":\"https://api.sypht.com\"," +
                "\"grant_type\":\"client_credentials\"" +	
                "}", Encoding.UTF8, "application/json");
            
            var response = await this.httpClient.PostAsync("/oauth/token", content);
            response.EnsureSuccessStatusCode();
            var auth0Response = await response.Content.ReadAsStringAsync();
            return JObject.Parse(auth0Response)["access_token"].ToObject<string>();
        }

        private async Task<string> authCognito()
        {
            httpClient.BaseAddress = new Uri("https://auth.sypht.com");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Encode($"{clientId}:{clientSecret}"));
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var dict = new Dictionary<string, string>();
            dict.Add("client_id", clientId);
            dict.Add("grant_type", "client_credentials");

            var response = await this.httpClient.PostAsync("/oauth2/token", new FormUrlEncodedContent(dict));
            response.EnsureSuccessStatusCode();
            var auth0Response = await response.Content.ReadAsStringAsync();
            return JObject.Parse(auth0Response)["access_token"].ToObject<string>();
        }

        public async Task<string> login()
        {
            if (AuthenticationEndpoint.Contains("/oauth2/token"))
            {
                return await authCognito();
            } else {
                return await authAuth0();
            }
        }
    }
}
