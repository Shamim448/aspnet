using CSEData.Web.Data;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;

namespace CSEData.Web.Models
{
    public class DataScraper : IDataScraper
    {
        List<Price> priceList = new List<Price>();

        List<string> SLList = new List<string>();
        List<string> StockCodeList = new List<string>();
        List<string> LTPList = new List<string>();
        List<string> OpenList = new List<string>();
        List<string> HighList = new List<string>();
        List<string> LowList = new List<string>();
        List<string> VolumeList = new List<string>();
        public HtmlDocument GetDocument(string? url)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            return doc;
        }
        public List<Price> InsertPrice(string url)
        {
            GetLisByUrl(url);
            for (int i = 0; i < 30; i++)
            {

                priceList.Add(new Price
                {
                    CompanyId = Convert.ToInt32(SLList[i]),
                    LTP = Convert.ToString(LTPList[i]),
                    Open = Convert.ToString(LTPList[i]),
                    High = Convert.ToString(LTPList[i]),
                    Low = Convert.ToString(LTPList[i]),
                    Volume = Convert.ToString(LTPList[i]),
                    Time = DateTime.Today,
                });
                // Add the Price entities to the DbContext.
                // _context.Prices.AddRange(priceList);
                // Save the changes to the database.
                // _context.SaveChanges();
            }
            return priceList;
        }
        public void GetLisByUrl(string? url)
        {          
            HtmlDocument doc = GetDocument(url);
            HtmlNodeCollection SL = doc.DocumentNode.SelectNodes(xpath: "//tr/td[1]");
            HtmlNodeCollection StockCode = doc.DocumentNode.SelectNodes(xpath: "//tr/td[2]");
            HtmlNodeCollection LTP = doc.DocumentNode.SelectNodes(xpath: "//tr/td[3]");
            HtmlNodeCollection Opens = doc.DocumentNode.SelectNodes(xpath: "//tr/td[4]");
            HtmlNodeCollection High = doc.DocumentNode.SelectNodes(xpath: "//tr/td[5]");
            HtmlNodeCollection Low = doc.DocumentNode.SelectNodes(xpath: "//tr/td[6]");
            HtmlNodeCollection Volume = doc.DocumentNode.SelectNodes(xpath: "//tr/td[10]");

            foreach (HtmlNode node in SL)
            {
                SLList.Add(node.InnerText);
            }

            foreach (HtmlNode node in StockCode)
            {
                StockCodeList.Add(node.InnerText);
            }
            foreach (HtmlNode node in LTP)
            {
                LTPList.Add(node.InnerText);
            }

            foreach (HtmlNode node in Opens)
            {
                OpenList.Add(node.InnerText);
            }
            foreach (HtmlNode node in High)
            {
                HighList.Add(node.InnerText);
            }
            foreach (HtmlNode node in Low)
            {
                LowList.Add(node.InnerText);
            }

            foreach (HtmlNode node in Volume)
            {
                VolumeList.Add(node.InnerText);
            }

            
            

            
        }

        
    }
}
