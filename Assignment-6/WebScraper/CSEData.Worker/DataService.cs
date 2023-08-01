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
        public DataService(ILogger<DataService> logger)
        { 
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Execution Started");
                try 
                {
                    //Write my logic here

                }
                catch(Exception ex) 
                { 
                    _logger.LogError(ex, ex.Message);
                }
                await Task.Delay(1000, stoppingToken);
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
