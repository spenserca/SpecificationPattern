using System;
using System.Collections.Generic;
using Microsoft.Extensions.Primitives;
using SpecificationPattern.Common.Models;

namespace SpecificationPattern.Common.Specifications
{
    public class WeatherForecastIdQueryStringSpecification: SpecificationBase<WeatherForecast>
    {
        private const string QueryStringKey = "id";


        public WeatherForecastIdQueryStringSpecification(IReadOnlyDictionary<string, StringValues> querystring)
        {
            if (querystring.ContainsKey(QueryStringKey))
            {
                var numericId = Convert.ToInt32(querystring[QueryStringKey]);

                Criteria = wf => wf.Id == numericId;
            }
        }
    }
}