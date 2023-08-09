
using CSEData.Application.Services;

namespace CSEData.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IWebScraperService _load;
        public Worker(ILogger<Worker> logger, IWebScraperService load)
        {
            _logger = logger;
            _load = load;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                 _load.LoadAsunc("https://www.cse.com.bd/market/current_price");                              
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000 * 60, stoppingToken);
            }
        }
    }
}