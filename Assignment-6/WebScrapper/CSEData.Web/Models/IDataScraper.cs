using HtmlAgilityPack;

namespace CSEData.Web.Models
{
    public interface IDataScraper
    {
        //public List<string> SLList { get; set; }
        //public List<string> LTPList { get; set; }
        
        HtmlDocument GetDocument(string url);
        void GetLisByUrl(string url);
        List<Price> InsertPrice(string url);
    }
}