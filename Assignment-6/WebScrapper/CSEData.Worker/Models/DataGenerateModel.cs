using CSEData.Domain;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSEData.Worker.Models
{
    public class DataGenerateModel
    {
        public HtmlDocument GetDocument( string? url)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            //GetNodsValue(doc);
            return doc;
        }
        public List<Company> GetNodsValue(string? url)
        {
            var val = new List<Company>();
            HtmlDocument doc = GetDocument(url);
            HtmlNodeCollection SL = doc.DocumentNode.SelectNodes(xpath: "//tr/td[1]");
            HtmlNodeCollection StockCode = doc.DocumentNode.SelectNodes(xpath: "//tr/td[2]");
            HtmlNodeCollection LTP = doc.DocumentNode.SelectNodes(xpath: "//tr/td[3]");
            HtmlNodeCollection Opens = doc.DocumentNode.SelectNodes(xpath: "//tr/td[4]");
            HtmlNodeCollection High = doc.DocumentNode.SelectNodes(xpath: "//tr/td[5]");
            HtmlNodeCollection Low = doc.DocumentNode.SelectNodes(xpath: "//tr/td[6]");
            HtmlNodeCollection Volume = doc.DocumentNode.SelectNodes(xpath: "//tr/td[10]");
            if(StockCode != null)
            {
                for(int i = 0;  i < StockCode.Count; i++)
                {
                    var company = new Company()
                    {
                        StockCodeName = StockCode[i].InnerText,
                        Prices = new List<Price>()
                        {
                            new Price()
                            {   
                                LTP = LTP[i].InnerText,
                                Open = Opens[i].InnerText,
                                High = High[i].InnerText,
                                Low = Low[i].InnerText,
                                Volume = Volume[i].InnerText,
                            }
                        }
                    };
                    val.Add(company);
                }
            }
            return val;

        }
    }
}
