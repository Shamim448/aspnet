using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSEData.Worker
{
    public class DataScraper : IDataScraper
    {
        public HtmlDocument GetDocument(string url)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            return doc;
        }
        public void GetTheValusOfAllColumn(string url)
        {
            List<string> SLList = new List<string>();
            List<string> stockCodeList = new List<string>();
            List<string> LTPList = new List<string>();
            List<string> OpenList = new List<string>();
            List<string> HighList = new List<string>();
            List<string> LowList = new List<string>();
            List<string> VolumnList = new List<string>();

            HtmlDocument doc = GetDocument(url);
            //HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(xpath: "//td/a");
            ////HtmlNodeCollection nodes1 = doc.DocumentNode.SelectNodes(xpath: "//td[@class='trdPrice']/span");
            HtmlNodeCollection SL = doc.DocumentNode.SelectNodes(xpath: "//tr/td[1]");
            HtmlNodeCollection StockCode = doc.DocumentNode.SelectNodes(xpath: "//tr/td[2]");
            HtmlNodeCollection LTP = doc.DocumentNode.SelectNodes(xpath: "//tr/td[3]");
            HtmlNodeCollection Open = doc.DocumentNode.SelectNodes(xpath: "//tr/td[4]");
            HtmlNodeCollection High = doc.DocumentNode.SelectNodes(xpath: "//tr/td[5]");
            HtmlNodeCollection Low = doc.DocumentNode.SelectNodes(xpath: "//tr/td[6]");
            HtmlNodeCollection Volumn = doc.DocumentNode.SelectNodes(xpath: "//tr/td[10]");
            foreach (HtmlNode node in SL)
            {
                SLList.Add(node.InnerText);
            }
            foreach (HtmlNode node in StockCode)
            {
                stockCodeList.Add(node.InnerText);
            }
            foreach (HtmlNode node in LTP)
            {
                LTPList.Add(node.InnerText);
            }
            foreach (HtmlNode node in Open)
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
            foreach (HtmlNode node in Volumn)
            {
                VolumnList.Add(node.InnerText);
            }

        }
    }
}
