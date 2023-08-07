using CSEData.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace CSEData.Web.Models
{
    public class InsertValues
    {
        public ApplicationDbContext _context;
        private readonly IDataScraper _scraper;
        public InsertValues(ApplicationDbContext context, IDataScraper scraper)
        {
            _context = context;
            _scraper = scraper;
        }
        
        //public void InsertPrice(Price price)
        //{
        //    List<Price> priceList = new List<Price>();
        //    for (int i = 0; i < 10; i++)
        //    {
        //        priceList.Add(new Price { 
        //            CompanyID = Convert.ToInt32(_scraper.SLList[i]), 
        //            LTP = Convert.ToString(_scraper.LTPList[i]),
        //            Open = Convert.ToString(_scraper.LTPList[i]),
        //            High = Convert.ToString(_scraper.LTPList[i]),
        //            Low = Convert.ToString(_scraper.LTPList[i]),
        //            Volume = Convert.ToString(_scraper.LTPList[i]),
        //            Time = DateTime.Today,
        //        });
        //    }
        //    // Add the Price entities to the DbContext.
        //    _context.Prices.AddRange(priceList);

        //    // Save the changes to the database.
        //    _context.SaveChanges();



        //}

    }
}
