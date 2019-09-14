using System;
using System.Threading.Tasks;

namespace Sypht.Examples
{
    class Simple
    {
        static async Task Main(string[] args)
        {
            var sypht = new Client();
            Console.WriteLine(await sypht.upload("../../../examples/receipt.pdf"));
        }
    }
}
