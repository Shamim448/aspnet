using CSEData.Web.Data;
using CSEData.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CSEData.Web.Controllers
{
    public class DataController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IDataScraper _scraper;

        public DataController(ApplicationDbContext context, IDataScraper scraper)
        {
            _context = context;
            _scraper = scraper;
            _scraper.GetLisByUrl("https://www.cse.com.bd/market/current_price");

        }
        public IActionResult Index()
        {
            
            List<Price> getAllPrice = _context.Prices.ToList();
            return View(getAllPrice);
        }
    }
}
