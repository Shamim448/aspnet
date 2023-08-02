using HtmlAgilityPack;

namespace CSEData.Web.Models
{
    public interface IDataScraper
    {
        HtmlDocument GetDocument(string url);
        void GetLisByUrl(string url);
    }
}