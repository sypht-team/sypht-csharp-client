using System;
using System.Threading.Tasks;

namespace Sypht
{
    class Client
    {
        private String SYPHT_URL = "https://api.sypht.com";
        private Auth0Helper auth0Helper = null;

        public Client()
        {
            this.auth0Helper = new Auth0Helper();
        }

        public async Task<String> upload(String fileName)
        {
            var bearerToken = await auth0Helper.login();

            return null;
        }
    }
}