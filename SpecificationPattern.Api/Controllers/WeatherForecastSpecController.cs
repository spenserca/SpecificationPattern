using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpecificationPattern.Common;
using SpecificationPattern.Common.Models;
using SpecificationPattern.Specifications;

namespace SpecificationPattern.Controllers
{
    [ApiController]
    [Route("/forecasts")]
    public class WeatherForecastSpecController : ControllerBase
    {
        private readonly ILogger<WeatherForecastSpecController> _logger;
        private readonly SpecificationPatternDbContext _dbContext;

        public WeatherForecastSpecController(ILogger<WeatherForecastSpecController> logger,
            SpecificationPatternDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet("/spec")]
        public async Task<IEnumerable<WeatherForecast>> Get([FromQuery] string summary)
        {
            _logger.LogInformation("getting weather information with specification pattern");
            return await _dbContext.WeatherForecasts
                .Where(new WeatherForecastSummarySpecification(summary))
                .ToListAsync();
        }
    }
}