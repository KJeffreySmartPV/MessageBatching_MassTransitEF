using MassTransit;
using MassTransit.Testing;
using MassTransit_EF_WebApp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace MassTransit_EF_WebApp_Tests
{
    [Binding]
    public class MessageProcessingFeatureSteps
    {
        private InMemoryTestHarness harness;

        [BeforeScenario]
        public void BeforeScenario()
        {
            var options = new DbContextOptionsBuilder<WeatherForecastDbContext>().UseInMemoryDatabase("myDB").Options;
            this.harness = new InMemoryTestHarness();
            this.harness.Start();
            this.harness.Consumer<EventConsumer>(() => new EventConsumer(new WeatherForecastDbContext(options)));
        }
        
        [Given(@"there is a message on the queue")]
        public async Task GivenThereIsAMessageOnTheQueue()
        {

        }
        
        [Given(@"there are (.*) messages on the queue")]
        public async Task GivenThereAreMessagesOnTheQueue(int messageCount)
        {

        }
        
        [When(@"the message is processed")]
        public void WhenTheMessageIsProcessed()
        {

        }
        
        [When(@"the messages are processed")]
        public void WhenTheMessagesAreProcessed()
        {
        }
        
        [Then(@"the message should be saved to the database")]
        public void ThenTheMessageShouldBeSavedToTheDatabase()
        {
        }
        
        [Then(@"a file with the single message should be created")]
        public void ThenAFileWithTheSingleMessageShouldBeCreated()
        {
        }
        
        [Then(@"the messages should be saved to the database")]
        public void ThenTheMessagesShouldBeSavedToTheDatabase()
        {
        }
        
        [Then(@"a file with all messages should be created")]
        public void ThenAFileWithAllMessagesShouldBeCreated()
        {
        }
    }

    public class ApiFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder webHostBuilder)
        {
            webHostBuilder.ConfigureServices(services =>
              {
                  services.AddMassTransit(c =>
                  {
                      c.UsingInMemory((ctx, cfg) =>
                      {
                          cfg.ReceiveEndpoint("event-listener", e =>
                          {
                              e.ConfigureConsumer<EventConsumer>(ctx);
                          });
                      });
                  });
              });
        }
    }
}
