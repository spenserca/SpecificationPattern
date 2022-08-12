using System.Net;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpecificationPattern.Common;
using SpecificationPattern.Specifications;

namespace SpecificationPattern.Functions.Functions
{
    public class WeatherForecastSpecFunction
    {
        private readonly ILogger<WeatherForecastSpecFunction> _logger;
        private readonly SpecificationPatternDbContext _dbContext;

        public WeatherForecastSpecFunction(ILogger<WeatherForecastSpecFunction> logger, SpecificationPatternDbContext dbContext)
        {
            _logger = logger;
            dbContext.Database.EnsureCreated();
            _dbContext = dbContext;
        }

        [Function(nameof(WeatherForecastSpecFunction))]
        public async Task<HttpResponseData> RunAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "forecasts/spec")]
            HttpRequestData request)
        {
            var summary = string.Empty;
            
            _logger.LogInformation("getting weather information with specification pattern");
            var forecasts = await _dbContext.WeatherForecasts
                .Where(new WeatherForecastSummarySpecification(summary))
                .ToListAsync();
            
            var response = Request.CreateResponse();
            await response.WriteAsJsonAsync(Response.Content, Response.StatusCode, cancellationToken);
        }
    }
}