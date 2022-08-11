using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpecificationPattern.Specifications;

namespace SpecificationPattern.Controllers
{
    [ApiController]
    [Route("/forecasts")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly SpecificationPatternDbContext _dbContext;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            SpecificationPatternDbContext dbContext)
        {
            _logger = logger;
            dbContext.Database.EnsureCreated();
            _dbContext = dbContext;
        }

        [HttpGet("/where")]
        public async Task<IEnumerable<WeatherForecast>> Get([FromQuery] string summary)
        {
            return await _dbContext.WeatherForecasts
                .Where(wf => wf.Summary.Equals(summary, StringComparison.CurrentCultureIgnoreCase))
                .ToListAsync();
        }

        [HttpGet("/specification")]
        public async Task<IEnumerable<WeatherForecast>> Post([FromQuery] string summary)
        {
            return await _dbContext.WeatherForecasts
                .Where(new WeatherForecastSummarySpecification(summary))
                .ToListAsync();
        }
    }
}