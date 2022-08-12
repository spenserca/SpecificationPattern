using System;
using System.Collections.Generic;
using Microsoft.Extensions.Primitives;
using SpecificationPattern.Common.Models;

namespace SpecificationPattern.Common.Specifications
{
    public class WeatherForecastSummaryQueryStringSpecification : SpecificationBase<WeatherForecast>
    {
        private const string QueryStringKey = "summary";

        public WeatherForecastSummaryQueryStringSpecification(IReadOnlyDictionary<string, StringValues> querystring)
        {
            if (querystring.ContainsKey(QueryStringKey))
            {
                Criteria = wf =>  wf.Summary.Equals(querystring[QueryStringKey], StringComparison.CurrentCultureIgnoreCase);
            }
        }
    }
}