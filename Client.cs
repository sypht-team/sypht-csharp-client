using System;
using System.Threading.Tasks;

namespace Sypht
{
    class Client
    {
        private String SYPHT_URL = "https://api.sypht.com";
        private Auth0Helper _auth0Helper = null;

        public Client()
        {
            this._auth0Helper = new Auth0Helper();
        }

        public async Task<String> upload(String fileName)
        {
            Console.WriteLine(await _auth0Helper.login());

            return null;
        }
    }
}