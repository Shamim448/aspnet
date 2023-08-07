using CSEData.Worker.Models;

namespace CSEData.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly CompanyCreateModel _company;

        public Worker(ILogger<Worker> logger, CompanyCreateModel company)
        {
            _logger = logger;
            _company = company;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _company.StockCodeName = "Shamim";
                _company.CreateCompany();
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}