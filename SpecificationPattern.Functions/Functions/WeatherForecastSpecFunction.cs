using System.Net;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using SpecificationPattern.Common;
using SpecificationPattern.Specifications;

namespace SpecificationPattern.Functions.Functions
{
    public class WeatherForecastSpecFunction
    {
        private readonly ILogger<WeatherForecastSpecFunction> _logger;
        private readonly SpecificationPatternDbContext _dbContext;

        public WeatherForecastSpecFunction(ILogger<WeatherForecastSpecFunction> logger,
            SpecificationPatternDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [Function(nameof(WeatherForecastSpecFunction))]
        public async Task<HttpResponseData> RunAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "forecasts/spec")]
            HttpRequestData request)
        {
            _logger.LogInformation("getting weather information with specification pattern");
            var querystring = QueryHelpers.ParseQuery(request.Url.Query);
            var summary = querystring.GetQueryStringValue("summary");

            var forecasts = await _dbContext.WeatherForecasts
                .Where(new WeatherForecastSummarySpecification(summary))
                .ToListAsync();

            var response = request.CreateResponse();
            await response.WriteAsJsonAsync(forecasts, HttpStatusCode.OK);
            return response;
        }
    }
}