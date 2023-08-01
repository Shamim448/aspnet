using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSEData.Worker
{
    public class DataService : BackgroundService
    {
        private readonly ILogger<DataService> _logger;
        private readonly IDataScraper _scraper;
        public DataService(ILogger<DataService> logger, IDataScraper scraper)
        { 
            _logger = logger;
            _scraper = scraper;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Execution Started");
                try 
                {
                    //Write my logic here
                    _scraper.GetLisByUrl("https://www.cse.com.bd/market/current_price");
                    //foreach(var name in stockConeName)
                    //{
                    //    await Console.Out.WriteLineAsync(name);
                    //    _logger.LogInformation($"{name}");
                    //}

                }
                catch(Exception ex) 
                { 
                    _logger.LogError(ex, ex.Message);
                }
                await Task.Delay(90000, stoppingToken);
            }
        }
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Service Started");
            return base.StartAsync(cancellationToken);
        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Service Stoped");
            return base.StopAsync(cancellationToken); 
        }
        
    }
}
