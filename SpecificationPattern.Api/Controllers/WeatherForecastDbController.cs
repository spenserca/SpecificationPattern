using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpecificationPattern.Common;
using SpecificationPattern.Common.Models;

namespace SpecificationPattern.Controllers
{
    [ApiController]
    [Route("/forecasts")]
    public class WeatherForecastDbController : ControllerBase
    {
        private readonly ILogger<WeatherForecastDbController> _logger;
        private readonly SpecificationPatternDbContext _dbContext;

        public WeatherForecastDbController(ILogger<WeatherForecastDbController> logger,
            SpecificationPatternDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet("/db")]
        public async Task<IEnumerable<WeatherForecast>> Get([FromQuery] string summary)
        {
            _logger.LogInformation("getting weather information with direct ef");
            return await _dbContext.WeatherForecasts
                .Where(wf => wf.Summary.Equals(summary, StringComparison.CurrentCultureIgnoreCase))
                .ToListAsync();
        }
    }
}