using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace SpecificationPattern.Functions
{
    public static class Program
    {
        public static async Task Main()
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .Build();

            await host.RunAsync();
        }
    }
}