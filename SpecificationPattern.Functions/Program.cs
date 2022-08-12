using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpecificationPattern.Common;

namespace SpecificationPattern.Functions
{
    public static class Program
    {
        public static async Task Main()
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureServices(services =>
                {
                    services.AddDbContext<Common.SpecificationPatternDbContext>(
                        //Since the DbConnection is not opened, EF will take ownership of the lifecycle
                        options => { options.UseInMemoryDatabase("SpecificationPatternFunctions"); });


                    using (var serviceProvider = services.BuildServiceProvider())
                    {
                        var dbContext = serviceProvider.GetRequiredService<SpecificationPatternDbContext>();
                        dbContext.Database.EnsureCreated();
                    }
                })
                .Build();


            await host.RunAsync();
        }
    }
}