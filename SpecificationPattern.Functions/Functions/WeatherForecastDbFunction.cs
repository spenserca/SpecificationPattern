using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using SpecificationPattern.Common;

namespace SpecificationPattern.Functions.Functions
{
    public class WeatherForecastDbFunction
    {
        private readonly ILogger<WeatherForecastDbFunction> _logger;
        private readonly SpecificationPatternDbContext _dbContext;

        public WeatherForecastDbFunction(ILogger<WeatherForecastDbFunction> logger, SpecificationPatternDbContext dbContext)
        {
            _logger = logger;
            dbContext.Database.EnsureCreated();
            _dbContext = dbContext;
        }
        
        [Function(nameof(WeatherForecastDbFunction))]
        public async Task<HttpResponseData> RunAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "forecasts/db")]
            HttpRequestData request)
        {
            return request.CreateResponse();
        }
    }
}