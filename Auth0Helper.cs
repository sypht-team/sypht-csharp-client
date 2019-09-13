using System;
using System.Net.Http

namespace sypht_csharp_client {
    class Auth0Helper
        {
        private String clientId = null;
        private String clientSecret = null;
        static String AUTH0_URL = "";
        private HttpClient httpClient = new HttpClient();
        public Auth0Helper() {
            var syphtApiKey = Environment.GetEnvironmentVariable("SYPHT_API_KEY");
            if (syphtApiKey != null) {
                clientId = syphtApiKey.Split(":")[0];
                clientSecret = syphtApiKey.Split(":")[1];
            } 
        }

        public String login() {

        }
    }
}