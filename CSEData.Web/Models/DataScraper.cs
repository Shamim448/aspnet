using CSEData.Web.Data;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CSEData.Web.Models
{
    public class DataScraper : IDataScraper
    {
        public ApplicationDbContext _context;
        public DataScraper(ApplicationDbContext context)
        {
            _context = context;         
        }
       
        List<Price> priceList = new List<Price>();
        List<Company> companyList = new List<Company>();

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
        public List<Company> InsertCompany(string url)
        {
            GetLisByUrl(url);
            for (int i = 0; i < 30; i++)
            {
                var st = _context.Companys.Where(company => company.StockCodeName == StockCodeList[i]).FirstOrDefault();
                if (st == null)
                {
                    companyList.Add(new Company
                    {
                        StockCodeName = StockCodeList[i]
                    });
                }
            }
            return companyList;
        }
        public List<Price> InsertPrice(string url)
        {

            List<Company> getAllCompany = _context.Companys.ToList();
            GetLisByUrl(url);
            for (int i = 0; i < 30; i++)
            {
                
                
                priceList.Add(new Price
                {
                    CompanyId = getAllCompany[i].Id,
                    LTP = LTPList[i],
                    Open = OpenList[i],
                    High = HighList[i],
                    Low = LowList[i],
                    Volume = VolumeList[i],
                    Time = DateTime.Today,
                }); 
                
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
            //for(int i = 0; i < StockCode.Count; i++)
            //{
            //    priceList.Add(new Price
            //    {
            //        CompanyId = getAllCompany[i].Id,
            //        LTP = LTP[i].InnerText,
            //        Open = Opens[i].InnerText,
            //        High = High[i].InnerText,
            //        Low = Low[i].InnerText,
            //        Volume = Volume[i]I,
            //        Time = DateTime.Today,
            //    });
            //}

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
