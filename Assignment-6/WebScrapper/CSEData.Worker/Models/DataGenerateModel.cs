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
        public HtmlNodeCollection SL { get; set; }
        public HtmlNodeCollection StockCode { get; set; }
        public HtmlNodeCollection LTP { get; set; }
        public HtmlNodeCollection Opens { get; set; }
        public HtmlNodeCollection High { get; set; }
        public HtmlNodeCollection Low { get; set; }
        public HtmlNodeCollection Volume { get; set; }

        public HtmlDocument GetDocument( string? url)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            //GetNodsValue(doc);
            return doc;
        }
        public void /*List<Company>*/ GetNodsValue(string? url)
        {
            var val = new List<Company>();
            HtmlDocument doc = GetDocument(url);
             SL = doc.DocumentNode.SelectNodes(xpath: "//tr/td[1]");
             StockCode = doc.DocumentNode.SelectNodes(xpath: "//tr/td[2]");
             LTP = doc.DocumentNode.SelectNodes(xpath: "//tr/td[3]");
             Opens = doc.DocumentNode.SelectNodes(xpath: "//tr/td[4]");
             High = doc.DocumentNode.SelectNodes(xpath: "//tr/td[5]");
             Low = doc.DocumentNode.SelectNodes(xpath: "//tr/td[6]");
             Volume = doc.DocumentNode.SelectNodes(xpath: "//tr/td[10]");
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
           // return val;

        }
    }
}
