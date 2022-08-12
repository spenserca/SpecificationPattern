using System.Net;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using SpecificationPattern.Common;

namespace SpecificationPattern.Functions.Functions
{
    public class WeatherForecastDbFunction
    {
        private readonly ILogger<WeatherForecastDbFunction> _logger;
        private readonly SpecificationPatternDbContext _dbContext;

        public WeatherForecastDbFunction(ILogger<WeatherForecastDbFunction> logger,
            SpecificationPatternDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [Function(nameof(WeatherForecastDbFunction))]
        public async Task<HttpResponseData> RunAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "forecasts/db")]
            HttpRequestData request)
        {
            _logger.LogInformation("getting weather information with direct ef");
            var querystring = QueryHelpers.ParseQuery(request.Url.Query);
            var summary = querystring.GetQueryStringValue("summary");

            var forecasts = await _dbContext.WeatherForecasts
                .Where(wf => wf.Summary.Equals(summary, StringComparison.CurrentCultureIgnoreCase))
                .ToListAsync();

            var response = request.CreateResponse();
            await response.WriteAsJsonAsync(forecasts, HttpStatusCode.OK);
            return response;
        }
    }
}