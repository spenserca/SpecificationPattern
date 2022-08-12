using System.Net;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using SpecificationPattern.Common;
using SpecificationPattern.Common.Specifications;

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
            var id = querystring.GetQueryStringValue("id");

            var forecastsQuery = _dbContext.WeatherForecasts.AsQueryable();
            if (!string.IsNullOrEmpty(summary))
            {
                forecastsQuery = forecastsQuery.Where(new WeatherForecastSummarySpecification(summary));
            }

            if (!string.IsNullOrEmpty(id))
            {
                forecastsQuery = forecastsQuery.Where(new WeatherForecastIdSpecification(Convert.ToInt32(id)));
            }

            var forecasts = await forecastsQuery.ToListAsync();

            var response = request.CreateResponse();
            await response.WriteAsJsonAsync(forecasts, HttpStatusCode.OK);
            return response;
        }
    }
}