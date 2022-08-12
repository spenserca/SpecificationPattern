using SpecificationPattern.Common.Models;

namespace SpecificationPattern.Common.Specifications
{
    public class WeatherForecastIdSpecification : SpecificationBase<WeatherForecast>
    {
        public WeatherForecastIdSpecification(int id)
        {
            Criteria = wf => wf.Id == id;
        }
    }
}