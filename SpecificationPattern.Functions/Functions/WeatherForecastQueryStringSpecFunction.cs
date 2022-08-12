using System.Net;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpecificationPattern.Common;
using SpecificationPattern.Common.Specifications;

namespace SpecificationPattern.Functions.Functions;

public class WeatherForecastQueryStringSpecFunction
{
    private readonly ILogger<WeatherForecastQueryStringSpecFunction> _logger;
    private readonly SpecificationPatternDbContext _dbContext;

    public WeatherForecastQueryStringSpecFunction(ILogger<WeatherForecastQueryStringSpecFunction> logger,
        SpecificationPatternDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [Function(nameof(WeatherForecastQueryStringSpecFunction))]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "forecasts/qs-spec")]
        HttpRequestData request)
    {
        _logger.LogInformation("getting weather information with specification pattern");
        var querystring = QueryHelpers.ParseQuery(request.Url.Query);

        var forecasts = await _dbContext.WeatherForecasts
            .Where(new WeatherForecastSummaryQueryStringSpecification(querystring))
            .Where(new WeatherForecastIdQueryStringSpecification(querystring))
            .ToListAsync();

        var response = request.CreateResponse();
        await response.WriteAsJsonAsync(forecasts, HttpStatusCode.OK);
        return response;
    }
}