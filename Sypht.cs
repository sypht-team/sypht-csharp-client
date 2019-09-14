using System;
using System.Threading.Tasks;

namespace sypht_csharp_client
{
    class Sypht
    {
        static async Task Main(string[] args)
        {
            var auth = new Auth0Helper();
            Console.WriteLine(await auth.login());
        }
    }
}
