using CSEData.Worker.DataController;
using CSEData.Worker.Models;

namespace CSEData.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ValueLoadToModel _load;
       // private readonly CompanyCreateModel _companyCreate;

        public Worker(ILogger<Worker> logger, ValueLoadToModel load)
        {
            _logger = logger;
            _load = load;
            //_companyCreate = companyCreate;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                _load.Load("https://www.cse.com.bd/market/current_price");
                
                
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000 * 60 * 3 , stoppingToken);
            }
        }
    }
}