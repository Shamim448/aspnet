using CSEData.Domain.Entities;
using CSEData.Web.Data;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
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
        private readonly IApplicationDbContext _dBContext;
        public DataService(ILogger<DataService> logger, IDataScraper scraper, IApplicationDbContext dBContext)
        { 
            _logger = logger;
            _scraper = scraper;
            _dBContext = dBContext;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Execution Started");
                try 
                {
                    //Write my logic here
                    _scraper.GetTheValusOfAllColumn("https://www.cse.com.bd/market/current_price");
                    var val = _dBContext.Prices.ToList();
                    
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
        public void Putvalue(Price obj)
        {
            _dBContext.Prices.Add(obj);
           
        }
    }
}
