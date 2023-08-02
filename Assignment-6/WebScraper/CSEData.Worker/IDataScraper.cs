using HtmlAgilityPack;

namespace CSEData.Worker
{
    public interface IDataScraper
    {
        HtmlDocument GetDocument(string url);
        void GetTheValusOfAllColumn(string url);
    }
}