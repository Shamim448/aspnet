﻿
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
        //private readonly IApplicationDbContext _dBContext;
        //private readonly IDataScraper _scraper;
        public DataService(ILogger<DataService> logger /*IApplicationDbContext dBContext, IDataScraper scraper*/)
        { 
            //_logger = logger;
            //_dBContext = dBContext;
            //_scraper = scraper;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Execution Started");
                try 
                {
                    //Write my logic here
                   // _scraper.GetTheValusOfAllColumn("https://www.cse.com.bd/market/current_price");    
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
