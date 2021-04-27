using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace MassTransit_EF_WebApp
{
    public class EventConsumer : IConsumer
    {
        ILogger<EventConsumer> _logger;

        WeatherForecastDbContext dbContext;

        public EventConsumer(WeatherForecastDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public EventConsumer(ILogger<EventConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<WeatherForecastBatchMessage> context)
        {
            _logger.LogInformation($"Summary: {context.Message.Forecast.Summary}");
            await dbContext.Forecasts.AddAsync(context.Message.Forecast);

            if (await dbContext.Forecasts.CountAsync() == context.Message.TotalRows)
            {
                using (StreamWriter file = File.CreateText($"./forcasts_{context.Message.RunId}.json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, dbContext.Forecasts);
                }
            }
        }
    }

    public class WeatherForecastDbContext: DbContext
    {
        public WeatherForecastDbContext(DbContextOptions options): base(options)
        {
        }

        public DbSet<WeatherForecast> Forecasts { get; set; }
    }
}