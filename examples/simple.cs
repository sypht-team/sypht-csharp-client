using System;
using System.Threading.Tasks;

namespace Sypht.Examples
{
    class Simple
    {
        static async Task Main(string[] args)
        {
            var sypht = new Client();
            var fileId = await sypht.upload("../../../examples/receipt.pdf");
            Console.WriteLine(await sypht.result(fileId));
        }
    }
}
