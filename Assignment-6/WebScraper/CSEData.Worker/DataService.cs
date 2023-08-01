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
                    GetLisByUrl("https://www.cse.com.bd/market/current_price");
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
        static HtmlDocument GetDocument(string url)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            return doc;
        }
        static void GetLisByUrl(string url){
            List<string> stockCodeList = new List<string>();
            List<string> Price = new List<string>();

            HtmlDocument doc = GetDocument(url);
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(xpath: "//td/a");
            //HtmlNodeCollection nodes1 = doc.DocumentNode.SelectNodes(xpath: "//td[@class='trdPrice']/span");
            HtmlNodeCollection SL = doc.DocumentNode.SelectNodes(xpath: "//tr/td[1]");
            HtmlNodeCollection StockCode = doc.DocumentNode.SelectNodes(xpath: "//tr/td[2]");
            HtmlNodeCollection LTP = doc.DocumentNode.SelectNodes(xpath: "//tr/td[3]");
            HtmlNodeCollection Open = doc.DocumentNode.SelectNodes(xpath: "//tr/td[4]");
            HtmlNodeCollection High = doc.DocumentNode.SelectNodes(xpath: "//tr/td[5]");
            HtmlNodeCollection Low = doc.DocumentNode.SelectNodes(xpath: "//tr/td[6]");
            HtmlNodeCollection Volumn = doc.DocumentNode.SelectNodes(xpath: "//tr/td[10]");
            foreach (HtmlNode node in nodes)
            {
                stockCodeList.Add(node.InnerText);               
            }
            foreach (var node in stockCodeList)
            {
                Console.WriteLine(node);
            }
            foreach (HtmlNode node in LTP)
            {
                Price.Add(node.InnerText);
            }
            foreach (var node in Price)
            {
                Console.WriteLine(node);
            }

        }
    }
}
