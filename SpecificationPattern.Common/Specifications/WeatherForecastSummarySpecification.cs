using System;
using SpecificationPattern.Common.Models;

namespace SpecificationPattern.Specifications
{
    public class WeatherForecastSummarySpecification: SpecificationBase<WeatherForecast>
    {
        public WeatherForecastSummarySpecification(string summary)
        {
            Criteria = wf => wf.Summary.Equals(summary, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}