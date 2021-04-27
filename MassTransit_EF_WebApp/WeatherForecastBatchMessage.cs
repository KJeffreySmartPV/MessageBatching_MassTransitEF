using System;

namespace MassTransit_EF_WebApp
{
    public class WeatherForecastBatchMessage
    {
        public Guid RunId { get; set; }
        public int TotalRows { get; set; }
        public WeatherForecast Forecast { get; set; }
    }
}
